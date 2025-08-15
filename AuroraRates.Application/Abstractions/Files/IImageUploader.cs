namespace AuroraRates.Application.Abstractions.Files;

public interface IImageUploader
{
    Task<string?> UploadImageAsync(IFormFileAdapter? file, string dirToUpload = "uploads");
}