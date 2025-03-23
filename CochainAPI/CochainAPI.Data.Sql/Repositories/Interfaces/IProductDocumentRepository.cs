using CochainAPI.Model.Documents;

namespace CochainAPI.Data.Sql.Repositories.Interfaces
{
    public interface IProductDocumentRepository : IBaseDocumentRepository
    {
        Task<ProductDocument?> AddDocument(ProductDocument documentObj);
    }
}
