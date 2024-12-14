using ImageProService.Domain.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;

namespace ImageProService.Infrastructure.Services
{
    public class ImageProcessingService : IImageProcessingService
    {
        public (int width, int height, string format) GetImageMetadata(Stream stream)
        {
            stream.Position = 0;
            using var image = Image.Load(stream);
            //var format = Image.DetectFormat(stream);
            return (image.Width, image.Height, image.Metadata.DecodedImageFormat.DefaultMimeType);
        }

        public (MemoryStream resizedStream, int width, int height, string format) GenerateThumbnail(Stream originalStream, int thumbnailWidth)
        {
            originalStream.Position = 0;
            using var image = Image.Load(originalStream);
            //var format = Image.DetectFormat(originalStream);

            var aspectRatio = (double)image.Height / image.Width;
            var thumbnailHeight = (int)(thumbnailWidth * aspectRatio);

            image.Mutate(x => x.Resize(thumbnailWidth, thumbnailHeight));
            var ms = new MemoryStream();
            image.Save(ms, SixLabors.ImageSharp.Formats.Png.PngFormat.Instance);
            ms.Position = 0;

            return (ms, thumbnailWidth, thumbnailHeight, image.Metadata.DecodedImageFormat.DefaultMimeType);
        }
    }

}
