using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection;
public static class RootModule
{
    public static IServiceCollection AddRootModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddApplicationModule()
            .AddDataModule(configuration)
            ;
    }
}
