namespace CochainAPI.Data.Services.Interfaces
{
    public interface IEmailService
    {
        void SendTemporaryPassword(string email, string tempPassword);
    }
}
