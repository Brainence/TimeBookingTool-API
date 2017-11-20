using System;

namespace TBT.Business.Managers.Interfaces
{

    public interface IManagerStore : IDisposable
    {
        IAccountManager AccountManager { get; }
        ICustomerManager CustomerManager { get; }
        IProjectManager ProjectManager { get; }
        IActivityManager ActivityManager { get; }
        ITimeEntryManager TimeEntryManager { get; }
        IUserManager UserManager { get; }
        IUserProjectManager UserProjectManager { get; }
        IResetTicketManager ResetTicketManager { get; }
        ICompanyManager CompanyManager { get; }
    }
}
