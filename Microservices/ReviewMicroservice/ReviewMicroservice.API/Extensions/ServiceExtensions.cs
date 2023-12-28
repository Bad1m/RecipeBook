using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ReviewMicroservice.Application.Interfaces;
using ReviewMicroservice.Application.Services;
using ReviewMicroservice.Domain.Mappings;
using ReviewMicroservice.Domain.Settings;
using ReviewMicroservice.Infrastructure.Data;
using ReviewMicroservice.Infrastructure.Interfaces;
using ReviewMicroservice.Infrastructure.Repositories;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace ReviewMicroservice.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Scoped);
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddAutoMapper(typeof(ReviewMappingProfile));
        }

        public static void ConfigureMongoDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

            services.Configure<MongoDBSettings>(configuration.GetSection(nameof(MongoDBSettings)));
            services.AddSingleton(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                var connectionString = isDocker ? settings.DockerConnectionString : settings.ConnectionString;

                return new MongoDBContext(connectionString, settings.DatabaseName);
            });
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
            });
        }
    }
}