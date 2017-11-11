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
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                logManager.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\r\nObjectType: {this.GetType()}\r\nMethod: {x.ReflectedType.Name}\r\nParameter: {mailMessage.ToString()}");
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
