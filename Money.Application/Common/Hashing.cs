using Microsoft.AspNetCore.Identity;

namespace Money.Application.Common
{
    public static class Hashing<TUser> where TUser : class
    {
        public static string HashPassword(TUser user, string password)
        {
           PasswordHasher<TUser> passwordHasher = new PasswordHasher<TUser>();
           var pass = passwordHasher.HashPassword(user, password);
           return pass;
        }

        public static PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            PasswordHasher<TUser> passwordHasher = new PasswordHasher<TUser>();
            var isValid = passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return isValid;
        }
    }
}
