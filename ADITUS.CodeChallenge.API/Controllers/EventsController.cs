using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ADITUS.CodeChallenge.API.Controllers
{
  [Route("events")]
  public class EventsController : ControllerBase
  {
    private readonly IEventService _eventService;
    private readonly IMediator _mediator;

    public EventsController(IEventService eventService, IMediator mediator)
    {
      _eventService = eventService;
      _mediator = mediator;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetEvents()
    {
      var events = await _mediator.Send(new GetEventsQuery());
      return Ok(events);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetEvent(Guid id)
    {
      var @event = await _mediator.Send(new GetEventByIdQuery { EventId = id });
      
      if (@event == null)
      {
        return NotFound();
      }

      return Ok(@event);
    }
  }
}