using ADITUS.CodeChallenge.Application.Configuration;
using ADITUS.CodeChallenge.Infrastructure.Configuration;

namespace ADITUS.CodeChallenge.API.Configuration
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddInfrastructure()
                    .AddApplication();

            return services;
        }
    }
}
