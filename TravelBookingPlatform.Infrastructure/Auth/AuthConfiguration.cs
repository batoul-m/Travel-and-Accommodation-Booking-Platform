using TravelBookingPlatform.Domain.Interfaces.Auth;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TravelBookingPlatform.Infrastructure.Auth;

public static class AuthConfiguration
{
    public static IServiceCollection AddAuthInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IValidator<JwtAuthConfig>, JwtAuthConfigValidator>();

        services.AddOptions<JwtAuthConfig>()
            .BindConfiguration(nameof(JwtAuthConfig))
            .Validate(config =>
            {
                using var scope = services.BuildServiceProvider().CreateScope();
                var validator = scope.ServiceProvider.GetRequiredService<IValidator<JwtAuthConfig>>();
                var result = validator.Validate(config);
                return result.IsValid;
            })
            .ValidateOnStart();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            using var scope = services.BuildServiceProvider().CreateScope();

            var config = scope.ServiceProvider
                .GetRequiredService<IOptions<JwtAuthConfig>>().Value;

            var key = Encoding.UTF8.GetBytes(config.SecretKey);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config.Issuer,
                ValidAudience = config.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
