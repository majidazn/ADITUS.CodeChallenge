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

public class OnSiteStrategy: IStrategy
{
  private readonly IHttpService<OnSiteHttpRequest, OnSiteHttpResponse> _onSiteStatisticsHttpService;

  public OnSiteStrategy(IHttpService<OnSiteHttpRequest, OnSiteHttpResponse> onSiteStatisticsHttpService)
  {
    _onSiteStatisticsHttpService = onSiteStatisticsHttpService;
  }
  
  public bool CanProcess(EventType eventType)
    => eventType == EventType.OnSite;

  public async Task<Event> Process(Event @event)
  {
    var onSiteHttpResponse = await _onSiteStatisticsHttpService.Send(new OnSiteHttpRequest { EventId = @event.Id });

    @event.OnSiteStatistic = new OnSiteStatistic
    {
      BoothsCount = onSiteHttpResponse.BoothsCount,
      ExhibitorsCount = onSiteHttpResponse.ExhibitorsCount,
      VisitorsCount = onSiteHttpResponse.VisitorsCount
    };

    return @event;
  }
}
