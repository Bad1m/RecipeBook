using ReviewMicroservice.API.Extensions;
using ReviewMicroservice.Application.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDependencies();
builder.Services.ConfigureRabbitMq(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);
builder.Services.ConfigureMongoDBContext(builder.Configuration);
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

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<GrpcRecipeService>();
app.MapGrpcService<GrpcUserService>();

app.ConfigureExceptionHandler();

app.ConfigureRabbitMqConsumer();

app.Run();