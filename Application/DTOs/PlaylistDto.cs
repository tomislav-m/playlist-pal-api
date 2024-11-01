namespace Application.DTOs;

public record PlaylistDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public IEnumerable<TrackSimpleDto> Tracks { get; set; } = default!;
}
