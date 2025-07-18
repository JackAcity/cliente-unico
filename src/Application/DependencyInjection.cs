using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

using Application.Common.Behaviors;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services){

        services.AddMediatR(config => {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
        });
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
        );
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(TransactionBehavior<,>)
        );
        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();
        
        return services;
    }
}