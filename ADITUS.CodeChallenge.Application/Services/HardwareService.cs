using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Domain.Entity;

namespace ADITUS.CodeChallenge.Application.Services;

public class HardwareService : IHardwareService
{
  private static readonly IList<Hardware> _hardwares;

  static HardwareService()
  {
    _hardwares = new List<Hardware>
    {
      new Hardware
      {
        Id = Guid.Parse("7c63631c-18d4-4395-9c1e-886554265111"), Name = "Drehsperre", Quantity = 10
      },
      new Hardware 
      {
        Id = Guid.Parse("7c63631c-18d4-4395-9c1e-886554265112"), Name = "Funkhandscanner", Quantity = 10
      },
      new Hardware
      {
        Id = Guid.Parse("7c63631c-18d4-4395-9c1e-886554265113"), Name = "Mobiles Scan-Terminal", Quantity = 10
      }
    };
  }

  public Task<Hardware> GetHardware(Guid id)
  {
    var hardware = _hardwares.FirstOrDefault(e => e.Id == id);
    return Task.FromResult(hardware);
  }

  public Task<IList<Hardware>> GetHardwares()
  {
    return Task.FromResult(_hardwares);
  }


  public List<Hardware> GetReservedHardwares()
  {
    return _hardwares.Where(q => q.ReservedQuantity > 0).ToList();
  }
}