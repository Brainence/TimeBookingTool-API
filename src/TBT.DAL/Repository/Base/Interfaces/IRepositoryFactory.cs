namespace TBT.DAL.Repository.Interfaces
{
    public interface IRepositoryFactory
    {
        T Create<T>(string repositoryName);
    }
}
