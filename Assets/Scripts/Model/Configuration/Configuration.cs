using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model.Options;

namespace Model.Configuration
{
    /// <summary>
    /// Represents current selected configuration 
    /// </summary>
    public class Configuration : IConfiguration
    {
        private readonly List<Option> _options = new();

        public string Name { get; set; }
        public IReadOnlyCollection<Option> Options => new ReadOnlyCollection<Option>(_options);

        public bool AddOption(Option option)
        {
            if (_options.Contains(option)) return false;
            _options.Add(option);
            return true;
        }

        public bool RemoveOption(Option option)
        {
            return _options.Remove(option);
        }

    }
}