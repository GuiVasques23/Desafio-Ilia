using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Time.Sheet.Application.Services;
using Time.Sheet.Domain.Services;
using Time.Sheet.Infraestructure;
using Time.Sheet.Infraestructure.Repositories;

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
builder.Services.AddDbContext<PontoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
