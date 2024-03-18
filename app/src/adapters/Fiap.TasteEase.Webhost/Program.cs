using Fiap.TasteEase.Api;
using Fiap.TasteEase.Application;
using Fiap.TasteEase.Infra;
using Fiap.TasteEase.Webhost;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppSettings();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMapsterConfiguration();
builder.Services.AddApplicationCore();
builder.Services.AddEfCoreConfiguration(builder.Configuration);
builder.Services.AddCognitoIdentity();
builder.Services.AddRestApi(builder.Configuration);

var app = builder.Build();

app.UseRestApi();
app.UseSeedData();
app.Run();