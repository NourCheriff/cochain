using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.Documents;
using CochainAPI.Data.Sql.Repositories.Interfaces;
using Azure.Storage.Blobs;
using Azure.Identity;
using Azure.Storage.Blobs.Models;

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
            string fileName = $"{contract.Id}{Path.GetExtension(contract.File.FileName)}";
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = contract.File.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contract.File.ContentType });
            }

            contract.Path = blobClient.Uri.ToString();
            return await _contractRepository.AddDocument(contract);
        }

        public async Task<bool> DeleteContract(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("contracts");
            var blobClient = containerClient.GetBlobClient(fileName);

            if (!await blobClient.ExistsAsync())
            {
                return false;
            }

            await blobClient.DeleteAsync();
            return true;
        }

        public async Task<BaseDocument?> AddCertificate(SupplyChainPartnerCertificate scpCertificate)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("certificates");
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            string fileName = $"{scpCertificate.Id}{Path.GetExtension(scpCertificate.File.FileName)}";
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = scpCertificate.File.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = scpCertificate.File.ContentType });
            }

            scpCertificate.Path = blobClient.Uri.ToString();
            return await _supplyChainPartnerCertificate.AddDocument(scpCertificate);
        }

        public async Task<bool> DeleteCertificate(string fileName)
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

        public async Task<BaseDocument?> AddProductDocument(ProductLifeCycleDocument productDocument)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("prodlifecycle");
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            string fileName = $"{productDocument.Id}{Path.GetExtension(productDocument.File.FileName)}";
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = productDocument.File.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = productDocument.File.ContentType });
            }

            productDocument.Path = blobClient.Uri.ToString();
            return await _productLifeCycleRepository.AddDocument(productDocument);
        }

        public async Task<bool> DeleteProductDocument(string fileName)
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

        public async Task<bool> DeleteById(string fileName, string Type)
        {
            return Type switch
            {
                "Contract" => await DeleteContract(fileName),
                "SCPCertificate" => await DeleteCertificate(fileName),
                "ProductDocument" => await DeleteProductDocument(fileName),
                _ => false,
            };
        }
    }
}
