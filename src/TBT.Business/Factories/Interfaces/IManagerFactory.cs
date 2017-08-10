namespace TBT.Business.Factories.Interfaces
{
    public interface IManagerFactory
    {
        T Create<T>(string managerName);
    }
}
