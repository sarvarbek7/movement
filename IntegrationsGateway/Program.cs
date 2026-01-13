using MassTransit;
using Microsoft.EntityFrameworkCore;
using Movement.IntegrationsGateway.Data;
using Movement.IntegrationsGateway.Endpoints;
using Movement.MessagingContracts.Buses;
using Shared.Interfaces;
using Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment webHostEnvironment = builder.Environment;

string rootFolder = webHostEnvironment.ContentRootPath;

string appsettingsPath = Path.Combine(rootFolder, "appsettings.json");
string appsettingsDevelopment = Path.Combine(rootFolder, "appsettings.Development.json");
string appsettingsStagingPath = Path.Combine(rootFolder, "appsettings.Staging.json");
string appsettingsProductionPath = Path.Combine(rootFolder, "appsettings.Production.json");

builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    IConfigurationSection? kestrelSection = context.Configuration.GetSection("Kestrel");

    serverOptions.Configure(kestrelSection);
});

if (builder.Environment.IsDevelopment())
{
    configuration.AddJsonFile(appsettingsDevelopment, optional: false, reloadOnChange: true);
}

if (builder.Environment.IsProduction())
{
    configuration.AddJsonFile(appsettingsProductionPath, optional: false, reloadOnChange: true);
}

if (builder.Environment.IsStaging())
{
    configuration.AddJsonFile(appsettingsStagingPath, optional: false, reloadOnChange: true);
}

const string MQSettingsKey = "MQSettings";

builder.Services.AddMassTransit<IEnaklIntegrationBus>(x =>
{
    const string busKey = nameof(IEnaklIntegrationBus);
    const string mqSettingsSectionKey = $"{MQSettingsKey}:{busKey}";

    var rabbitMqSettings = new RabbitMqConfigSetting();
    configuration.GetSection(mqSettingsSectionKey).Bind(rabbitMqSettings);

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqSettings.Host, rabbitMqSettings.VirtualHost,
                 h =>
                 {
                     h.Username(rabbitMqSettings.Username);
                     h.Password(rabbitMqSettings.Password);
                 });

        cfg.ConfigureEndpoints(context);
    });

    x.AddEntityFrameworkOutbox<IntegrationsGatewayDbContext>(o =>
    {
        o.UsePostgres();
        o.QueryDelay = TimeSpan.FromMinutes(1);
        o.QueryMessageLimit = 1000;
    });

    x.SetKebabCaseEndpointNameFormatter();
});

builder.Services.AddDbContext<IntegrationsGatewayDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("Default"));
    options.UseSnakeCaseNamingConvention();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPostDu2TimeIntegration();

app.Run();