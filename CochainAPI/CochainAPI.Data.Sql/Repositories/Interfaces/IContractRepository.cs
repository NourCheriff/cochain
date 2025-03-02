
using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IContractRepository : IBaseDocumentRepository
    {
        Task<Contract?> AddDocument(Contract documentObj);
    }
}
