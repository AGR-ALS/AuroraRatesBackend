namespace AuroraRates.Domain.Abstractions.MediaType;

public interface IMediaTypeService
{
    Task<List<Models.MediaType>> GetAllAsync();
    Task<Models.MediaType> GetTypeByIdAsync(Guid id);
    Task<Models.MediaType?> GetByNameAsync(string name);
}