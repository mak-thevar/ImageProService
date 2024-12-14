using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ImageProService.Domain.Interfaces;
using ImageProService.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProService.Infrastructure.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobSettings _blobSettings;

        public BlobStorageService(IOptions<BlobSettings> options)
        {
            _blobSettings = options.Value;
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            var blobClient = new BlobContainerClient(_blobSettings.ConnectionString, _blobSettings.ContainerName);
            await blobClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blob = blobClient.GetBlobClient(fileName);
            var blobHeaders = new BlobHttpHeaders { ContentType = contentType };

            // Reset stream position if needed
            fileStream.Position = 0;
            await blob.UploadAsync(fileStream, new BlobUploadOptions { HttpHeaders = blobHeaders });

            return blob.GenerateSasUri(Azure.Storage.Sas.BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddDays(2)).ToString();
        }
    }

}
