using Model.Options;

namespace Providers.Storage
{
    public interface IConfigurationStorage
    {
        string[] GetStoredConfigurations();
        void StoreConfiguration(Model.Configuration configuration);
        Model.Configuration LoadConfigurationByName(string name, ConfigurationData data);
    }
}