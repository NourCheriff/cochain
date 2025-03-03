
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
            return documentObj switch
            {
                Contract contract => await _contractRepository.AddDocument(contract),
                SupplyChainPartnerCertificate scpCertificate => await _supplyChainPartnerCertificate.AddDocument(scpCertificate),
                ProductLifeCycleDocument productDocument => await _productLifeCycleRepository.AddDocument(productDocument),
                _ => null,
            };
        }
        public async Task<BaseDocument?> GetById(string id, string Type)
        {
            return Type switch
            {
                "Contract" => await _contractRepository.GetById(id),
                "SCPCertificate" => await _supplyChainPartnerCertificate.GetById(id),
                "ProductDocument" => await _productLifeCycleRepository.GetById(id),
                _ => null,
            };
        }
    }
}
