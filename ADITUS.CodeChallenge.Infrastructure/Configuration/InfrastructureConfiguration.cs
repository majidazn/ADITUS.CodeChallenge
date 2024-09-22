using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Domain.Models;
using ADITUS.CodeChallenge.Infrastructure.HttpServices;
using Microsoft.Extensions.DependencyInjection;

namespace ADITUS.CodeChallenge.Infrastructure.Configuration;

public static class InfrastructureConfiguration
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {


    return AddHttpClients(services);
  }

  private static IServiceCollection AddHttpClients(IServiceCollection services)
  {
    services.AddHttpClient<IHttpService<OnlineHttpRequest, OnlineHttpResponse>, OnlineStatisticsHttpService>();

    services.AddHttpClient<IHttpService<OnSiteHttpRequest, OnSiteHttpResponse>, OnSiteStatisticsHttpService>();

    return services;
  }
}