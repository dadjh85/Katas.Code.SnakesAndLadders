using API.Configuration.Settings;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Configuration.Startup
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(AddSwaggerConfiguration(GetSwaggerOptions(configuration)));
            return services;
        }

        public static WebApplication UseCustomSwagger(this WebApplication app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }

        #region Private Methods

        private static SwaggerSettings GetSwaggerOptions(IConfiguration configuration)
        {
            SwaggerSettings swaggerConfig = new SwaggerSettings();
            configuration.GetSection("SwaggerConfig").Bind(swaggerConfig);
            return swaggerConfig;
        }

        private static Action<SwaggerGenOptions> AddSwaggerConfiguration(SwaggerSettings swaggerConfig)
            => AddSwaggerDocumentation(swaggerConfig);

        private static Action<SwaggerGenOptions> AddSwaggerDocumentation(SwaggerSettings swaggerConfig)
            => c =>
            {
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.SwaggerDoc(swaggerConfig.Version, new OpenApiInfo { Title = swaggerConfig.Title, Version = swaggerConfig.Version });
            };

        #endregion
    }
}
