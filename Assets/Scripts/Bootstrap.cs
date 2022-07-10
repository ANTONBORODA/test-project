using Controllers;
using Providers;
using Providers.Configuration;
using Providers.Previews;
using Providers.Storage;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public void Awake()
    {
        var locator = new ServiceLocator();
        locator.AddService(typeof(IConfigurationDataProvider), FindObjectOfType<DefaultConfigurationDataProvider>());
        locator.AddService(typeof(IPreviewProvider), FindObjectOfType<LocalPreviewProvider>());
        locator.AddService(typeof(IConfigurationStorage), new PlayerPrefsConfigurationStorage());
        locator.AddService(typeof(ConfiguratorController),
            new ConfiguratorController(ServiceLocator.Instance.GetService<IConfigurationDataProvider>(),
                ServiceLocator.Instance.GetService<IConfigurationStorage>()));
    }
}