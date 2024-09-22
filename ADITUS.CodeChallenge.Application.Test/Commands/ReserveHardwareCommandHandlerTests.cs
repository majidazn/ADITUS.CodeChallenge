using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using ADITUS.CodeChallenge.Application.Commands;
using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Domain.Entity;
using ADITUS.CodeChallenge.Domain.Exceptions;

namespace ADITUS.CodeChallenge.Application.Test.Commands
{

  public class ReserveHardwareCommandHandlerTests
  {
    private readonly Mock<IHardwareService> _hardwareServiceMock;
    private readonly Mock<IEventService> _eventServiceMock;
    private readonly ReserveHardwareCommandHandler _handler;

    public ReserveHardwareCommandHandlerTests()
    {
      _hardwareServiceMock = new Mock<IHardwareService>();
      _eventServiceMock = new Mock<IEventService>();
      _handler = new ReserveHardwareCommandHandler(_hardwareServiceMock.Object, _eventServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEventStartsInLessThanFourWeeks()
    {
      // Arrange
      var command = new ReserveHardwareCommand
      {
        EventId = Guid.NewGuid(),
        HardwareId = Guid.NewGuid(),
        Quantity = 5
      };

      var @event = new Event
      {
        StartDate = DateTime.UtcNow.AddDays(20) 
      };

      _eventServiceMock.Setup(x => x.GetEvent(command.EventId))
          .ReturnsAsync(@event);

      // Act & Assert
      await Assert.ThrowsAsync<ReserveHardwareLessThan4WeeksIsNotAllowedException>(() =>
          _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenNotEnoughHardwareAvailable()
    {
      // Arrange
      var command = new ReserveHardwareCommand
      {
        EventId = Guid.NewGuid(),
        HardwareId = Guid.NewGuid(),
        Quantity = 10
      };

      var @event = new Event
      {
        StartDate = DateTime.UtcNow.AddDays(40) 
      };

      var hardware = new Hardware
      {
        Id = command.HardwareId,
        Quantity = 20,
        ReservedQuantity = 15 
      };

      _eventServiceMock.Setup(x => x.GetEvent(command.EventId))
          .ReturnsAsync(@event);

      _hardwareServiceMock.Setup(x => x.GetHardware(command.HardwareId))
          .ReturnsAsync(hardware);

      // Act & Assert
      await Assert.ThrowsAsync<NotSufficientHardwareException>(() =>
          _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldReserveHardware_WhenValidRequest()
    {
      // Arrange
      var command = new ReserveHardwareCommand
      {
        EventId = Guid.NewGuid(),
        HardwareId = Guid.NewGuid(),
        Quantity = 5
      };

      var @event = new Event
      {
        StartDate = DateTime.UtcNow.AddDays(40), 
        ReservedHardwares = new List<ReservedHardware>()
      };

      var hardware = new Hardware
      {
        Id = command.HardwareId,
        Quantity = 20,
        ReservedQuantity = 5
      };

      _eventServiceMock.Setup(x => x.GetEvent(command.EventId))
          .ReturnsAsync(@event);

      _hardwareServiceMock.Setup(x => x.GetHardware(command.HardwareId))
          .ReturnsAsync(hardware);

      // Act
      await _handler.Handle(command, CancellationToken.None);

      // Assert
      Assert.Equal(10, hardware.ReservedQuantity); 
      Assert.Single(@event.ReservedHardwares);
    }
  }
}