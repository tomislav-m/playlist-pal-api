using Application.DTOs;

namespace Application.Interfaces;

public interface ISpotifyTrackService
{
    Task<IEnumerable<TrackSimpleDto>> GetRecommendations(
        IEnumerable<string>? trackIds = null,
        IEnumerable<string>? artistIds = null,
        IEnumerable<string>? genreIds = null,
        int limit = 100,
        CancellationToken cancellationToken = default);
}