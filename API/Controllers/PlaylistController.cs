using Application.DTOs;
using Application.Playlist.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PlaylistController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost]
    public Task<PlaylistDto> Create(GeneratePlaylist.Command command, CancellationToken cancellationToken)
    {
        return Mediator.Send(command, cancellationToken);
    }
}