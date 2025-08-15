namespace AuroraRates.Application.Abstractions.Files;

public interface IFileDeletingService
{
    Task DeleteFileAsync(List<string> filesToDelete);
}