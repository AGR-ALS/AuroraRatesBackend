using AuroraRates.Domain.Abstractions.MediaType;
using AuroraRates.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuroraRates.DataAccess.Repository;

public class MediaTypeRepository : IMediaTypeRepository
{
    private readonly AuroraRatesDatabaseContext _context;

    public MediaTypeRepository(AuroraRatesDatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<MediaType>> GetAllAsync()
    {
        List<MediaType> mediaTypes = await _context.MediaTypes.Select(mt => new MediaType(mt.Id, mt.Name)).ToListAsync();
        
        return mediaTypes;
    }

    public async Task<MediaType?> GetByIdAsync(Guid id)
    {
        var mediaType = await _context.MediaTypes.Where(mt => mt.Id == id).Select(mt => new MediaType(mt.Id, mt.Name)).FirstAsync();
        return mediaType;
    }
    
    public async Task<MediaType?> GetByNameAsync(string name)
    {
        var mediaType = await _context.MediaTypes.Where(mt => mt.Name == name).Select(mt => new MediaType(mt.Id, mt.Name)).FirstAsync();
        return mediaType;
    }
}