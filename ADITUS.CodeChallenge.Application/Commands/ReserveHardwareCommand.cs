using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Domain.Entity;
using ADITUS.CodeChallenge.Domain.Exceptions;
using MediatR;

namespace ADITUS.CodeChallenge.Application.Commands;

public class ReserveHardwareCommand: IRequest
{
  public Guid EventId { get; set; }
  public Guid HardwareId { get; set; }
  public int Quantity { get; set; }
}

public class ReserveHardwareCommandHandler(IHardwareService hardwareService, IEventService eventService): IRequestHandler<ReserveHardwareCommand>
{
  public async Task Handle(ReserveHardwareCommand request, CancellationToken cancellationToken)
  {
    var @event = await eventService.GetEvent(request.EventId);

    if (IsEventStartingInLessThanFourWeeks(@event))
    {
      throw new ReserveHardwareLessThan4WeeksIsNotAllowedException("Hardware cannot be reserved for events that are starting in less than 4 weeks");
    }

    var hardware = await hardwareService.GetHardware(request.HardwareId);
    if ((hardware.ReservedQuantity + request.Quantity) > hardware.Quantity)
    {
      throw new NotSufficientHardwareException($"There are not sufficient hardwares to be reserved for HardwareId: {request.HardwareId}");
    }

    hardware.ReservedQuantity += request.Quantity;

    var reservedHardware = @event.ReservedHardwares.FirstOrDefault(q => q.HardwareId == hardware.Id);
    if (reservedHardware is null)
    {
      @event.ReservedHardwares.Add(new ReservedHardware
      {
        EventId = request.EventId,
        HardwareId = request.HardwareId,
        Quantity = hardware.ReservedQuantity,
        Name = hardware.Name,
        ReserveDate = DateTime.UtcNow
      });
    }
    else
    {
      reservedHardware.Quantity = hardware.ReservedQuantity;
      reservedHardware.ReserveDate = DateTime.UtcNow;
    }
  }
  
  private bool IsEventStartingInLessThanFourWeeks(Event @event)
  {
      var timeUntilStart = @event.StartDate.Value - DateTime.UtcNow;

      return timeUntilStart.TotalDays <= 28;
  }
}