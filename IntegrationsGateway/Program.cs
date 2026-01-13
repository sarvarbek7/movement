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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();