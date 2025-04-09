namespace URLSHORTENER.Models;

public class LinkUsage
{
    public Guid Id {get; set;}
    public Guid LinkId {get; set;}
    public DateTimeOffset UsedAt {get; set;}
}
