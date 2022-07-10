namespace Providers.Configuration
{
    public interface IConfigurationDataProvider
    {
        Model.Options.ConfigurationData GetConfigurationData();
    }
}