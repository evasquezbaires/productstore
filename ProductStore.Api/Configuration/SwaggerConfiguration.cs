using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ProductStore.Api.Configuration
{
    /// <summary>
    /// Custom configuration for Swagger injection
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Allows to inject the necessary dependencies for Swagger configuration
        /// </summary>
        /// <param name="services">Services of the application</param>
        public static void AddSwagger(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            var startupAssembly = Assembly.GetEntryAssembly();
            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    var assemblyDetails = startupAssembly.GetCustomAttribute<AssemblyProductAttribute>();

                    c.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = "ProductStore API Documentation",
                        Version = description.ApiVersion.ToString(),
                        Description = "Perform different actions over stock items on the Store",
                        Contact = new OpenApiContact()
                        {
                            Name = "Edwin Vásquez",
                            Email = "edwin.vasquez.osorio@gmail.com"
                        }
                    });
                }

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseSwaggerPage(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(sw =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    sw.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"ProductStore API - {description.GroupName.ToUpperInvariant()}");
                }
            });
        }
    }
}
