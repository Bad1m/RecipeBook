using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RecipeMicroservice.API.ValidationHandler;
using RecipeMicroservice.Application.Helpers;
using RecipeMicroservice.Application.Interfaces;
using RecipeMicroservice.Application.Mappings;
using RecipeMicroservice.Application.Producers;
using RecipeMicroservice.Application.Recipes.CommandHandlers.Create;
using RecipeMicroservice.Domain.Constants;
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
            services.AddAutoMapper(typeof(RecipeMappingProfile).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateRecipeHandler).Assembly));
            services.AddScoped<IRabbitMqProducer>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();

                return new RabbitMqProducer(RabbitMqConfig.HostName, RabbitMqConfig.ExchangeName, RabbitMqConfig.Key);
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