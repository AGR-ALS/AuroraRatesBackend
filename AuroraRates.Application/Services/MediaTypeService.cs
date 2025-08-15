using AuroraRates.Domain.Abstractions.MediaType;
using AuroraRates.Domain.Models;

namespace AuroraRates.Application.Services;

public class MediaTypeService : IMediaTypeService
{
    private readonly IMediaTypeRepository _repository;

    public MediaTypeService(IMediaTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MediaType>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<MediaType> GetTypeByIdAsync(Guid id)
    {
        return (await _repository.GetByIdAsync(id))!;
    }

    public async Task<MediaType?> GetByNameAsync(string name)
    {
        return await _repository.GetByNameAsync(name);
    }
}