using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Providers.Configuration
{
    public class DefaultConfigurationDataProvider : MonoBehaviour, IConfigurationDataProvider
    {
        public TextAsset BuiltInConfigurationData;
        
        public Model.Options.ConfigurationData GetConfigurationData()
        {
            return JsonConvert.DeserializeObject<Model.Options.ConfigurationData>(BuiltInConfigurationData.text,
                new JsonSerializerSettings {PreserveReferencesHandling = PreserveReferencesHandling.Objects});
        }
    }
}