using System;
using System.Collections.Generic;
using Controllers;
using Providers;
using Providers.Storage;
using TinyMessenger;
using TinyMessenger.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartPageUI : MonoBehaviour
    {
        public TMP_Dropdown SavedConfigurationsDropdown;
        public Button LoadConfigurationButton;

        private IConfigurationStorage _configurationStorage;
        private ConfiguratorController _controller;
        private ITinyMessengerHub _eventBus;

        public void OnStartConfigurationButtonClick()
        {
            _controller.CreateDefaultConfiguration();
            _eventBus.Publish(new SetConfigurationUIStateEvent(this));
        }

        public void OnLoadConfigurationButtonClick()
        {
            if (SavedConfigurationsDropdown.value == 0) return;
            _controller.LoadAndApplyConfiguration(SavedConfigurationsDropdown.options[SavedConfigurationsDropdown.value].text);
            _eventBus.Publish(new SetConfigurationUIStateEvent(this));
        }

        private void Awake()
        {
            _configurationStorage = ServiceLocator.Instance.GetService<IConfigurationStorage>();
            _controller = ServiceLocator.Instance.GetService<ConfiguratorController>();
            _eventBus = ServiceLocator.Instance.GetService<ITinyMessengerHub>();
            SavedConfigurationsDropdown.onValueChanged.AddListener((i) =>
            {
                LoadConfigurationButton.interactable = i != 0;
            });
        }

        private void OnEnable()
        {
            var optionData = new List<TMP_Dropdown.OptionData>();
            optionData.Add(new TMP_Dropdown.OptionData("Select"));
            foreach (var storedConfiguration in _configurationStorage.GetStoredConfigurations())
            {
                optionData.Add(new TMP_Dropdown.OptionData(storedConfiguration));
            }

            SavedConfigurationsDropdown.options = optionData;
        }
    }
}