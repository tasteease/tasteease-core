using Fiap.TasteEase.Api;
using Fiap.TasteEase.Application;
using Fiap.TasteEase.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var secretPath = Environment.GetEnvironmentVariable("SECRETS_PATH") ?? "";
builder.Configuration.AddJsonFile($"{secretPath}appsettings.json", optional: true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMapsterConfiguration();
builder.Services.AddApplicationCore();
builder.Services.AddEfCoreConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSeedData();
app.Run();
