using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProService.Domain.Models
{
    public class ImageInfoDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
        public string Format { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public DateTime UploadedAt { get; set; }
        public string? AIInsights { get; set; }
    }
}
