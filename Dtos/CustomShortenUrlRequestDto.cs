namespace URLSHORTENER.Dtos;

public record CustomShortenUrlRequestDto
(
    string Url,
    string customName
);
