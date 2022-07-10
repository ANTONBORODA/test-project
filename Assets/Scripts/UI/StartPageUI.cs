using System;
using System.Collections.Generic;
using Controllers;
using Providers;
using Providers.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartPageUI : MonoBehaviour
    {
        public ConfigurationUI ConfigurationUI;
        public TMP_Dropdown SavedConfigurationsDropdown;
        public Button LoadConfigurationButton;

        private IConfigurationStorage _configurationStorage;
        private ConfiguratorController _controller;

        public void OnStartConfigurationButtonClick()
        {
            _controller.CreateDefaultConfiguration();
            TransitionToConfiguration();
        }

        public void OnLoadConfigurationButtonClick()
        {
            if (SavedConfigurationsDropdown.value == 0) return;
            _controller.LoadAndApplyConfiguration(SavedConfigurationsDropdown.options[SavedConfigurationsDropdown.value].text);
            TransitionToConfiguration();
        }

        private void TransitionToConfiguration()
        {
            ConfigurationUI.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
        private void Start()
        {
            _configurationStorage = ServiceLocator.Instance.GetService<IConfigurationStorage>();
            _controller = ServiceLocator.Instance.GetService<ConfiguratorController>();
            var optionData = new List<TMP_Dropdown.OptionData>();
            optionData.Add(new TMP_Dropdown.OptionData("Select"));
            foreach (var storedConfiguration in _configurationStorage.GetStoredConfigurations())
            {
                optionData.Add(new TMP_Dropdown.OptionData(storedConfiguration));
            }

            SavedConfigurationsDropdown.options = optionData;
        }

        private void Update()
        {
            LoadConfigurationButton.interactable = SavedConfigurationsDropdown.value != 0;
        }
    }
}