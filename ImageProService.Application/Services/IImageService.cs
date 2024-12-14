using ImageProService.Domain.Models;

namespace ImageProService.Application.Services
{
    public interface IImageService
    {
        Task<IEnumerable<ImageInfoDto>> GetAllImagesAsync();
        Task<ImageInfoDto?> GetImageAsync(int id);
        Task<ImageInfoDto> UploadImageAsync(Stream fileStream, string originalFileName, string contentType);
    }
}