using TBT.Business.EmailService.Interfaces;
using TBT.Business.EmailService.Models;
using TBT.Components.Interfaces.Logger;

namespace TBT.Business.EmailService.Implementations
{
    public class EmailService : BaseEmailSevice, IEmailService
    {
        public EmailService(ILogManager logManager, SmtpSettings smtpSettings)
            : base(logManager, smtpSettings)
        { }
    }
}
