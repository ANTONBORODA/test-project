using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Controllers;
using Model;
using Model.Options;
using Model.Options.Extensions;
using Providers;
using UI.GroupDrawers;
using UI.OptionDrawers;
using UnityEngine;
using Component = UnityEngine.Component;

namespace UI
{
    public class ConfigurationUI : MonoBehaviour
    {
        public StartPageUI StartPageUI;
        public SaveConfigurationUI SaveConfigurationUI;
        public Transform ContentParent;
        public ToggleGroupWithNameAndDescriptionDrawer ToggleGroupWithNameAndDescriptionPrefab;
        public ToggleGroupWithNameDrawer ToggleGroupWithNameTextPrefab;
        public ToggleGroupWithPreviewDrawer ToggleGroupWithPreviewPrefab;
        
        private ConfiguratorController _controller;
        private ConfigurationData _configurationData;

        private readonly Dictionary<Option, IOptionDrawer> _optionDrawers = new();
        private List<GameObject> _groupDrawers = new();

        private void Awake()
        {
            _controller = ServiceLocator.Instance.GetService<ConfiguratorController>();
            _controller.PropertyChanged += OnControllerPropertyChanged;
        }

        private void Start()
        {
            _configurationData = _controller.Data;
            if (_configurationData != null)
                UpdateConfigurationData();
        }

        public void OnSaveButtonClick()
        {
            SaveConfigurationUI.gameObject.SetActive(true);
        }

        public void OnBackButtonClick()
        {
            TransitionToStartPage();
        }

        private void TransitionToStartPage()
        {
            this.gameObject.SetActive(false);
            StartPageUI.gameObject.SetActive(true);
        }

        private void OnControllerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Configuration")
            {
                UpdateDisplayedConfiguration(_controller.CurrentConfiguration);
            }

            if (e.PropertyName == "Data")
            {
                _configurationData = _controller.Data;
                UpdateConfigurationData();
            }
        }

        private void UpdateConfigurationData()
        {
            foreach (var drawer in _groupDrawers)
            {
                Destroy(drawer);
            }
            _groupDrawers.Clear();
            
            foreach (var group in _configurationData.Groups)
            {
                IGroupDrawer drawer = null;
                if (group.PreviewType == PreviewType.NameWithDescription)
                {
                    drawer = Instantiate(ToggleGroupWithNameAndDescriptionPrefab, ContentParent);
                }
                if (group.PreviewType == PreviewType.Name)
                {
                    drawer = Instantiate(ToggleGroupWithNameTextPrefab, ContentParent);
                }

                if (group.PreviewType == PreviewType.Image)
                {
                    drawer = Instantiate(ToggleGroupWithPreviewPrefab, ContentParent);
                }
                
                if (drawer == null) continue;
                
                foreach (var optionDrawer in drawer.CreateOptionDrawers(group))
                {
                    _optionDrawers.Add(optionDrawer.Option, optionDrawer);
                    optionDrawer.OptionChanged += OnOptionChanged;
                }
                _groupDrawers.Add(((Component)drawer).gameObject);
                
            }
            UpdateDisplayedConfiguration(_controller.CurrentConfiguration);
        }

        private void UpdateDisplayedConfiguration(IConfiguration configuration)
        {
            if (_optionDrawers.Count == 0) return;
            foreach (var option in _configurationData.Options)
            {
                if (!_optionDrawers.ContainsKey(option)) continue;
                var state = OptionDrawingState.Unselected;
                if (option.GetOptionStateInContext(configuration, _configurationData) == OptionExtensions.OptionState.Unavailable)
                {
                    state = OptionDrawingState.Hidden;
                }
                else
                {
                    if (configuration.Options.Contains(option))
                        state = OptionDrawingState.Selected;
                    else
                        state = OptionDrawingState.Unselected;
                }
                
                _optionDrawers[option].SetOptionState(state);
                _optionDrawers[option].IsOptional =
                    option.GetRelationType(_controller.CurrentConfiguration, _configurationData) ==
                    RelationType.Optional;

            }
        }

        private void OnOptionChanged(Option op, bool value)
        {
            if (value) _controller.AddOption(op);
            else _controller.RemoveOption(op);
        }
    }
}