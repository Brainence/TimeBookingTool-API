using TBT.Business.EmailService.Models;

namespace TBT.Business.Constants
{
    public static class StringConstants
    {
        public const string ConnectionString = "TimeBookingToolConnectionString";
        public const string LogManager = "LogManagerName";
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
                Username = "timebrainence@gmail.com",
                Password = "brainence!",
                Port = 587,
                Server = "smtp.gmail.com",
                UseSsl = true
            };
        }
    }

}
