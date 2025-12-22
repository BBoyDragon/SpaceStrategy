using Microsoft.OpenApi.Models;
using SpaceBackend.Repositories;
using SpaceBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SpaceBackend API",
        Version = "v1",
        Description = "API для получения информации о сервисе и базе данных"
    });
});

// Регистрация зависимостей трехслойной архитектуры
builder.Services.AddScoped<IDatabaseRepository, DatabaseRepository>();
builder.Services.AddScoped<IServiceInfoService, ServiceInfoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Swagger должен быть доступен всегда
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpaceBackend API v1");
    c.RoutePrefix = "swagger"; // Доступен по адресу /swagger
});

// HTTPS редирект только для Production окружения (в Docker используем только HTTP)
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();