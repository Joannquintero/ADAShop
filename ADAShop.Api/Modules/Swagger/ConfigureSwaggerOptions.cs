using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ADAShop.Api.Modules.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        //In C# the tag ---> <inheritdoc/>
        //states that a documentation comment must inherit
        //documentation from a base class or implemented interface.
        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            // Personalizar swagger 💻
            var info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = "Mi Backend",
                Description = "Versionamiento API .Net Core.",
                Contact = new OpenApiContact
                {
                    Name = "Joann Quintero",
                    Email = "joann@example.com",
                    Url = new Uri("https://www.linkedin.com/in/joannquintero/")
                },
                License = new OpenApiLicense
                {
                    Name = "Licencia de Uso bajo LICX"
                }
            };
            if (description.IsDeprecated)
            {
                info.Description += "Esta versión de API ha quedado obsoleta.";
            }
            return info;
        }
    }
}