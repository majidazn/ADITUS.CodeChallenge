using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Application.Strategy;
using ADITUS.CodeChallenge.Domain.Entity;
using MediatR;

namespace ADITUS.CodeChallenge.Application.Queries;

public class GetEventByIdQuery : IRequest<Event>
{
  public Guid EventId { get; set; }
}

public class GetEventByIdQueryHandler(IEventService eventService, IEnumerable<IStrategy> strategies) : IRequestHandler<GetEventByIdQuery, Event>
{

  public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
  {
    var @event = await eventService.GetEvent(request.EventId);
    if (@event is null) throw new ArgumentNullException("Event is Null");

    var strategy = strategies.FirstOrDefault(q => q.CanProcess(@event.Type));
    if (strategy is null) throw new ArgumentNullException("Strategy is null");

    return await strategy.Process(@event);
  }
}