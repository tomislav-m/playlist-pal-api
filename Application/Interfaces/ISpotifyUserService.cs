using Application.DTOs;

namespace Application.Interfaces;

public interface ISpotifyUserService
{
    Task<IEnumerable<ArtistDto>> GetUserArtistsHistory(string? timeRange, int limit = 50, CancellationToken cancellationToken = default);
    Task<IEnumerable<TrackDto>> GetUserTracksHistory(string? timeRange, int limit = 50, CancellationToken cancellationToken = default);
}
