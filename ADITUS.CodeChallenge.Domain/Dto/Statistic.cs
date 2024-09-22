using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ADITUS.CodeChallenge.Domain.Dto
{
  public class Statistic
  {
    
  }

  public class OnlineStatistic: Statistic
  {
    //online
    [JsonProperty("attendees")]
    public int Attendees { get; set; }
    [JsonProperty("invites")]
    public int Invites { get; set; }
    [JsonProperty("visits")]
    public int Visits { get; set; }
    [JsonProperty("virtualRooms")]
    public int VirtualRooms { get; set; }
  }
  
  public class OnSiteStatistic: Statistic
  {
    //onsite
    [JsonProperty("visitorsCount")]
    public int VisitorsCount { get; set; }
    [JsonProperty("exhibitorsCount")]
    public int ExhibitorsCount { get; set; }
    [JsonProperty("boothsCount")]
    public int BoothsCount { get; set; }
  }

}
