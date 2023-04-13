using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Time.Sheet.Domain.Repositories;
using Time.Sheet.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Registro do serviço IBatidasService
builder.Services.AddScoped<IBatidasService, BatidasService>();
// Registro do serviço IFolhaDePontoService
builder.Services.AddScoped<IFolhasDePontoService, FolhasDePontoService>();
// Registro do serviço IRegistrosRepository
builder.Services.AddScoped<IRegistrosRepository, RegistrosRepository>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Configuração do banco de dados

builder.Services.AddControllers();

//Adionar Swagger

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Controle de Ponto API", Version = "1.0" });
});

var app = builder.Build();

// Habilita o middleware do Swagger

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
