using System;
using System.Threading.Tasks;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IApplicationUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IProjectRepository Projects { get; }
        IActivityRepository Activities { get; }
        ITimeEntryRepository TimeEntries { get; }
        IUserRepository Users { get; }
        IUserProjectRepository UserProjects { get; }
        IResetTicketRepository ResetTickets { get; }
        ICompanyRepository Companies { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
