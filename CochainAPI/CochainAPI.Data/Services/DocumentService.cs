using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CochainAPI.Model.Helper;

namespace CochainAPI.Data.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IContractRepository _contractRepository;
        private readonly ISupplyChainPartnerCertificateRepository _supplyChainPartnerCertificate;
        private readonly IProductLifeCycleDocumentRepository _productLifeCycleRepository;
        private readonly IProductDocumentRepository _productDocumentRepository;
        private readonly IUserRepository _userRepository;

        public DocumentService(IContractRepository contractRepository, ISupplyChainPartnerCertificateRepository supplyChainPartnerCertificateRepository, IProductLifeCycleDocumentRepository productLifeCycleDocumentRepository, IProductDocumentRepository productDocumentRepository, IUserRepository userRepository)
        {
            _contractRepository = contractRepository;
            _productLifeCycleRepository = productLifeCycleDocumentRepository;
            _supplyChainPartnerCertificate = supplyChainPartnerCertificateRepository;
            _productDocumentRepository = productDocumentRepository;
            _userRepository = userRepository;
            //string blobAccountUrl = "https://teststoragedocum.blob.core.windows.net";
            //_blobServiceClient = new BlobServiceClient(new Uri(blobAccountUrl), new DefaultAzureCredential());
            _blobServiceClient = new BlobServiceClient(Environment.GetEnvironmentVariable("BLOB_STORAGE_SECRET"));
        }
        public async Task<BaseDocument?> AddDocument(BaseDocument documentObj)
        {
            documentObj.Id = Guid.NewGuid();
            return documentObj switch
            {
                Contract contract => await AddContract(contract),
                ProductLifeCycleDocument productLifeCycleDocument => await AddProductLifeCycleDocument(productLifeCycleDocument),
                ProductDocument productDocument => await AddProductDocument(productDocument),
                _ => null,
            };
        }
        public async Task<BaseDocument?> AddCertificate(BaseDocument documentObj)
        {
            documentObj.Id = Guid.NewGuid();
            return documentObj switch
            {
                SupplyChainPartnerCertificate scpCertificate => await AddCertificate(scpCertificate),               
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
                await _supplyChainPartnerCertificate.DeleteDocumentById(docId);
                return true;
            }
            return false;
        }

        public async Task<BaseDocument?> AddProductLifeCycleDocument(ProductLifeCycleDocument productDocument)
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

        public async Task<bool> DeleteProductLifeDocument(Guid id, string fileName)
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
                await _productLifeCycleRepository.DeleteDocumentById(docId);
                return true;
            }
            return false;
        }

        public async Task<BaseDocument?> AddProductDocument(ProductDocument productDocument)
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
            return await _productDocumentRepository.AddDocument(productDocument);
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
                await _productDocumentRepository.DeleteDocumentById(docId);
                return true;
            }
            return false;
        }

        public async Task<BaseDocument?> GetById(string id, string Type)
        {
            return Type switch
            {
                "contract" => await _contractRepository.GetById(id),
                "sustainability" => await _supplyChainPartnerCertificate.GetById(id),
                "invoice" or "transport" => await _productLifeCycleRepository.GetById(id),
                "origin" or "quality" => await _productDocumentRepository.GetById(id),
                _ => null,
            };
        }

        public async Task<bool> DeleteById(Guid id, string filename, string Type)
        {
            return Type switch
            {
                "contract" => await DeleteContract(id, filename),
                "invoice" or "transport" => await DeleteProductLifeDocument(id, filename),
                "origin" or "quality" => await DeleteProductDocument(id, filename),
                _ => false,
            };
        }

        public async Task<bool> DeleteCertificateById(Guid id, string filename, string Type)
        {
            return Type switch
            {
                "sustainability" => await DeleteCertificate(id, filename),
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

        public async Task<Page<Contract>?> GetReceivedContracts(string userId, string? queryParam, int? pageNumber, int? pageSize)
        {
            if (Guid.TryParse(userId, out var id))
            {
                var user = await _userRepository.GetById(id.ToString());
                var scpId = user!.SupplyChainPartnerId.GetValueOrDefault();
                if (string.IsNullOrEmpty(scpId.ToString()))
                {
                    return null;
                }
                return await _contractRepository.GetReceivedContracts(scpId, queryParam, pageNumber, pageSize);
            }

            return null;
        }
    }
}