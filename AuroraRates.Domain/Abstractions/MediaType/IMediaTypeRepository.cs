namespace AuroraRates.Domain.Abstractions.MediaType;

public interface IMediaTypeRepository
{
    Task<List<Models.MediaType>> GetAllAsync();
    Task<Models.MediaType?> GetByIdAsync(Guid id);
    Task<Models.MediaType?> GetByNameAsync(string name);
}