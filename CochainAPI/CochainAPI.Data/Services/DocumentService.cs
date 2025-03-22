using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using Azure.Storage.Blobs;
using Azure.Identity;
using Azure.Storage.Blobs.Models;
using CochainAPI.Model.Authentication;
using CochainAPI.Model.Helper;

namespace CochainAPI.Data.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IContractRepository _contractRepository;
        private readonly ISupplyChainPartnerCertificateRepository _supplyChainPartnerCertificate;
        private readonly IProductLifeCycleDocumentRepository _productLifeCycleRepository;

        public DocumentService(IContractRepository contractRepository, ISupplyChainPartnerCertificateRepository supplyChainPartnerCertificateRepository, IProductLifeCycleDocumentRepository productLifeCycleDocumentRepository)
        {
            _contractRepository = contractRepository;
            _productLifeCycleRepository = productLifeCycleDocumentRepository;
            _supplyChainPartnerCertificate = supplyChainPartnerCertificateRepository;
            string blobAccountUrl = "https://teststoragedocum.blob.core.windows.net";
            _blobServiceClient = new BlobServiceClient(new Uri(blobAccountUrl), new DefaultAzureCredential());
        }
        public async Task<BaseDocument?> AddDocument(BaseDocument documentObj)
        {
            documentObj.Id = Guid.NewGuid();
            return documentObj switch
            {
                Contract contract => await AddContract(contract),
                SupplyChainPartnerCertificate scpCertificate => await AddCertificate(scpCertificate),
                ProductLifeCycleDocument productDocument => await AddProductDocument(productDocument),
                _ => null,
            };
        }

        public async Task<BaseDocument?> AddContract(Contract contract)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("contracts");
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            string fileName = $"{contract.Id}.pdf";
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = new MemoryStream(contract.File))
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = "application/pdf" });
            }

            contract.Path = blobClient.Uri.ToString();
            return await _contractRepository.AddDocument(contract);
        }

        public async Task<bool> DeleteContract(Guid id, string fileName)
        {
            if (Guid.TryParse(id.ToString(), out var docId))
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient("contracts");
                var blobClient = containerClient.GetBlobClient(fileName);

                if (!await blobClient.ExistsAsync())
                {
                    return false;
                }

                await blobClient.DeleteAsync();
                await _contractRepository.DeleteDocumentById(docId);
                return true;
            }
            return false;
        }

        public async Task<BaseDocument?> AddCertificate(SupplyChainPartnerCertificate scpCertificate)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("certificates");
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            string fileName = $"{scpCertificate.Id}.pdf";
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = new MemoryStream(scpCertificate.File))
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = "application/pdf" });
            }

            scpCertificate.Path = blobClient.Uri.ToString();
            return await _supplyChainPartnerCertificate.AddDocument(scpCertificate);
        }

        public async Task<bool> DeleteCertificate(Guid id, string fileName)
        {
            if (Guid.TryParse(id.ToString(), out var docId))
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient("certificates");
                var blobClient = containerClient.GetBlobClient(fileName);

                if (!await blobClient.ExistsAsync())
                {
                    return false;
                }

                await blobClient.DeleteAsync();
                return true;
            }
            return false;
        }

        public async Task<BaseDocument?> AddProductDocument(ProductLifeCycleDocument productDocument)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("prodlifecycle");
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            string fileName = $"{productDocument.Id}.pdf";
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = new MemoryStream(productDocument.File))
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = "application/pdf" });
            }

            productDocument.Path = blobClient.Uri.ToString();
            return await _productLifeCycleRepository.AddDocument(productDocument);
        }

        public async Task<bool> DeleteProductDocument(Guid id, string fileName)
        {
            if (Guid.TryParse(id.ToString(), out var docId))
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient("prodlifecycle");
                var blobClient = containerClient.GetBlobClient(fileName);

                if (!await blobClient.ExistsAsync())
                {
                    return false;
                }

                await blobClient.DeleteAsync();
                return true;
            }
            return false;
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

        public async Task<bool> DeleteById(Guid id, string filename, string Type)
        {
            return Type switch
            {
                "Contract" => await DeleteContract(id, filename),
                "ProductDocument" => await DeleteProductDocument(id, filename),
                _ => false,
            };
        }

        public async Task<bool> DeleteCertificateById(Guid id, string filename, string Type)
        {
            return Type switch
            {
                "SCPCertificate" => await DeleteCertificate(id, filename),
                _ => false,
            };
        }

        public async Task<Page<SupplyChainPartnerCertificate>> GetSustainabilityCertificates(string? queryParam, int? pageNumber, int? pageSize)
        {
            return await _supplyChainPartnerCertificate.GetSustainabilityCertificates(queryParam, pageNumber, pageSize);
        }

        public async Task<Page<Contract>?> GetEmittedContracts(string userId, string? queryParam, int? pageNumber, int? pageSize)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await _contractRepository.GetEmittedContracts(userId, queryParam, pageNumber, pageSize);
            }

            return null;
        }

        public async Task<Page<Contract>?> GetReceivedContracts(string scpId, string? queryParam, int? pageNumber, int? pageSize)
        {
            if (Guid.TryParse(scpId, out var id))
            {
                return await _contractRepository.GetReceivedContracts(id, queryParam, pageNumber, pageSize);
            }

            return null;
        }
    }
}