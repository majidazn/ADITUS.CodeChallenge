using ADITUS.CodeChallenge.Application.Commands;
using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Application.Queries;
using ADITUS.CodeChallenge.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ADITUS.CodeChallenge.API.Controllers
{
  [Route("hardwares")]
  public class HardwaresController : ControllerBase
  {
    private readonly IEventService _eventService;
    private readonly IMediator _mediator;

    public HardwaresController(IEventService eventService, IMediator mediator)
    {
      _eventService = eventService;
      _mediator = mediator;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetHardwares() =>
   Ok(await _mediator.Send(new GetHardwaresQuery()));

    [HttpPost]
    [Route("reserve")]
    public async Task<IActionResult> Reserve(ReserveHardwareCommand reserveHardwareCommand)
    {
      await _mediator.Send(reserveHardwareCommand);
      return Ok();
    }

    [HttpGet]
    [Route("get-status")]
    public async Task<IActionResult> GetStatus(Guid id)
    {
      var hardwares = await _mediator.Send(new GetReserveHardwaresQuery());
      return Ok(hardwares);
    }
  }
}