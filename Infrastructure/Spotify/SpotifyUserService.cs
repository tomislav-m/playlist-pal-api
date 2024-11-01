using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SpotifyAPI.Web;
using TimeRange = Application.Core.Enums.TimeRange;

namespace Infrastructure.Spotify;

public class SpotifyUserService(IHttpContextAccessor contextAccessor, IMapper mapper)
    : SpotifyBaseService(contextAccessor, mapper), ISpotifyUserService
{
    public async Task<IEnumerable<ArtistDto>> GetUserArtistsHistory(string? timeRange, int limit = 50, CancellationToken cancellationToken = default)
    {
        var timeRanges = MapTimeRange(timeRange);

        var tasks = new List<Task<Paging<FullArtist>>>();
        foreach (var range in timeRanges)
        {
            var personalizationTopRequest = new PersonalizationTopRequest
            {
                TimeRangeParam = range,
                Limit = limit
            };
            
            tasks.Add(SpotifyClient.Personalization.GetTopArtists(personalizationTopRequest, cancellationToken));
        }

        var topArtists = (await Task.WhenAll(tasks)).SelectMany(x => x.Items!);

        topArtists = topArtists.DistinctBy(x => x.Id)
            .OrderBy(_ => Guid.NewGuid())
            .Take(limit);

        return Mapper.ProjectTo<ArtistDto>(topArtists.AsQueryable());
    }

    public async Task<IEnumerable<TrackDto>> GetUserTracksHistory(string? timeRange, int limit = 50, CancellationToken cancellationToken = default)
    {
        var timeRanges = MapTimeRange(timeRange);
        
        var tasks = new List<Task<Paging<FullTrack>>>();
        foreach (var range in timeRanges)
        {
            var personalizationTopRequest = new PersonalizationTopRequest
            {
                TimeRangeParam = range,
                Limit = 50
            };
            
            tasks.Add(SpotifyClient.Personalization.GetTopTracks(personalizationTopRequest, cancellationToken));
        }
        
        var topTracks = (await Task.WhenAll(tasks)).SelectMany(x => x.Items!);
        
        return Mapper.ProjectTo<TrackDto>(topTracks.DistinctBy(x => x.Id).AsQueryable());
    }

    private static IEnumerable<PersonalizationTopRequest.TimeRange> MapTimeRange(string? timeRange)
    {
        return timeRange switch
        {
            TimeRange.Medium => [PersonalizationTopRequest.TimeRange.MediumTerm],
            TimeRange.Short => [PersonalizationTopRequest.TimeRange.ShortTerm],
            TimeRange.Long => [PersonalizationTopRequest.TimeRange.LongTerm],
            _ =>
            [
                PersonalizationTopRequest.TimeRange.ShortTerm,
                PersonalizationTopRequest.TimeRange.MediumTerm,
                PersonalizationTopRequest.TimeRange.LongTerm
            ]
        };
    }
}
