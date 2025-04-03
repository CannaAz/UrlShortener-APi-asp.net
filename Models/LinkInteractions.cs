namespace URLSHORTENER.Models;

public class LinkInteractions
{
    public Guid Id {get; set;}
    public Guid LinkId {get; set;}
    public int LinkUsages {get; set;}
}
