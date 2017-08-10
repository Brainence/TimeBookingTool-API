using System.Globalization;
using System.Net.Mail;
using System.Threading.Tasks;
using TBT.Business.EmailService.Interfaces;
using TBT.Business.Helpers;
using TBT.Business.Implementations;
using TBT.Business.Infrastructure.CastleWindsor;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.Business.Managers.Implementations
{
    public class ResetTicketManager : CrudManager<ResetTicket, ResetTicketModel>, IResetTicketManager
    {
        public ResetTicketManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider)
            : base(unitOfWork, unitOfWork.ResetTickets, objectMapper, configurationProvider)
        { }

        public async Task<bool> ChangePassword(int userId, string newPassword, string token)
        {
            newPassword = PasswordHelpers.HashPassword(newPassword);
            return await UnitOfWork.ResetTickets.ChangePassword(userId, newPassword, token);
        }

        public async Task<bool> CreateResetTicket(int userId)
        {
            var resetTicket = await UnitOfWork.ResetTickets.CreateResetTicket(userId);
            if (resetTicket == null) return false;

            var emailService = ServiceLocator.Current.Get<IEmailService>();

            var emailMessage = new MailMessage();

            emailMessage.From = new MailAddress(Constants.SmtpSettingsConstants.DefaultSmtpSettings.Username);
            emailMessage.To.Add(new MailAddress(resetTicket.Username));
            emailMessage.Subject = "Password restore";
            emailMessage.Body = $"";
            emailMessage.Priority = MailPriority.Normal;
            emailMessage.Body =
                $"Your token is: <b>{resetTicket.Token}</b><br><br>" +
                $"Expiration date <b>{resetTicket.ExpirationDate.ToString()} UTC</b>.";
            emailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            emailMessage.IsBodyHtml = true;

            var succeed = await emailService.SendMailAsync(emailMessage);

            return succeed;
        }
    }
}
