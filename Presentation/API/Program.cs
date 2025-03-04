using Persistence.Extensions;
using Application.Extenstions;
using Infrastructure.Extensions;
using API.Extensions;
using API.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.ConfigureLogging();

builder.Services.AddControllers();

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddAPIServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors(cfg => 
{
    cfg.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
