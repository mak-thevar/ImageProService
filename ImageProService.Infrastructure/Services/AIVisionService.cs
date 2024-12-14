using Azure;
using ImageProService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Azure.AI.Vision.ImageAnalysis;
using ImageProService.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using ImageProService.Domain.Models;
namespace ImageProService.Infrastructure.Services
{
    public class AIVisionService : IAIVisionService
    {
        private readonly HuggingFaceSettings _settings;
        private readonly HttpClient _httpClient;

        public AIVisionService(IOptions<HuggingFaceSettings> options, HttpClient httpClient)
        {
            _settings = options.Value;
            _httpClient = httpClient;

            if (!string.IsNullOrEmpty(_settings.AccessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.AccessToken);
            }
        }

        public async Task<string> AnalyzeImageAsync(Stream imageStream)
        {
            if (string.IsNullOrEmpty(_settings.AccessToken) || string.IsNullOrEmpty(_settings.ModelUrl))
                return string.Empty;

            // Reset stream and create StreamContent
            imageStream.Position = 0;
            using var content = new StreamContent(imageStream);
            content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg"); // or detect MIME based on the image

            var response = await _httpClient.PostAsync(_settings.ModelUrl, content);
            if (!response.IsSuccessStatusCode) return string.Empty;

            var result = await response.Content.ReadFromJsonAsync<List<ApiResponse>>();
            return result?[0]?.GeneratedCaption ?? string.Empty;
        }
    }
}
