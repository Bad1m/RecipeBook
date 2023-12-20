using AuthMicroservice.BusinessLogic.Interfaces;
using AuthMicroservice.BusinessLogic.Mappings;
using AuthMicroservice.BusinessLogic.Models;
using AuthMicroservice.BusinessLogic.Services;
using AuthMicroservice.BusinessLogic.Validators;
using AuthMicroservice.DataAccess.Data;
using AuthMicroservice.DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AuthMicroservice.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserRegistrationDtoValidator, UserRegistrationDtoValidator>();
            services.AddScoped<IUserLoginDtoValidator, UserLoginDtoValidator>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddAutoMapper(typeof(UserMappingProfile));
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
            var connectionString = isDocker ? configuration.GetConnectionString("DockerConnection") : configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AuthContext>(opts =>
                opts.UseSqlServer(connectionString, sqlServerOptions =>
                    sqlServerOptions.MigrationsAssembly("AuthMicroservice.DataAccess"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            var serviceProvider = services.BuildServiceProvider();
            var authContext = serviceProvider.GetRequiredService<AuthContext>();
            authContext.Database.EnsureCreated();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("JwtConfig");
            var secretKey = jwtConfig["secret"];
            var authSettings = new AuthSettings();
            configuration.GetSection("JwtConfig").Bind(authSettings);
            services.AddSingleton(authSettings);
            services.Configure<AuthSettings>(jwtConfig);
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AuthContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Auth API",
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
