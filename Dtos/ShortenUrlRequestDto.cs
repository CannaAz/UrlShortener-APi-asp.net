using System.ComponentModel.DataAnnotations;

namespace URLSHORTENER.Dtos;

public record ShortenUrlRequestDto
(
    string Url
);
