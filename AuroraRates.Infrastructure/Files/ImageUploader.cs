using AuroraRates.Application.Abstractions.Files;
using Microsoft.AspNetCore.Hosting;

namespace AuroraRates.Infrastructure.Files;

public class ImageUploader : IImageUploader
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageUploader(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    
    public async Task<string?> UploadImageAsync(IFormFileAdapter? file, string dirToUpload = "uploads")
    {
        string? filePath = null;
        string? fileName = null;
        if (file != null)
        {
            try
            {
                var uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, dirToUpload);
                Directory.CreateDirectory(uploadsDir);
            
                fileName =  Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(uploadsDir, fileName);
            }
            catch (NullReferenceException ex)
            {
                throw new NullReferenceException("No web root folder was configured(such as wwwroot)", ex);
            }
            using (var stream =  new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            filePath = $"{dirToUpload}/{fileName}";
        }
        return filePath;
    }
}