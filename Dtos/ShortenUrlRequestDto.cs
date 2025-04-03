using System.ComponentModel.DataAnnotations;

namespace URLSHORTENER.Dtos;

public class ShortenUrlRequestDto
{
    [Required]
    public string Url {get; set;} 
}
