using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Model;
using Model.Options;
using Model.Options.Extensions;
using Providers;
using Providers.Configuration;
using Providers.Storage;

namespace Controllers
{
    public class ConfiguratorController : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ConfigurationData Data
        {
            get => _configurationData;
            set
            {
                _configurationData = value;
                OnPropertyChanged();
            }
        }

        public IConfiguration CurrentConfiguration => _configuration;

        private Configuration Configuration
        {
            set
            {
                _configuration = value;
                OnPropertyChanged();
            }
        }

        private Configuration _configuration;
        private ConfigurationData _configurationData;
        private readonly IConfigurationStorage _configurationStorage;
        private readonly IConfigurationDataProvider _configurationDataProvider;

        public ConfiguratorController(IConfigurationDataProvider dataProvider, IConfigurationStorage storage)
        {
            _configurationStorage = storage;
            _configurationDataProvider = dataProvider;
            GetConfigurationData();
            CreateDefaultConfiguration();
        }

        private void GetConfigurationData()
        {
            Data = _configurationDataProvider.GetConfigurationData();
        }

        public void AddOption(Option option)
        {
            if (_configuration.Options.Contains(option)) return;
            
            var groups = option.GetOptionGroups(Data);
            foreach (var group in groups)
            {
                if (group.GroupType != GroupType.SingleSelect) continue;
                foreach (var groupOption in group.Options)
                {
                    RemoveOption(groupOption);
                }
            }
            var optionAdded = _configuration.AddOption(option);
            var defaultOptions = option.GetRelatedGroupsDefaultOptionsInContext(_configuration, Data);
            foreach (var defaultOption in defaultOptions)
            {
                if (_configuration.AddOption(defaultOption))
                    optionAdded = true;
            }
            if (optionAdded)
                OnPropertyChanged(nameof(Configuration));
        }

        public void RemoveOption(Option option)
        {
            var optionRemoved = _configuration.RemoveOption(option);
            if (optionRemoved)
            {
                foreach (var relatedOption in option.GetAllRelatedOptions(Data))
                {
                    _configuration.RemoveOption(option);
                }
            }
            if (optionRemoved)
                OnPropertyChanged(nameof(Configuration));
        }

        public void LoadAndApplyConfiguration(string name)
        {
            Configuration = ServiceLocator.Instance.GetService<IConfigurationStorage>()
                .LoadConfigurationByName(name, _configurationData);
        }

        public void SaveConfiguration(string name)
        {
            _configuration.Name = name;
            ServiceLocator.Instance.GetService<IConfigurationStorage>().StoreConfiguration(_configuration);
        }

        public void CreateDefaultConfiguration()
        {
            _configuration = new Configuration();
            var rootOption = Data.GetDefaultRootOption();
            AddOption(rootOption);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}