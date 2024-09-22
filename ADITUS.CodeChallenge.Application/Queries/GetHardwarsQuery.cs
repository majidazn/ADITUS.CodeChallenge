using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Domain.Entity;
using MediatR;

namespace ADITUS.CodeChallenge.Application.Queries;

public class GetHardwaresQuery: IRequest<IList<Hardware>>
{
}

public class GetHardwaresQueryHandler(IHardwareService hardwareService, IEventService eventService): IRequestHandler<GetHardwaresQuery, IList<Hardware>>
{
  public  async Task<IList<Hardware>> Handle(GetHardwaresQuery request, CancellationToken cancellationToken)
  {
    var result =await  hardwareService.GetHardwares();
    return result;
     
  }
}