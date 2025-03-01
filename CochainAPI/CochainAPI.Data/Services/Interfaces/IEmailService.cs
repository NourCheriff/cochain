namespace CochainAPI.Data.Services.Interfaces
{
    public interface IEmailService
    {
        Task EmailPasswordTemporanea(string email, string tempPassword);
    }
}
