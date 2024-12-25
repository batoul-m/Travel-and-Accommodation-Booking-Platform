using Serilog;
using TravelBookingPlatform.Api;
using TravelBookingPlatform.Api.RateLimiting;
using TravelBookingPlatform.Application;
using TravelBookingPlatform.Infrastructure;
using TravelBookingPlatform.Infrastructure.Persistence;
using static TravelBookingPlatform.Api.RateLimiting.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services
  .AddWebComponents()
  .AddApplication()
  .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();

app.UseSwaggerUI();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.Migrate();

app.UseAuthentication();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers()
   .RequireRateLimiting(policyName: RateLimiterPolicy);

app.Run();