using Microsoft.AspNet.Identity;
using System;

namespace TBT.Business.Helpers
{
    public class PasswordHelpers
    {
        public static bool ValidatePassword(string password)
        {
            var validator = new PasswordValidator()
            {
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
                RequiredLength = 8
            };
            var validationResult = validator.ValidateAsync(password);
            validationResult.Wait();
            if (validationResult.Result != IdentityResult.Success)
            {
                throw new Exception("Password must at least 8 characters and contains at least one uppercase character one lowercase character and one number");
            }
            return true;
        }

        public static string HashPassword(string password)
        {
            return new PasswordHasher().HashPassword(password);
        }

        public static bool VerifyPassword(string hashedPassword, string password)
        {
            return new PasswordHasher().VerifyHashedPassword(hashedPassword, password) == PasswordVerificationResult.Success;
        }
    }
}