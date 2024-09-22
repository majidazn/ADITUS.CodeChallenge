using ADITUS.CodeChallenge.Domain.Dto;
using ADITUS.CodeChallenge.Domain.Entity;
using ADITUS.CodeChallenge.Domain.Enum;

namespace ADITUS.CodeChallenge.Application.Strategy;

public interface IStrategy
{
  bool CanProcess(EventType eventType);

  Task<Event> Process(Event @event);
}

