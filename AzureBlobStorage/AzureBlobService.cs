using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace EventEase_Booking_System.AzureBlobStorage
{
    public class AzureBlobService: IBlobService
    {
        private readonly string _connectionString;
        private readonly string _containerName;


        public AzureBlobService(IConfiguration config)
        {
            _connectionString = config["AzureStorage:ConnectionString"];
            _containerName = config["AzureStorage:ContainerName"];
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            var blobContainerClient = new BlobContainerClient(_connectionString, _containerName);
            await blobContainerClient.CreateIfNotExistsAsync();

            var blobClient = blobContainerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);

            return blobClient.Uri.ToString();
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            var blobContainerClient = new BlobContainerClient(_connectionString, _containerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            var response = await blobClient.DeleteIfExistsAsync();
            return response.Value;
        }

    }
}
