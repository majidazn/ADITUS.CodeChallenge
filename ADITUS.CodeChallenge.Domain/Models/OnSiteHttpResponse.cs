using Newtonsoft.Json;

namespace ADITUS.CodeChallenge.Domain.Models;

public class OnSiteHttpResponse
{
  [JsonProperty("visitorsCount")]
  public int VisitorsCount { get; set; }
  
  [JsonProperty("exhibitorsCount")]
  public int ExhibitorsCount { get; set; }
  
  [JsonProperty("boothsCount")]
  public int BoothsCount { get; set; }
}