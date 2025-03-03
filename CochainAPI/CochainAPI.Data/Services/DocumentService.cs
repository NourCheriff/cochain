
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Data.Sql.Repositories.Interfaces;

namespace CochainAPI.Data.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IContractRepository _contractRepository;
        private readonly ISupplyChainPartnerCertificateRepository _supplyChainPartnerCertificate;
        private readonly IProductLifeCycleDocumentRepository _productLifeCycleRepository;

        public DocumentService(IContractRepository contractRepository, ISupplyChainPartnerCertificateRepository supplyChainPartnerCertificateRepository, IProductLifeCycleDocumentRepository productLifeCycleDocumentRepository)
        {
            _contractRepository = contractRepository;
            _productLifeCycleRepository = productLifeCycleDocumentRepository;
            _supplyChainPartnerCertificate = supplyChainPartnerCertificateRepository;
        }
        public async Task<BaseDocument?> AddDocument(BaseDocument documentObj)
        {
            switch (documentObj)
            {
                case Contract contract:
                    return await _contractRepository.AddDocument(contract);
                case SupplyChainPartnerCertificate scpCertificate:
                    return await _supplyChainPartnerCertificate.AddDocument(scpCertificate);
                case ProductLifeCycleDocument productDocument:
                    return await _productLifeCycleRepository.AddDocument(productDocument);
            }
            return null;
        }
        public async Task<BaseDocument?> GetById(string id, string Type)
        {
            switch (Type)
            {
                case "Contract":
                    return await _contractRepository.GetById(id);
                case "SCPCertificate":
                    return await _supplyChainPartnerCertificate.GetById(id);
                case "ProductDocument":
                    return await _productLifeCycleRepository.GetById(id);
            }
            return null;
        }
    }
}
