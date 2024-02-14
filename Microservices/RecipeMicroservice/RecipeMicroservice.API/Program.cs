using RecipeMicroservice.API.Extensions;
using RecipeMicroservice.Application.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDependencies();
builder.Services.AddGrpcClient(builder.Configuration);
builder.Services.ConfigureRabbitMq(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();

var app = builder
    .ConfigureKestrel()
    .Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGrpcService<GrpcUserRecipeService>();

app.ConfigureExceptionHandler();

app.Run();