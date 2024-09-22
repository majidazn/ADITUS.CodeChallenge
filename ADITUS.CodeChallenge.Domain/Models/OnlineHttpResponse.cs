using Newtonsoft.Json;

namespace ADITUS.CodeChallenge.Domain.Models;

public class OnlineHttpResponse
{
  [JsonProperty("attendees")]
  public int Attendees { get; set; }
  
  [JsonProperty("invites")]
  public int Invites { get; set; }
  
  [JsonProperty("visits")]
  public int Visits { get; set; }
  
  [JsonProperty("virtualRooms")]
  public int VirtualRooms { get; set; }
}