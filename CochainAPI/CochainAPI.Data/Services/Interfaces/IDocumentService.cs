
using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<BaseDocument?> AddDocument(BaseDocument documentObj);
        Task<BaseDocument?> GetById(string id, string Type);
        Task<bool> DeleteById(string id, string Type);
    }
}
