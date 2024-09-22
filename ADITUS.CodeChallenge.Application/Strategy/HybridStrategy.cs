using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Application.Strategy;
using ADITUS.CodeChallenge.Domain.Dto;
using ADITUS.CodeChallenge.Domain.Entity;
using ADITUS.CodeChallenge.Domain.Enum;
using ADITUS.CodeChallenge.Domain.Models;

namespace ADITUS.CodeChallenge.Application.Strategy;

public class HybridStrategy: IStrategy
{
  private readonly IHttpService<OnSiteHttpRequest, OnSiteHttpResponse> _onSiteStatisticsHttpService;
  private readonly IHttpService<OnlineHttpRequest, OnlineHttpResponse> _onlineStatisticsHttpService;

  public HybridStrategy(IHttpService<OnSiteHttpRequest, OnSiteHttpResponse> onSiteStatisticsHttpService, IHttpService<OnlineHttpRequest, OnlineHttpResponse> onlineStatisticsHttpService)
  {
    _onSiteStatisticsHttpService = onSiteStatisticsHttpService;
    _onlineStatisticsHttpService = onlineStatisticsHttpService;
  }
  
  public bool CanProcess(EventType eventType)
    => eventType == EventType.Hybrid;

  public async Task<Event> Process(Event @event)
  {
    var onSiteHttpResponse = await _onSiteStatisticsHttpService.Send(new OnSiteHttpRequest { EventId = @event.Id });

   
    var onlineHttpResponse = await _onlineStatisticsHttpService.Send(new OnlineHttpRequest { EventId = @event.Id });
    
    @event.OnSiteStatistic = new OnSiteStatistic
    {
      BoothsCount = onSiteHttpResponse.BoothsCount,
      ExhibitorsCount = onSiteHttpResponse.ExhibitorsCount,
      VisitorsCount = onSiteHttpResponse.VisitorsCount
    };
    
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
