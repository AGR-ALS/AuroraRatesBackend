namespace AuroraRates.Domain.Models;

public class MediaType
{
    public MediaType(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
}