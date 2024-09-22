using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Application.Strategy;
using ADITUS.CodeChallenge.Domain.Entity;
using ADITUS.CodeChallenge.Domain.Enum;
using MediatR;

namespace ADITUS.CodeChallenge.Application.Queries;

public class GetEventsQuery : IRequest<IList<Event>>
{
}

public class GetEventsQueryHandler(IEventService eventService, IEnumerable<IStrategy> strategies)
  : IRequestHandler<GetEventsQuery, IList<Event>>
{
  public async Task<IList<Event>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
  {
    var events = await eventService.GetEvents();

    var onlineEvents = events.Where(q => q.Type == EventType.Online);
    var onsiteEvents = events.Where(q => q.Type == EventType.OnSite);
    var hybridEvents = events.Where(q => q.Type == EventType.Hybrid);

    var onlineStrategy = strategies.FirstOrDefault(q => q.CanProcess(EventType.Online)) ?? throw new Exception($"No strategy found for event type: {EventType.Online} ");
    var onsiteStrategy = strategies.FirstOrDefault(q => q.CanProcess(EventType.OnSite)) ?? throw new Exception($"No strategy found for event type: {EventType.OnSite} ");
    var hybridStrategy = strategies.FirstOrDefault(q => q.CanProcess(EventType.Hybrid)) ?? throw new Exception($"No strategy found for event type: {EventType.Hybrid} ");

    var tasks = new List<Task<Event>>();
    tasks.AddRange(onlineEvents.Select(q => onlineStrategy.Process(q)).ToArray());
    tasks.AddRange(onsiteEvents.Select(q => onsiteStrategy.Process(q)).ToArray());
    tasks.AddRange(hybridEvents.Select(q => hybridStrategy.Process(q)).ToArray());

    var results = await Task.WhenAll(tasks);
    return results;
  }
}