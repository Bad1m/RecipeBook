using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RecipeMicroservice.Application.Mappings;
using RecipeMicroservice.Application.Recipes.CommandHandlers.Create;
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
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Scoped);
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IInstructionRepository, InstructionRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddAutoMapper(typeof(RecipeMapperProfile));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateRecipeHandler).Assembly));
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RecipeContext>(opts =>
                opts.UseSqlServer(connectionString, sqlServerOptions =>
                    sqlServerOptions.MigrationsAssembly("RecipeMicroservice.Infrastructure"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            var serviceProvider = services.BuildServiceProvider();
            var authContext = serviceProvider.GetRequiredService<RecipeContext>();
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
            });
        }
    }
}
