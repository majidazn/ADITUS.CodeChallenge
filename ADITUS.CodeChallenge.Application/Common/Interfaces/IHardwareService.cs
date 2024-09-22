using ADITUS.CodeChallenge.Domain.Entity;

namespace ADITUS.CodeChallenge.Application.Common.Interfaces;

public interface IHardwareService
{
  Task<Hardware> GetHardware(Guid id);

  Task<IList<Hardware>> GetHardwares();

  List<Hardware> GetReservedHardwares();
}