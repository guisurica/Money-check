using System.Text.RegularExpressions;

namespace Money.Application.Common
{
    public static class PasswordStrengthChecker
    {
        private static bool IsValidLength(string pass, out string message)
        {
            string pattern = @"^.{8,}$";

            if (!Regex.IsMatch(pass, pattern))
            {
                message = "Password is too short.";
                return false;
            }
            message = "";
            return Regex.IsMatch(pass, pattern);
        }

        private static bool HaveTheMinimumEspecialChars(string pass, out string message)
        {
            string pattern = @"^(?=.*[^\w\d]).+$";
            if (!Regex.IsMatch(pass, pattern))
            {
                message = "Password must have at least one special character";
                return false;
            }
            message = "";
            return Regex.IsMatch(pass, pattern);
        }

        private static bool HaveTheMinimumNumberIn(string pass, out string message)
        {
            string pattern = @"^(?=.*\d).+$";
            if (!Regex.IsMatch(pass, pattern))
            {
                message = "Password must have at least one number";
                return false;
            }
            message = "";
            return Regex.IsMatch(pass, pattern);
        }

        public static bool PasswordValidation(string pass, out string message)
        {
            for(int i = 0; i < 3; i++)
            {
                if (!IsValidLength(pass, out string messageReturnLength))
                {
                    message = messageReturnLength;
                    return false;
                }

                if (!HaveTheMinimumEspecialChars(pass, out string messageReturnSpecialChars))
                {
                    message = messageReturnSpecialChars;
                    return false;
                }

                if (!HaveTheMinimumNumberIn(pass, out string messageReturnNumbersIn))
                {
                    message = messageReturnNumbersIn;
                    return false;
                }
            }

            message = "";
            return true;
        }
    }
}
