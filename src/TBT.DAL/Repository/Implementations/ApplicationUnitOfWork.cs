﻿using System.Data.Entity;
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
        private ICompanyRepository _companyRepository;

        public ApplicationUnitOfWork(IRepositoryFactory repositoryFactory, DbContext context)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
        }

        public IActivityRepository Activities => _activityRepository ?? (_activityRepository = new ActivityRepository(_context));

        public ICustomerRepository Customers => _customersRepository ?? (_customersRepository = new CustomerRepository(_context));

        public IProjectRepository Projects => _projectsRepository ?? (_projectsRepository = new ProjectRepository(_context));

        public ITimeEntryRepository TimeEntries => _timeEntriesRepository ?? (_timeEntriesRepository = new TimeEntryRepository(_context));

        public IUserRepository Users => _usersRepository ?? (_usersRepository = new UserRepository(_context));

        public IUserProjectRepository UserProjects => _userProjectRepository ?? (_userProjectRepository = new UserProjectRepository(_context));

        public IResetTicketRepository ResetTickets => _resetTicketRepository ?? (_resetTicketRepository = new ResetTicketRepository(_context));

        public ICompanyRepository Companies => _companyRepository ?? (_companyRepository = new CompanyRepository(_context));

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
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
