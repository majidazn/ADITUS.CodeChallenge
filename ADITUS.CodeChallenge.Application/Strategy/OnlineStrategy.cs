using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Domain.Dto;
using ADITUS.CodeChallenge.Domain.Entity;
using ADITUS.CodeChallenge.Domain.Enum;
using ADITUS.CodeChallenge.Domain.Models;

namespace ADITUS.CodeChallenge.Application.Strategy;

public class OnlineStrategy : IStrategy
{
  private readonly IHttpService<OnlineHttpRequest, OnlineHttpResponse> _onLineStatisticsHttpService;

  public OnlineStrategy(IHttpService<OnlineHttpRequest, OnlineHttpResponse> onLineStatisticsHttpService)
  {
    _onLineStatisticsHttpService = onLineStatisticsHttpService;
  }

  public bool CanProcess(EventType eventType)
    => eventType == EventType.Online;

  public async Task<Event> Process(Event @event)
  {
    var onlineHttpResponse = await _onLineStatisticsHttpService.Send(new OnlineHttpRequest { EventId = @event.Id });

    @event.OnlineStatistic = new OnlineStatistic
    {
      Attendees = onlineHttpResponse.Attendees,
      Invites = onlineHttpResponse.Invites,
      Visits = onlineHttpResponse.Visits,
      VirtualRooms = onlineHttpResponse.VirtualRooms
    };

    return @event;
  }
}