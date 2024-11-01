using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase(IMediator mediator) : ControllerBase
{
    protected IMediator Mediator => mediator;
}