using desafio_microondas.API.DTOs;
using desafio_microondas.Application.Commands.MicrowaveCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace desafio_microondas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MicrowaveController
{
    [HttpPost("start")]
    public async Task<MicrowaveDTO> StartJobAsync(
        [FromServices] IMediator mediator, [FromBody] StartHeatingCommand command)
        => await mediator.Send(command);

    [HttpPost("pause")]
    public async Task<MicrowaveDTO> PauseJobAsync(
        [FromServices] IMediator mediator, [FromBody] PauseHeatingCommand command)
        => await mediator.Send(command);

    [HttpPost("stop")]
    public async Task<MicrowaveDTO> StopJobAsync(
        [FromServices] IMediator mediator, [FromBody] StopHeatingCommand command)
        => await mediator.Send(command);
}