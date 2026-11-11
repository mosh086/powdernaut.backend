using Powdernaut.WebApi.Filters;
using Microsoft.OpenApi;

namespace Powdernaut.WebApi.Providers;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            // Swagger document info
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Powdernaut WebAPI",
                Version = "v1",
                Description = "Powdernaut",
                Contact = new OpenApiContact
                {
                    Name = "Salazar",
                    Email = "mosh086@gmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            // XML Comments
            var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
                c.IncludeXmlComments(xmlPath);

            // JWT Bearer Security Definition
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            // Global Security Requirement for JWT
            c.AddSecurityRequirement(doc => new OpenApiSecurityRequirement());

            // Add global operation filter for 401 responses
            c.OperationFilter<UnauthorizedResponseOperationFilter>();

            // Optional: Sort endpoints alphabetically
            c.OrderActionsBy(apiDesc =>
                $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");

            // Optional: Use camelCase schema IDs
            c.CustomSchemaIds(type => type.FullName!.Replace("+", "."));
        });

        return services;
    }

    public static WebApplication UseSwaggerService(this WebApplication app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Powdernaut WebAPI v1");
            options.RoutePrefix = "swagger";
            options.DisplayRequestDuration();
            options.EnableTryItOutByDefault();
            options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        });

        return app;
    }
}