using System.Configuration;
namespace TBT.Components.Interfaces.ConfigurationManager
{
    public interface IConfigurationManager
    {
        T Get<T>(string key);
        ConnectionStringSettingsCollection ConnectionStrings { get; }
    }
}
