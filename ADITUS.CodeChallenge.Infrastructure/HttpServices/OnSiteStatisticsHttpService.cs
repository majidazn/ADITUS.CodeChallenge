using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ADITUS.CodeChallenge.Infrastructure.HttpServices;

public class OnSiteStatisticsHttpService(
  HttpClient httpClient,
  ILogger<IHttpService<OnSiteHttpRequest, OnSiteHttpResponse>> logger)
  : BaseHttpService<OnSiteHttpRequest, OnSiteHttpResponse>(httpClient, logger)
{
  public override HttpRequestMessage GetHttpRequestMessage(OnSiteHttpRequest request)
  {
    var uri = $"https://codechallenge-statistics.azurewebsites.net/api/onsite-statistics/{request.EventId}";

    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

    return httpRequestMessage;
  }
}