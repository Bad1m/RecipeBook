using Hangfire;
using RecipeMicroservice.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDependencies();
builder.Services.ConfigureRabbitMq(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.ConfigureExceptionHandler();

app.Run();