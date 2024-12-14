using ImageProService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProService.Domain.Interfaces
{
    public interface IImageRepository
    {
        Task<ImageMetadata?> GetByIdAsync(int id);
        Task<IEnumerable<ImageMetadata>> GetAllAsync();
        Task AddAsync(ImageMetadata metadata);
        Task SaveChangesAsync();
    }
}
