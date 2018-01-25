using System;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using TBT.Business.EmailService.Models;
using TBT.Components.Interfaces.Logger;

namespace TBT.Business.EmailService.Implementations
{
    public abstract class BaseEmailSevice : IDisposable
    {
        #region Fields

        private ILogManager _logManager;
        private SmtpClient _smtpClient;
        private SmtpSettings _smtpSettings;

        #endregion

        #region Properties

        protected ILogManager LogManager => _logManager;

        protected SmtpClient SmtpClient => _smtpClient;

        protected SmtpSettings SmtpSettings => _smtpSettings;

        #endregion

        #region Constructors

        protected BaseEmailSevice(ILogManager logManager, SmtpSettings smtpSettings)
        {
            _logManager = logManager;
            _smtpClient = new SmtpClient();
            SetSmtpSettings(smtpSettings);
        }

        #endregion

        #region Methods

        public virtual async Task<bool> SendMailAsync(MailMessage mailMessage)
        {
            try
            {
                await _smtpClient.SendMailAsync(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                _logManager.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameter: {mailMessage}");
                return false;
            }
        }

        public virtual Task<bool> SendMailAsync(MailMessage mailMessage, SmtpSettings smtpSettings)
        {
            SetSmtpSettings(smtpSettings);

            return SendMailAsync(mailMessage);
        }

        protected void SetSmtpSettings(SmtpSettings smtpSettings)
        {
            if (smtpSettings.IsValid())
            {
                _smtpClient.Host = smtpSettings.Server;
                _smtpClient.Port = smtpSettings.Port;
                _smtpClient.EnableSsl = smtpSettings.UseSsl;
                _smtpClient.Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password);
                _smtpSettings = smtpSettings;
            }
        }

        public void Dispose()
        {
            if (_smtpClient != null)
            {
                _smtpClient.Dispose();
                _smtpClient = null;
            }
        }

        #endregion
    }
}
