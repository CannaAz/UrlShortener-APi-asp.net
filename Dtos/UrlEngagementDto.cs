namespace URLSHORTENER.Dtos;

public record UrlEngagementDto(
    List<DateTimeOffset> UsageDates,
    int AmountOfInteractions
);
