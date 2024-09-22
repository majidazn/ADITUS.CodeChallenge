using System.Reflection;
using ADITUS.CodeChallenge.Application.Common.Interfaces;
using ADITUS.CodeChallenge.Application.Services;
using ADITUS.CodeChallenge.Application.Strategy;
using Microsoft.Extensions.DependencyInjection;

namespace ADITUS.CodeChallenge.Application.Configuration;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(q=>q.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddSingleton<IEventService, EventService>();
        services.AddSingleton<IHardwareService, HardwareService>();
        services.AddScoped<IStrategy, OnlineStrategy>();
        services.AddScoped<IStrategy, OnSiteStrategy>();
        services.AddScoped<IStrategy, HybridStrategy>();
        
        return services;
    }
}