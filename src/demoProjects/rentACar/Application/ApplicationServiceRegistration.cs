﻿using System.Reflection;
using Application.Features.Auth.Rules;
using Application.Features.Brands.Rules;
using Application.Services.AuthServices;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<BrandBusinessRules>();
        services.AddScoped<AuthRules>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddScoped<IAuthService, AuthManager>();

        return services;
    }
}