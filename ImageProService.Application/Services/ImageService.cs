using ImageProService.Domain.Entities;
using ImageProService.Domain.Interfaces;
using ImageProService.Domain.Models;
using ImageProService.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProService.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IBlobStorageService _blobService;
        private readonly IImageProcessingService _imageProcessingService;
        private readonly IAIVisionService _aiVisionService;
        private readonly BlobSettings _blobSettings;

        public ImageService(
            IImageRepository imageRepository,
            IBlobStorageService blobService,
            IImageProcessingService imageProcessingService,
            IAIVisionService aiVisionService,
            IOptions<BlobSettings> blobOptions)
        {
            _imageRepository = imageRepository;
            _blobService = blobService;
            _imageProcessingService = imageProcessingService;
            _aiVisionService = aiVisionService;
            _blobSettings = blobOptions.Value;
        }

        public async Task<ImageInfoDto> UploadImageAsync(Stream fileStream, string originalFileName, string contentType)
        {
            var (width, height, format) = _imageProcessingService.GetImageMetadata(fileStream);

            var (thumbStream, thumbWidth, thumbHeight, thumbFormat) = _imageProcessingService.GenerateThumbnail(fileStream, 200);
            var originalExt = Path.GetExtension(originalFileName);
            var uniqueName = $"{Guid.NewGuid()}{originalExt}";
            var thumbName = $"thumb-{uniqueName}";

            fileStream.Position = 0;
           var fileSizeInBytes = fileStream.Length;
 
            var cdnUrl =  await _blobService.UploadAsync(fileStream, uniqueName, contentType);
            var thumbUrl = await _blobService.UploadAsync(thumbStream, thumbName, contentType);

            // Analysis not done here, just set status to Pending
            var metadata = new ImageMetadata
            {
                OriginalFileName = originalFileName,
                BlobFileName = uniqueName,
                ThumbnailBlobFileName = thumbName,
                Url = cdnUrl,
                ThumbnailUrl = thumbUrl,
                Width = width,
                Height = height,
                Format = format,
                FileSizeBytes = fileStream.Length,
                UploadedAt = DateTime.UtcNow,
                AIInsights = null,
                AnalysisStatus = "Pending"
            };

            await _imageRepository.AddAsync(metadata);
            await _imageRepository.SaveChangesAsync();

            // Return the DTO with no AIInsights yet
            return new ImageInfoDto
            {
                Id = metadata.Id,
                Url = metadata.Url,
                ThumbnailUrl = metadata.ThumbnailUrl,
                Width = metadata.Width,
                Height = metadata.Height,
                Format = metadata.Format,
                FileSizeBytes = metadata.FileSizeBytes,
                UploadedAt = metadata.UploadedAt,
                AIInsights = "Image is being analysed" // placeholder
            };
        }

        public async Task<ImageInfoDto?> GetImageAsync(int id)
        {
            var metadata = await _imageRepository.GetByIdAsync(id);
            if (metadata == null) return null;

            return new ImageInfoDto
            {
                Id = metadata.Id,
                Url = metadata.Url,
                ThumbnailUrl = metadata.ThumbnailUrl,
                Width = metadata.Width,
                Height = metadata.Height,
                Format = metadata.Format,
                FileSizeBytes = metadata.FileSizeBytes,
                UploadedAt = metadata.UploadedAt,
                AIInsights = metadata.AnalysisStatus == "Completed" ? metadata.AIInsights : "Image is being analysed"
            };
        }

        public async Task<IEnumerable<ImageInfoDto>> GetAllImagesAsync()
        {
            var all = await _imageRepository.GetAllAsync();
            return all.Select(m => new ImageInfoDto
            {
                Id = m.Id,
                Url = m.Url,
                ThumbnailUrl = m.ThumbnailUrl,
                Width = m.Width,
                Height = m.Height,
                Format = m.Format,
                FileSizeBytes = m.FileSizeBytes,
                UploadedAt = m.UploadedAt,
                AIInsights = m.AIInsights
            });
        }
    }
}
