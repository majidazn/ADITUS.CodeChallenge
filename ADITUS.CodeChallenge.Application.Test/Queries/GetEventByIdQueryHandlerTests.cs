using Xunit;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ADITUS.CodeChallenge.Application.Queries;
using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Domain.Entity;
using ADITUS.CodeChallenge.Application.Strategy;

namespace ADITUS.CodeChallenge.Application.Queries.Tests
{
  public class GetEventByIdQueryHandlerTests
  {
    private readonly Mock<IEventService> _eventServiceMock;
    private readonly Mock<IStrategy> _strategyMock;
    private readonly GetEventByIdQueryHandler _handler;

    public GetEventByIdQueryHandlerTests()
    {
      _eventServiceMock = new Mock<IEventService>();
      _strategyMock = new Mock<IStrategy>();
      var strategies = new List<IStrategy> { _strategyMock.Object };
      _handler = new GetEventByIdQueryHandler(_eventServiceMock.Object, strategies);
    }

    [Fact]
    public async Task Handle_ShouldReturnProcessedEvent_WhenStrategyCanProcess()
    {
      // Arrange
      var command = new GetEventByIdQuery
      {
        EventId = Guid.NewGuid()
      };

      var @event = new Event
      {
        Id = command.EventId,
        Type = Domain.Enum.EventType.OnSite
      };

      var processedEvent = new Event
      {
        Id = command.EventId,
        Type = Domain.Enum.EventType.OnSite
      };

      _eventServiceMock.Setup(x => x.GetEvent(command.EventId))
          .ReturnsAsync(@event);

      _strategyMock.Setup(x => x.CanProcess(@event.Type))
          .Returns(true);

      _strategyMock.Setup(x => x.Process(@event))
          .ReturnsAsync(processedEvent);

      // Act
      var result = await _handler.Handle(command, CancellationToken.None);

      // Assert
      Assert.Equal(processedEvent, result);
      _strategyMock.Verify(x => x.Process(@event), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenNoStrategyCanProcess()
    {
      // Arrange
      var command = new GetEventByIdQuery
      {
        EventId = Guid.NewGuid()
      };

      var @event = new Event
      {
        Id = command.EventId,
        Type = Domain.Enum.EventType.OnSite
      };

      _eventServiceMock.Setup(x => x.GetEvent(command.EventId))
          .ReturnsAsync(@event);

      _strategyMock.Setup(x => x.CanProcess(@event.Type))
          .Returns(false);

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, CancellationToken.None));

      _strategyMock.Verify(x => x.Process(It.IsAny<Event>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEventIsNull()
    {
      // Arrange
      var command = new GetEventByIdQuery
      {
        EventId = Guid.NewGuid()
      };

      _eventServiceMock.Setup(x => x.GetEvent(command.EventId))
           .ReturnsAsync((Event)null);

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, CancellationToken.None));
    }
  }
}