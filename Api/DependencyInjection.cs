using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Movement.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();

        services.AddLocalizationResources();

        return services;
    }

    private static IServiceCollection AddLocalizationResources(this IServiceCollection services)
    {
                services.AddLocalization((options) =>
        {
            options.ResourcesPath = "Resources";
        });

        services.AddRequestLocalization((options) =>
        {
            options.DefaultRequestCulture = new("en");

            IList<CultureInfo> supportedCultures =
            [
                new CultureInfo("en"),
                new CultureInfo("ru"),
                new CultureInfo("uz"),
            ];

            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.RequestCultureProviders.Clear();

            options.RequestCultureProviders.Add(new CustomRequestCultureProvider((context) =>
            {
                if (context.Request.Headers.TryGetValue("lang", out var langHeader))
                {
                    var lang = langHeader.FirstOrDefault();

                    return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(lang ?? "en"));
                }

                return Task.FromResult<ProviderCultureResult?>(null);
            }));

            options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider()
            {
                QueryStringKey = "lang",
                UIQueryStringKey = "lang"
            });

            options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
        });

        return services;
    }
}