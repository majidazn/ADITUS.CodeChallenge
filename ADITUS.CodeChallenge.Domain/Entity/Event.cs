using ADITUS.CodeChallenge.Domain.Dto;
using ADITUS.CodeChallenge.Domain.Enum;

namespace ADITUS.CodeChallenge.Domain.Entity
{
  public class Event
  {
    public Guid Id { get; init; } 
    public int Year { get; init; }
    public string Name { get; init; }
    public EventType Type { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    
    public OnlineStatistic OnlineStatistic  { get; set; }
    
    public OnSiteStatistic OnSiteStatistic  { get; set; }

    public List<ReservedHardware> ReservedHardwares { get; set; } = [];
  }
}