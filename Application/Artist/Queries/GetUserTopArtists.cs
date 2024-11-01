using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.Artist.Queries;

public static class GetUserTopArtists
{
    public record Query(string TimeRange) : IRequest<IEnumerable<ArtistSimpleDto>>;
    
    public class Handler(ISpotifyUserService userService) : IRequestHandler<Query, IEnumerable<ArtistSimpleDto>>
    {
        public async Task<IEnumerable<ArtistSimpleDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var artists = await userService.GetUserArtistsHistory(request.TimeRange, cancellationToken: cancellationToken);

            return artists;
        }
    }
}