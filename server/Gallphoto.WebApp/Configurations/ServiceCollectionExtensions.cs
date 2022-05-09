using Gallphoto.WebApp.Configurations.Models;
using Gallphoto.WebApp.Services.Users;
using Microsoft.OpenApi.Models;

namespace Gallphoto.WebApp.Configurations;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register configurations
    /// </summary>
    /// <param name="services">service collection</param>
    /// <param name="configuration">config</param>
    public static void RegisterConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
    }
    
    
    /// <summary>
    /// Register services
    /// </summary>
    /// <param name="services">service collection</param>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
    
    /// <summary>
    /// Register swagger gen
    /// </summary>
    /// <param name="services">service collection</param>
    public static void RegisterSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Gallphoto API v1", Version = "v1" });
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
    }
}