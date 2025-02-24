namespace CochainAPI.Data.Services.Interfaces
{
    public interface IEmailService
    {
        void EmailPasswordTemporanea(string email, string tempPassword);
    }
}
