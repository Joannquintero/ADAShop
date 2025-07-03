using Asp.Versioning;

namespace ADAShop.Api.Modules.Versioning
{
    public static class VersioningExtensions
    {
        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
                //o.ApiVersionReader = ApiVersionReader.Combine(
                //   new QueryStringApiVersionReader("api-version"),
                //   new HeaderApiVersionReader("x-version"),
                //   new MediaTypeApiVersionReader("version")
                //);
                // Tipos de configuracion para versionamiento
                //o.ApiVersionReader = new UrlSegmentApiVersionReader(); // Para tomarlo directamente desde la url '[Route("api/v{version:apiVersion}/[controller]")]'
                o.ApiVersionReader = new HeaderApiVersionReader("x-version"); // Para tomarlo desde la cabecera del request
                //o.ApiVersionReader = new QueryStringApiVersionReader("version"); // Para incluir en la cadena de string '/api/Categories/GetAll?version=1.0'
            }).AddApiExplorer(options =>
        {
            //semantic versioning
            //first character is the principal or greater version
            //second character is the minor version
            //third character is the patch
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
            return services;
        }
    }
}