using Microsoft.OpenApi.Models;

namespace TableServe.API.Extensions
{
    public static class SwaggerConfiguration
    {
        public static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TableServe API",
                    Version = "v1",
                    Description = "API documentation for TableServe"
                });

                options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Name = "X-Api-Key",
                    In = ParameterLocation.Header,
                    Description = "API Key Authentication",
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }
    }
}
