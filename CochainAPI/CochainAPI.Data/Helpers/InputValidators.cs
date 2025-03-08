using System.Net.Mail;

namespace CochainAPI.Data.Helpers
{
    public static class InputValidators
    {
        public static bool IsValidEmail(this string email)
        {
            return MailAddress.TryCreate(email, out var emailresult);
        }
    }
}
