using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProService.Domain.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);
    }
}
