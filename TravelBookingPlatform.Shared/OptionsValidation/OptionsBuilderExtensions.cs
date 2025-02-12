using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace TravelBookingPlatform.Shared.OptionsValidation;

public static class OptionsBuilderExtensions
{
    public static OptionsBuilder<TOptions> AddFluentValidation<TOptions>(
        this OptionsBuilder<TOptions> builder)
        where TOptions : class
    {
        builder.Services.AddSingleton<IValidateOptions<TOptions>, FluentValidateOptions<TOptions>>();

        return builder;
    }
}
