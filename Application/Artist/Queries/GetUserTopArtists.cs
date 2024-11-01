using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.Artist.Queries;

public static class GetUserTopArtists
{
    public record Query(string TimeRange) : IRequest<IEnumerable<ArtistSimpleDto>>
    {
        public int Limit { get; init; } = 10;
    }
    
    public class Handler(ISpotifyUserService userService) : IRequestHandler<Query, IEnumerable<ArtistSimpleDto>>
    {
        public async Task<IEnumerable<ArtistSimpleDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var artists = await userService.GetUserArtistsHistory(request.TimeRange, request.Limit, cancellationToken);

            return artists;
        }
    }
}