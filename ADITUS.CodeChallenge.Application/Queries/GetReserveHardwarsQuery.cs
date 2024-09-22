using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Domain.Entity;
using MediatR;

namespace ADITUS.CodeChallenge.Application.Queries;

public class GetReserveHardwaresQuery: IRequest<List<Hardware>>
{
}

public class GetReserveHardwaresQueryHandler(IHardwareService hardwareService, IEventService eventService): IRequestHandler<GetReserveHardwaresQuery, List<Hardware>>
{
  public Task<List<Hardware>> Handle(GetReserveHardwaresQuery request, CancellationToken cancellationToken)
  {
      return Task.FromResult(hardwareService.GetReservedHardwares());
  }
}