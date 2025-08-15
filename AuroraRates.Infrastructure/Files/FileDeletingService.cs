using AuroraRates.Application.Abstractions.Files;
using AuroraRates.Application.Exceptions;
using Microsoft.AspNetCore.Hosting;

namespace AuroraRates.Infrastructure.Files;

public class FileDeletingService : IFileDeletingService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileDeletingService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task DeleteFileAsync(List<string> filesToDelete)
    {
        await Task.Run(() =>
        {
            foreach (var fileToDelete in filesToDelete)
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileToDelete);
                if (!File.Exists(filePath))
                {
                    throw new NotFoundException("Files was not found");
                }
                File.Delete(filePath);
            }
        });
    }
}