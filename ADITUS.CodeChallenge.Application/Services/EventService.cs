using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Domain.Entity;
using ADITUS.CodeChallenge.Domain.Enum;
using Microsoft.Extensions.Logging;

namespace ADITUS.CodeChallenge.Application.Services;

public class EventService : IEventService
{
  private static readonly IList<Event> _events;

  static EventService()
  {
    _events = new List<Event>
    {
      new Event
      {
        Id = Guid.Parse("7c63631c-18d4-4395-9c1e-886554265eb0"),
        Year = 2019,
        Name = "ADITUS Code Challenge 2019",
        StartDate = new DateTime(2019, 1, 1),
        EndDate = new DateTime(2019, 1, 31),
        Type = EventType.OnSite
      },
      new Event
      {
        Id = Guid.Parse("751fd775-2c8e-48e0-955c-2144008e984a"),
        Year = 2020,
        Name = "ADITUS Code Challenge 2020",
        StartDate = new DateTime(2020, 1, 1),
        EndDate = new DateTime(2020, 1, 15),
        Type = EventType.Hybrid
      },
      new Event
      {
        Id = Guid.Parse("974098e0-9b3f-41d5-80c2-551600ad204a"),
        Year = 2021,
        Name = "ADITUS Code Challenge 2021",
        StartDate = new DateTime(2021, 1, 1),
        EndDate = new DateTime(2021, 1, 18),
        Type = EventType.Online
      },
      new Event
      {
        Id = Guid.Parse("28669572-2b9a-4b2c-ad7e-6434ea8ab761"),
        Year = 2022,
        Name = "ADITUS Code Challenge 2022",
        StartDate = new DateTime(2022, 1, 1),
        EndDate = new DateTime(2022, 1, 11),
        Type = EventType.Online
      },
      new Event
      {
        Id = Guid.Parse("3a17b294-8716-448c-94db-ebf9bf53f1c1"),
        Year = 2024,
        Name = "ADITUS Code Challenge 2024",
        StartDate = new DateTime(2024, 11, 1),
        EndDate = new DateTime(2024, 11, 23),
        Type = EventType.OnSite
      }
    };
  }

  public Task<Event> GetEvent(Guid id)
  {
    var @event = _events.FirstOrDefault(e => e.Id == id);
    return Task.FromResult(@event);
  }

  public  Task<IList<Event>> GetEvents()
  {
    return Task.FromResult(_events);
  }
}