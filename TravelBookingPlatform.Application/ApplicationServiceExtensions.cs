using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using AutoMapper;

namespace TravelBookingPlatform.Application;
public static class ApplicationServiceExtensions
{
public static IServiceCollection AddApplication(this IServiceCollection services)
{
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    services.AddAutoMapper(Assembly.GetExecutingAssembly());

    return services;
    }
}