using ImageProService.Domain.Entities;
using ImageProService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProService.Infrastructure.Data
{
    public class EfImageRepository : IImageRepository
    {
        private readonly AppDbContext _context;
        public EfImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ImageMetadata metadata)
        {
            await _context.ImageMetadatas.AddAsync(metadata);
        }

        public async Task<ImageMetadata?> GetByIdAsync(int id)
        {
            return await _context.ImageMetadatas.FindAsync(id);
        }

        public async Task<IEnumerable<ImageMetadata>> GetAllAsync()
        {
            return await _context.ImageMetadatas.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
