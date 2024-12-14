namespace ImageProService.Domain.Entities;

public class ImageMetadata
{
    public int Id { get; set; }
    public string OriginalFileName { get; set; } = string.Empty;
    public string BlobFileName { get; set; } = string.Empty;
    public string ThumbnailBlobFileName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public int Width { get; set; }
    public int Height { get; set; }
    public string Format { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public string? AIInsights { get; set; }

    public string AnalysisStatus { get; set; } = "Pending"; // "Pending" or "Completed"
}

