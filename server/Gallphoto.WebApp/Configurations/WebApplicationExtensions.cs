namespace Gallphoto.WebApp.Configurations;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder UseConfiguredSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}