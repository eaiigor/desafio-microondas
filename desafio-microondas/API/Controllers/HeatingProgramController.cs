using desafio_microondas.API.DTOs;
using desafio_microondas.Application.Commands.HeatingProgramCommands;
using desafio_microondas.Application.Commands.MicrowaveCommands;
using desafio_microondas.Application.Queries.HeatingProgramQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace desafio_microondas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HeatingProgramController
{
    [HttpGet]
    public Task<List<HeatingProgramDTO>> GetAllAsync(
        [FromServices] IHeatingProgramQueries queries)
        => queries.GetHeatingProgramsAsync();


    [HttpPost]
    public Task<HeatingProgramDTO> CreatAsync(
        [FromServices] IMediator mediator, [FromBody] CreateHeatingProgramCommand command)
        => mediator.Send(command);

    [HttpDelete("{id}")]
    public Task<HeatingProgramDTO> DeleteAsync(
        [FromServices] IMediator mediator, long id)
        => mediator.Send(new DeleteHeatingProgramCommand { Id = id });
}