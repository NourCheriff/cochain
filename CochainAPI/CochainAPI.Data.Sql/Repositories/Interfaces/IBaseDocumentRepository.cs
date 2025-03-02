
using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IBaseDocumentRepository
    {
        Task<BaseDocument?> GetById(string id);
    }
}
