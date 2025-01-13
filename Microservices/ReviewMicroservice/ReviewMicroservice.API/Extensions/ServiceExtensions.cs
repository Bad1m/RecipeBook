using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ReviewMicroservice.Application.Interfaces;
using ReviewMicroservice.Application.Services;
using ReviewMicroservice.Application.Mappings;
using ReviewMicroservice.Domain.Settings;
using ReviewMicroservice.Infrastructure.Data;
using ReviewMicroservice.Infrastructure.Interfaces;
using ReviewMicroservice.Infrastructure.Repositories;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using ReviewMicroservice.Application.Consumers;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using ReviewMicroservice.Application.Grpc;

namespace ReviewMicroservice.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Scoped);
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ICacheRepository, CacheRepository>();
            services.AddAutoMapper(typeof(ReviewMappingProfile));
            services.AddScoped<GrpcRecipeService>();
            services.AddScoped<GrpcUserService>();
        }

        public static WebApplicationBuilder ConfigureKestrel(this WebApplicationBuilder builder)
        {
            builder.WebHost.UseKestrel(options =>
            {
                options.Listen(IPAddress.Any, 80, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1;
                });
                options.Listen(IPAddress.Any, 8087, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            });

            return builder;
        }

        public static void ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfig>(configuration.GetSection("RabbitMqConfig"));
            services.AddScoped<IRabbitMqConsumer>(provider =>
            {
                var reviewRepository = provider.GetRequiredService<IReviewRepository>();
                var recipeRepository = provider.GetRequiredService<IRecipeRepository>();
                var rabbitMqConfig = provider.GetRequiredService<IOptions<RabbitMqConfig>>().Value;
                var cacheRepository = provider.GetRequiredService<ICacheRepository>();

                return new RabbitMqConsumer(
                    rabbitMqConfig.HostName,
                    rabbitMqConfig.ExchangeName,
                    rabbitMqConfig.DeleteQueue,
                    rabbitMqConfig.Key,
                    reviewRepository,
                    cacheRepository,
                    recipeRepository
                );
            });
        }

        public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheOptions>(configuration.GetSection("CacheOptions"));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:Uri"];
            });
        }

        public static void ConfigureMongoDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBSettings>(configuration.GetSection(nameof(MongoDBSettings)));
            services.AddSingleton(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>();

                return new MongoDBContext(settings);
            });
        }

        public static void ConfigureRabbitMqConsumer(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var rabbitMqConsumer = scope.ServiceProvider.GetRequiredService<IRabbitMqConsumer>();
                rabbitMqConsumer.StartConsuming();
            }
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Reviews API",
                    Version = "v1",
                    Description = "Recipe API Services."
                });
                swaggerGenOptions.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            });
        }
    }
}