using Application;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ykvelit.Extensions.MediatR.Behaviors;

namespace CrossCutting.DependencyInjection;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        var assembly = typeof(CreateTodo).Assembly;

        services.AddValidatorsFromAssembly(assembly);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);

            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
