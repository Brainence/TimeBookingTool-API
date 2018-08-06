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
using TBT.Components.Interfaces.Logger;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.Business.Managers.Implementations
{
    public class ResetTicketManager : CrudManager<ResetTicket, ResetTicketModel>, IResetTicketManager
    {
        public ResetTicketManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
            : base(unitOfWork, unitOfWork.ResetTickets, objectMapper, configurationProvider, logger)
        { }

        public async Task<bool> ChangePassword(int userId, string newPassword, string token)
        {
            newPassword = PasswordHelpers.HashPassword(newPassword);
            return await UnitOfWork.ResetTickets.ChangePasswordAsync(userId, newPassword, token);
        }

        public async Task<bool> CreateResetTicket(int userId)
        {
            var resetTicket = await UnitOfWork.ResetTickets.CreateResetTicketAsync(userId);
            if (resetTicket == null) return false;
            var emailMessage = new MailMessage
            {
                From = new MailAddress(Constants.SmtpSettingsConstants.DefaultSmtpSettings.Username),
                Subject = "Password restore",
                Priority = MailPriority.Normal,
                Body = $"Your token is: <b>{resetTicket.Token}</b><br><br>" +
                       $"Expiration date <b>{resetTicket.ExpirationDate} UTC</b>.",
                BodyEncoding = System.Text.Encoding.UTF8,
                IsBodyHtml = true
            };
            emailMessage.To.Add(new MailAddress(resetTicket.Username));
            return await ServiceLocator.Current.Get<IEmailService>().SendMailAsync(emailMessage);
        }
    }
}
