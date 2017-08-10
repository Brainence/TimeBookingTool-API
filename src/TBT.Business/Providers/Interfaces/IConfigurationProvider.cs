namespace TBT.Business.Providers.Interfaces
{
    public interface IConfigurationProvider
    {
        string ConnectionString(string name);

        T Get<T>(string key);
    }
}
