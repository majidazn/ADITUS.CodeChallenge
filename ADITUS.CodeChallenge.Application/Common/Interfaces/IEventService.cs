using ADITUS.CodeChallenge.Domain.Entity;

namespace ADITUS.CodeChallenge.Application.Common.Interfaces
{
  public interface IEventService
  {
    Task<Event> GetEvent(Guid id);
    Task<IList<Event>> GetEvents();
  }
}