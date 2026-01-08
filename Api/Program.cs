using Microsoft.AspNetCore.Mvc;
using Movement.Api;
using Movement.Api.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRequestLocalization();

app.MapGet("/", ([FromServices] I18nService i18n) => $"Error: {i18n.GetError("SampleError")}, Message: {i18n.GetMessage("SampleMessage")}");

app.Run();