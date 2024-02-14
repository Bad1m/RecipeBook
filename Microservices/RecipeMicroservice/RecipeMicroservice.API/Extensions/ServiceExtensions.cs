using FluentValidation;
using Grpc.Net.Client;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RecipeMicroservice.API.ValidationHandler;
using RecipeMicroservice.Application.Helpers;
using RecipeMicroservice.Application.Interfaces;
using RecipeMicroservice.Application.Mappings;
using RecipeMicroservice.Application.Producers;
using RecipeMicroservice.Application.Recipes.CommandHandlers.Create;
using RecipeMicroservice.Domain.Settings;
using RecipeMicroservice.Infrastructure.Data;
using RecipeMicroservice.Infrastructure.Interfaces;
using RecipeMicroservice.Infrastructure.Repositories;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace RecipeMicroservice.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Scoped);
            services.AddScoped<IRecipeExistenceChecker, RecipeExistenceChecker>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IInstructionRepository, InstructionRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();
            services.AddAutoMapper(typeof(RecipeMappingProfile).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateRecipeHandler).Assembly));
            services.AddScoped<GrpcRecipeClient>();
        }

        public static void AddGrpcClient(this IServiceCollection services, IConfiguration configuration)
        {
            var grpcChannel = GrpcChannel.ForAddress(configuration["GrpcHost"]);
            services.AddSingleton(services => new GrpcRecipe.GrpcRecipeClient(grpcChannel));
        }

        public static void ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfig>(configuration.GetSection("RabbitMqConfig"));
            services.AddScoped<IRabbitMqProducer>(provider =>
            {
                var rabbitMqConfig = provider.GetRequiredService<IOptions<RabbitMqConfig>>().Value;

                return new RabbitMqProducer(
                    rabbitMqConfig.HostName,
                    rabbitMqConfig.ExchangeName,
                    rabbitMqConfig.Key
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

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RecipeContext>(opts =>
                opts.UseSqlServer(connectionString, sqlServerOptions =>
                    sqlServerOptions.MigrationsAssembly("RecipeMicroservice.Infrastructure"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            var serviceProvider = services.BuildServiceProvider();
            var recipeContext = serviceProvider.GetRequiredService<RecipeContext>();
            recipeContext.Database.Migrate();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Recipes API",
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