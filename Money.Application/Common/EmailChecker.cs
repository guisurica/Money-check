using System.Net.Mail;

namespace Money.Application.Common
{
    public static class EmailChecker
    {
        public static bool Check(string email)
        {
            var valid = MailAddress.TryCreate(email, out MailAddress mail);
            if (valid) 
            { 
                return true;
            }

            return false;
        }
    }
}
