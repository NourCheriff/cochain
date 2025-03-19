
using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IProductLifeCycleDocumentRepository : IBaseDocumentRepository
    {
        Task<ProductLifeCycleDocument?> AddDocument(ProductLifeCycleDocument documentObj);
    }
}
