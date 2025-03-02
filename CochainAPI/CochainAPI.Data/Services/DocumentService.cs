
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Data.Sql.Repositories.Interfaces;

namespace CochainAPI.Data.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentService _documentService;
        private readonly IBaseDocumentRepository _documentRepository;
        private readonly IContractRepository _contractRepository;
        private readonly ISupplyChainPartnerCertificateRepository _supplyChainPartnerCertificate;
        private readonly IProductLifeCycleDocumentRepository _productLifeCycleRepository;

        public DocumentService(IDocumentService documentService, IBaseDocumentRepository documentRepository)
        {
            _documentService = documentService;
            _documentRepository = documentRepository;
        }
        public async Task<BaseDocument?> AddDocument(BaseDocument documentObj)
        {
            switch (documentObj.Type)
            {
                case "Contract":
                    return await _contractRepository.AddDocument(documentObj);
                case "SCPCertificate":
                    return await _supplyChainPartnerCertificate.AddDocument(documentObj);
                case "ProductDocument":
                    return await _productLifeCycleRepository.AddDocument(documentObj);
            }
        }
        public async Task<BaseDocument?> GetById(string id)
        {
            return await _documentRepository.GetById(id);
        }
    }
}
