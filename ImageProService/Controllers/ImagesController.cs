using ImageProService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageProService.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImagesController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var allowedContentTypes = new[] { "image/jpeg", "image/png" };
        if (!allowedContentTypes.Contains(file.ContentType))
            return BadRequest("Unsupported file type.");

        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);

        var result = await _imageService.UploadImageAsync(ms, file.FileName, file.ContentType);
        // result.AIInsights at this point is "Image is being analysed"
        // Return a message to user that image has been uploaded and will be analyzed
        return Ok(new
        {
            Message = "Image uploaded successfully. Analysis is pending.",
            ImageInfo = result
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetImage(int id)
    {
        var image = await _imageService.GetImageAsync(id);
        if (image == null) return NotFound();
        return Ok(image);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllImages()
    {
        var images = await _imageService.GetAllImagesAsync();
        return Ok(images);
    }
}