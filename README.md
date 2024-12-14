# ImageProService ✨🖼️
ImageProService is a cleanly-architected .NET 8 Web API that helps you upload and store images in Azure Blob Storage, produce thumbnails, and use AI-powered captioning models to understand image content. The service also serves images via CDN for faster delivery and uses a background worker to run image analysis asynchronously, so your users never have to wait for insights.

---

## Key Features 🚀
- **Asynchronous Image Captioning:**
The service uploads your images and queues them for AI analysis. You’ll receive captions that help you quickly understand what’s in an image.
- **Azure Blob & CDN Integration:**
Images and thumbnails are stored in Azure Blob Storage. Access is improved with a CDN for quick global delivery.
- **Background Analysis:**
No blocking calls—upload first, then let the background worker handle AI insights so the user can get back to work.
- **Clean Architecture & SOLID Principles:**
Clear separation of concerns and well-structured code make this solution easy to maintain and scale.

---

## Why Image Captioning? 🤔
Image captioning uses machine learning models to generate short descriptions that summarize an image. This is like a human quickly describing what they see at a glance. For example, if you have a product image, the caption might say, "A red chair in a living room," which helps you understand the image’s content without opening it.

#### Pros of Image Captioning:

- **Faster Content Understanding:**
You get the main idea of what’s inside the picture without spending time interpreting complex visuals.
- **Improved Accessibility:**
Captions help people with visual impairments understand images better through screen readers
- **Better Search & Tagging:**
Rich captions act as metadata, making it simpler to find specific images based on what's shown (e.g., "cat on a couch").
- **Enhanced User Experience:**
Users can browse image libraries more efficiently if they know what each image shows right away.
---

## Project Structure 🗂️
```
ImageProService
 ├─ src
 │   ├─ ImageProService.Domain
 │   │   ├─ Entities
 │   │   │   └─ ImageMetadata.cs
 │   │   ├─ Interfaces
 │   │   │   └─ IAIVisionService.cs
 │   │   │   └─ IBlobStorageService.cs
 │   │   │   └─ IImageProcessingService.cs
 │   │   │   └─ IImageRepository.cs
 │   │   ├─ Models
 │   │   │   └─ ApiResponse.cs
 │   │   │   └─ ImageInfoDto.cs
 │   │   └─ Enums
 │   ├─ ImageProService.Application
 │   │   └─ Services
 │   │   │   ├─ IImageService.cs
 │   │   │   ├─ ImageService.cs
 │   ├─ ImageProService.Infrastructure
 │   │   ├─ Configurations
 │   │   │   ├─ BlobSettings.cs
 │   │   │   ├─ HuggingFaceSettings.cs
 │   │   ├─ Data
 │   │   │   ├─ AppDbContext.cs
 │   │   │   ├─ EfImageRepository.cs
 │   │   │   └─ ImageMetadataConfiguration.cs
 │   │   ├─ Services
 │   │   │   ├─ BlobStorageService.cs
 │   │   │   ├─ ImageProcessingService.cs
 │   │   │   ├─ AIVisionService.cs
 │   │   │   └─ ImageAnalysisBackgroundService.cs
 │   └─ ImageProService.Api
 │       ├─ Controllers
 │       │   └─ ImagesController.cs
 │       ├─ DB
 │       ├─ Program.cs
 │       └─ appsettings.json
```

## Getting Started 🏁
1. **Prerequisites:**
   - .NET 8 SDK
   - Azure Storage Account and Blob Container
   - Hugging Face API Key for the chosen model
   - Azure CDN configured for the blob endpoint
   - SQL Database connection
2. **Configuration:**
Update appsettings.json with your connection strings, blob settings, CDN endpoint, and Hugging Face credentials
3. **Build & Run:**
    ```bash
    dotnet build
    dotnet ef database update -s .\ImageProService -p .\ImageProService.Infrastructure #from the root directory
    dotnet run --project .\ImageProService
    ```
4. **Usage:**
    - Upload Image: POST /api/images/upload with form-data containing file
    - Get Image Info: GET /api/images/{id}
    
When you upload an image, the response will mention that analysis is pending. After the background service processes it, you can retrieve the image info again and see the generated caption.

---
## License 📜
This project is licensed under the [MIT License](https://github.com/mak-thevar/ImageProService/blob/master/LICENSE.txt).

---
## Contact
* **Website:** https://mak-thevar.dev
* **Project Link:** https://github.com/mak-thevar/ImageProService
* **Blog:** https://blog.mak-thevar.dev
* **LinkedIn:**: https://www.linkedin.com/in/mak11/
