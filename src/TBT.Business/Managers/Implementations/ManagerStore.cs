using System;
using System.Linq;
using System.Reflection;
using TBT.Business.Factories.Interfaces;
using TBT.Business.Managers.Interfaces;

namespace TBT.Business.Managers.Implementations
{
    public class ManagerStore : IManagerStore
    {
        #region Fields

        private IManagerFactory _managerFactory;

        private IAccountManager _accountManager;
        private ICustomerManager _customerManager;
        private IProjectManager _projectManager;
        private IActivityManager _taskManager;
        private ITimeEntryManager _timeEntryManager;
        private IUserManager _userManager;
        private IUserProjectManager _userProjectManager;
        private IResetTicketManager _resetTicketManager;
        private ICompanyManager _companyManager;

        #endregion

        #region Properties


        public ICustomerManager CustomerManager => _customerManager ?? (_customerManager = _managerFactory.Create<ICustomerManager>(typeof(ICustomerManager).FullName));

        public IProjectManager ProjectManager => _projectManager ?? (_projectManager = _managerFactory.Create<IProjectManager>(typeof(IProjectManager).FullName));

        public IActivityManager ActivityManager => _taskManager ?? (_taskManager = _managerFactory.Create<IActivityManager>(typeof(IActivityManager).FullName));

        public ITimeEntryManager TimeEntryManager => _timeEntryManager ?? (_timeEntryManager = _managerFactory.Create<ITimeEntryManager>(typeof(ITimeEntryManager).FullName));

        public IUserManager UserManager => _userManager ?? (_userManager = _managerFactory.Create<IUserManager>(typeof(IUserManager).FullName));

        public IUserProjectManager UserProjectManager => _userProjectManager ?? (_userProjectManager = _managerFactory.Create<IUserProjectManager>(typeof(IUserProjectManager).FullName));

        public IAccountManager AccountManager => _accountManager ?? (_accountManager = _managerFactory.Create<IAccountManager>(typeof(IAccountManager).FullName));

        public IResetTicketManager ResetTicketManager => _resetTicketManager ?? (_resetTicketManager = _managerFactory.Create<IResetTicketManager>(typeof(IResetTicketManager).FullName));

        public ICompanyManager CompanyManager => _companyManager ?? (_companyManager = _managerFactory.Create<ICompanyManager>(typeof(ICompanyManager).FullName));

        #endregion

        #region Constructors

        public ManagerStore(IManagerFactory managerFactory)
        {
            _managerFactory = managerFactory;
        }

        #endregion

        #region Interface Members

        public void Dispose()
        {
            foreach (var field in GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(m => m.FieldType != typeof(IManagerFactory)))
            {
                var value = field.GetValue(this) as IDisposable;

                if (value != null)
                {
                    value.Dispose();
                    field.SetValue(this, null);
                }
            }
        }

        #endregion
    }
}
