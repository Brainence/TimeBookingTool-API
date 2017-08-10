using System;
using System.Data.Entity;
using System.Threading.Tasks;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Repository.Implementations
{
    public class ApplicationUnitOfWork : IApplicationUnitOfWork
    {
        private DbContext _context;
        private IRepositoryFactory _repositoryFactory;
        private ICustomerRepository _customersRepository;
        private IProjectRepository _projectsRepository;
        private IActivityRepository _activityRepository;
        private ITimeEntryRepository _timeEntriesRepository;
        private IUserRepository _usersRepository;
        private IUserProjectRepository _userProjectRepository;
        private IResetTicketRepository _resetTicketRepository;

        public ApplicationUnitOfWork(IRepositoryFactory repositoryFactory, DbContext context)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
        }

        public IActivityRepository Activities
        {
            get { return _activityRepository ?? (_activityRepository = new ActivityRepository(_context)); }
        }

        public ICustomerRepository Customers
        {
            get { return _customersRepository ?? (_customersRepository = new CustomerRepository(_context)); }
        }

        public IProjectRepository Projects
        {
            get { return _projectsRepository ?? (_projectsRepository = new ProjectRepository(_context)); }
        }

        public ITimeEntryRepository TimeEntries
        {
            get { return _timeEntriesRepository ?? (_timeEntriesRepository = new TimeEntryRepository(_context)); }
        }

        public IUserRepository Users
        {
            get { return _usersRepository ?? (_usersRepository = new UserRepository(_context)); }
        }

        public IUserProjectRepository UserProjects
        {
            get { return _userProjectRepository ?? (_userProjectRepository = new UserProjectRepository(_context)); }
        }

        public IResetTicketRepository ResetTickets
        {
            get { return _resetTicketRepository ?? (_resetTicketRepository = new ResetTicketRepository(_context)); }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            //try
            //{
            //    return await _context.SaveChangesAsync();
            //}
            //catch (DbEntityValidationException dbEx)
            //{
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            Trace.TraceInformation("Property: {0} Error: {1}",
            //                                    validationError.PropertyName,
            //                                    validationError.ErrorMessage);
            //        }
            //    }
            //}

            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}
