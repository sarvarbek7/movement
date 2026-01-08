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

app.Run();