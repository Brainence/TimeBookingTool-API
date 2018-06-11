using System;
using System.Configuration;
using TBT.Business.EmailService.Models;

namespace TBT.Business.Constants
{
    public static class StringConstants
    {
        public const string ConnectionString = "TimeBookingToolConnectionString";
        public const string LogManager = "LogManagerName";
        public const string InformationLogManagerName = "TimeBookingTool_InformationLogger";
    }

    public static class NumericConstants
    {
        public const double TokenExpirationTimeInHours = 7 * 24;
    }

    public class SmtpSettingsConstants
    {
        public static readonly SmtpSettings DefaultSmtpSettings;
        static SmtpSettingsConstants()
        {
            DefaultSmtpSettings = new SmtpSettings()
            {
                Username = ConfigurationManager.AppSettings[Constants.SmtpUsername],
                Password = ConfigurationManager.AppSettings[Constants.SmtpPassword],
                Port = Convert.ToInt32(ConfigurationManager.AppSettings[Constants.SmtpPort]),
                Server = ConfigurationManager.AppSettings[Constants.SmtpServer],
                UseSsl = Convert.ToBoolean(ConfigurationManager.AppSettings[Constants.SmtpUseSsl])
            };
        }
    }

    public class Constants
    {
        public static string SmtpUsername => "SmtpUsername";
        public static string SmtpPassword => "SmtpPassword";
        public static string SmtpPort => "SmtpPort";
        public static string SmtpServer => "SmtpServer";
        public static string SmtpUseSsl => "SmtpUseSsl";
    }
    public class MailConstants
    {
        public static string FirstName => "FirstName";
        public static string LastName => "LastName";
        public static string Time => "Time";
        public static string Mesage => "Mesage";
    }
}

