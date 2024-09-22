using System.Net.Http.Headers;
using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ADITUS.CodeChallenge.Infrastructure.HttpServices;

public class OnlineStatisticsHttpService(
  HttpClient httpClient,
  ILogger<IHttpService<OnlineHttpRequest, OnlineHttpResponse>> logger)
  : BaseHttpService<OnlineHttpRequest, OnlineHttpResponse>(httpClient, logger)
{
  public override HttpRequestMessage GetHttpRequestMessage(OnlineHttpRequest request)
  {
    var uri = $"https://codechallenge-statistics.azurewebsites.net/api/online-statistics/{request.EventId}";

    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

    return httpRequestMessage;
  }
}