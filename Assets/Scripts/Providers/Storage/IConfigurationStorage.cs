using Model.Options;

namespace Providers.Storage
{
    public interface IConfigurationStorage
    {
        string[] GetStoredConfigurations();
        void StoreConfiguration(Model.Configuration.Configuration configuration);
        Model.Configuration.Configuration LoadConfigurationByName(string name, ConfigurationData data);
    }
}