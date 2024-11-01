using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SpotifyAPI.Web;

namespace Infrastructure.Spotify;

public class SpotifyTrackService(IHttpContextAccessor contextAccessor, IMapper mapper)
    : SpotifyBaseService(contextAccessor, mapper), ISpotifyTrackService
{
    public async Task<IEnumerable<TrackSimpleDto>> GetRecommendations(
        IEnumerable<string>? trackIds = null,
        IEnumerable<string>? artistIds = null,
        IEnumerable<string>? genreIds = null,
        int limit = 100,
        CancellationToken cancellationToken = default)
    {
        var request = new RecommendationsRequest
        {
            Market = MarketUS,
            Limit = limit
        };

        if (trackIds is not null)
        {
            foreach (var id in trackIds)
            {
                request.SeedTracks.Add(id);
            }
        }

        if (artistIds is not null)
        {
            foreach (var id in artistIds)
            {
                request.SeedArtists.Add(id);
            }
        }

        if (genreIds is not null)
        {
            foreach (var id in genreIds)
            {
                request.SeedGenres.Add(id);
            }
        }

        var recommendations = await SpotifyClient.Browse.GetRecommendations(request, cancellationToken);

        return Mapper.ProjectTo<TrackSimpleDto>(recommendations.Tracks.AsQueryable());
    }
}