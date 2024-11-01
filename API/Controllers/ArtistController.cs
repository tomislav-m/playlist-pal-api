using Application.Artist.Queries;
using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ArtistController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet("user-top")]
    public Task<IEnumerable<ArtistSimpleDto>> GetUserTopArtists([FromQuery] string timeRange, CancellationToken cancellationToken)
    {
        var query = new GetUserTopArtists.Query(timeRange);
        
        return Mediator.Send(query, cancellationToken);
    }
}