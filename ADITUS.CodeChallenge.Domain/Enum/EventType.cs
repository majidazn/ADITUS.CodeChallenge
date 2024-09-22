using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ADITUS.CodeChallenge.Domain.Enum
{
  [Flags]
  [JsonConverter(typeof(StringEnumConverter))]
  public enum EventType
  {
    [Display(Name = "OnSite")]
    [EnumMember(Value = "OnSite")]
    OnSite = 1,

    [Display(Name = "Online")]
    [EnumMember(Value = "Online")]
    Online = 2,

    [Display(Name = "Hybrid")]
    [EnumMember(Value = "Hybrid")]
    Hybrid = OnSite | Online
  }
  
  [Flags]
  [JsonConverter(typeof(StringEnumConverter))]
  public enum HardwareType
  {
    [Display(Name = "Drehsperre")]
    [EnumMember(Value = "Drehsperre")]
    Drehsperre = 1,

    [Display(Name = "Funkhandscanner")]
    [EnumMember(Value = "Funkhandscanner")]
    Funkhandscanner = 2,

    [Display(Name = "Mobiles Scan-Terminal")]
    [EnumMember(Value = "Mobiles Scan-Terminal")]
    MobilesScanTerminal = 3
  }
}