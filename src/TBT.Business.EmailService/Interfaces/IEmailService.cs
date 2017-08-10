using System.Net.Mail;
using System.Threading.Tasks;
using TBT.Business.EmailService.Models;

namespace TBT.Business.EmailService.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendMailAsync(MailMessage emailMessage);
        Task<bool> SendMailAsync(MailMessage mailMessage, SmtpSettings smtpSettings);
    }
}
