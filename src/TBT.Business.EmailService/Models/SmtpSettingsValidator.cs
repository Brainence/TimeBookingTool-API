namespace TBT.Business.EmailService.Models
{
    public static class SmtpSettingsValidator
    {
        public static bool IsValid(this SmtpSettings settings)
        {
            if (string.IsNullOrEmpty(settings.Server) || string.IsNullOrEmpty(settings.Username) || string.IsNullOrEmpty(settings.Password) || settings.Port <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
