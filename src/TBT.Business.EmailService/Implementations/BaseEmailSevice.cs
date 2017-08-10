using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TBT.Business.EmailService.Models;
using TBT.Components.Interfaces.Logger;

namespace TBT.Business.EmailService.Implementations
{
    public abstract class BaseEmailSevice : IDisposable
    {
        #region Fields

        private ILogManager logManager;
        private SmtpClient smtpClient;
        private SmtpSettings smtpSettings;

        #endregion

        #region Properties

        protected ILogManager LogManager
        {
            get { return logManager; }
        }

        protected SmtpClient SmtpClient
        {
            get { return smtpClient; }
        }

        protected SmtpSettings SmtpSettings
        {
            get { return smtpSettings; }
        }

        #endregion

        #region Constructors

        protected BaseEmailSevice(ILogManager logManager, SmtpSettings smtpSettings)
        {
            this.logManager = logManager;
            this.smtpClient = new SmtpClient();
            this.SetSmtpSettings(smtpSettings);
        }

        #endregion

        #region Methods

        public virtual async Task<bool> SendMailAsync(MailMessage mailMessage)
        {
            try
            {
                await this.smtpClient.SendMailAsync(mailMessage);

                return true;
            }
            catch (Exception exception)
            {
                this.logManager.Error(exception, exception.InnerException?.Message ?? exception.Message);

                return false;
            }
        }

        public virtual Task<bool> SendMailAsync(MailMessage mailMessage, SmtpSettings smtpSettings)
        {
            this.SetSmtpSettings(smtpSettings);

            return this.SendMailAsync(mailMessage);
        }

        protected void SetSmtpSettings(SmtpSettings smtpSettings)
        {
            if (smtpSettings.IsValid())
            {
                this.smtpClient.Host = smtpSettings.Server;
                this.smtpClient.Port = smtpSettings.Port;
                this.smtpClient.EnableSsl = smtpSettings.UseSsl;
                this.smtpClient.Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password);
                this.smtpSettings = smtpSettings;
            }
        }

        public void Dispose()
        {
            if (this.smtpClient != null)
            {
                this.smtpClient.Dispose();
                this.smtpClient = null;
            }
        }

        #endregion
    }
}
