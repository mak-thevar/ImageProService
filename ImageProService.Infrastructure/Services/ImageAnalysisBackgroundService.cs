using ImageProService.Domain.Interfaces;
using ImageProService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProService.Infrastructure.Services
{
    public class ImageAnalysisBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ImageAnalysisBackgroundService> _logger;

        public ImageAnalysisBackgroundService(IServiceProvider serviceProvider, ILogger<ImageAnalysisBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested )
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var aiVisionService = scope.ServiceProvider.GetRequiredService<IAIVisionService>();

                    // Fetch pending images
                    var pendingImages = await dbContext.ImageMetadatas
                        .Where(img => img.AnalysisStatus == "Pending")
                        .ToListAsync(stoppingToken);

                    _logger.LogInformation("Found {Count} pending images", pendingImages.Count);

                    foreach (var image in pendingImages)
                    {
                        try
                        {
                            _logger.LogInformation("Analyzing image: {Id}", image.Id);

                            // Download the image from blob storage to memory
                            // Since we have the URL, we can fetch the image data
                            // You can also store the original image locally or re-download.
                            var httpClient = new HttpClient();
                            var imageBytes = await httpClient.GetByteArrayAsync(image.Url, stoppingToken);
                            using var imageStream = new MemoryStream(imageBytes);

                            var insights = await aiVisionService.AnalyzeImageAsync(imageStream);

                            // Update DB
                            image.AIInsights = string.IsNullOrEmpty(insights) ? "No insights found." : insights;
                            image.AnalysisStatus = "Completed";
                            await dbContext.SaveChangesAsync(stoppingToken);

                            _logger.LogInformation("Completed analysis for image: {Id}", image.Id);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error analyzing image {Id}", image.Id);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in background image analysis loop");
                }

                // Wait before next iteration
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
