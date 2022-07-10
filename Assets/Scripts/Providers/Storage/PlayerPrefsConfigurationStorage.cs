using System;
using System.Collections.Generic;
using System.Linq;
using Model.Options;
using Newtonsoft.Json;
using UnityEngine;

namespace Providers.Storage
{
    public class PlayerPrefsConfigurationStorage : IConfigurationStorage
    {
        private const string PLAYER_PREFS_CONFIGURATION_STORAGE_KEY = "cfg";
        
        public string[] GetStoredConfigurations()
        {
            var items = GetStoredCollection();
            return items.Items.Select(t => t.Name).ToArray();
        }

        public void StoreConfiguration(Model.Configuration configuration)
        {
            var items = GetStoredCollection();

            var existingConfiguration = items.Items.FirstOrDefault(t => t.Name == configuration.Name);
            if (existingConfiguration != null)
                items.Items.Remove(existingConfiguration);

            items.Items.Add(ConvertToStoredConfiguration(configuration));

            var serializedCollection = JsonConvert.SerializeObject(items);
            PlayerPrefs.SetString(PLAYER_PREFS_CONFIGURATION_STORAGE_KEY, serializedCollection);
            PlayerPrefs.Save();
        }

        public Model.Configuration LoadConfigurationByName(string name, ConfigurationData data)
        {
            var items = GetStoredCollection();
            var storedConfiguration = items.Items.FirstOrDefault(t => t.Name == name);
            if (storedConfiguration == null) return null;

            var configuration = new Model.Configuration();
            configuration.Name = name;
            foreach (var option in storedConfiguration.Options)
            {
                var optionData = data.Options.FirstOrDefault(t => t.Code == option);
                if (optionData == null) continue;
                configuration.AddOption(optionData);
            }

            return configuration;
        }

        private PlayerPrefsConfigurationStorageCollection GetStoredCollection()
        {
            var items = new PlayerPrefsConfigurationStorageCollection();
            if (PlayerPrefs.HasKey(PLAYER_PREFS_CONFIGURATION_STORAGE_KEY))
            {
                items = JsonConvert.DeserializeObject<PlayerPrefsConfigurationStorageCollection>(
                    PlayerPrefs.GetString(PLAYER_PREFS_CONFIGURATION_STORAGE_KEY));
            }

            return items;
        }

        private PlayerPrefsConfigurationStorageItem ConvertToStoredConfiguration(Model.Configuration configuration)
        {
            var item = new PlayerPrefsConfigurationStorageItem();
            item.Name = configuration.Name;
            item.Options = configuration.Options.Select(t => t.Code).ToArray();
            return item;
        }
        
        private class PlayerPrefsConfigurationStorageCollection
        {
            public List<PlayerPrefsConfigurationStorageItem> Items = new();
        }
        
        [Serializable]
        private class PlayerPrefsConfigurationStorageItem
        {
            public string Name;
            public string[] Options;
        }
    }
}