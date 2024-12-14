using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ImageProService.Domain.Models
{
    public class ApiResponse
    {
        [JsonPropertyName("generated_text")]
        public string GeneratedCaption { get; set; } = string.Empty;
    }
}
