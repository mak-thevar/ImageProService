using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProService.Domain.Interfaces
{
    public interface IImageProcessingService
    {
        (int width, int height, string format) GetImageMetadata(Stream stream);
        (MemoryStream resizedStream, int width, int height, string format) GenerateThumbnail(Stream originalStream, int thumbnailWidth);
    }
}
