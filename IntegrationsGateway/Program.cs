using System.ComponentModel;
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

builder.Services.AddMassTransit<IMovementBackendBus>(x =>
{
    const string busKey = nameof(IMovementBackendBus);
    const string mqSettingsSectionKey = $"{MQSettingsKey}:{busKey}";

    var rabbitMqSettings = new RabbitMqConfigSetting();
    var settings = configuration.GetSection(mqSettingsSectionKey);

    settings.Bind(rabbitMqSettings);

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqSettings.Host, rabbitMqSettings.VHost,
                 h =>
                 {
                     h.Username(rabbitMqSettings.Username);
                     h.Password(rabbitMqSettings.Password);
                 });

        cfg.ConfigureEndpoints(context);
        cfg.UseTimeout(c => {
            c.Timeout = TimeSpan.FromSeconds(30);
        });
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