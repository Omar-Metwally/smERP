namespace smERP.Application.DTOs.Results.Image;

public record ImageURLs
{
    public string Thumbnail { get; init; }
    public string Fullscreen { get; init; }
}
