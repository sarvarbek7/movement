using MassTransit;
using Movement.Api;
using Movement.Api.Consumers;
using Movement.MessagingContracts.Buses;
using Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddApi();

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

    x.SetKebabCaseEndpointNameFormatter();

    // Register Consumers
    x.AddConsumer<PostDu2Consumer>();

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRequestLocalization();

app.Run();