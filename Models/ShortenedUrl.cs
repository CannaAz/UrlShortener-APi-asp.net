namespace URLSHORTENER.Models;

public class ShortenedUrl
{
    public Guid Id {get; set;}
    public string LongUrl {get; set;}

    public string ShortUrl {get; set;}

    public string Code {get; set;}

    public DateTimeOffset CreatedAt {get; set;}
}
