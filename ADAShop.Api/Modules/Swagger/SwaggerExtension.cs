using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ADAShop.Api.Modules.Swagger
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            #region Implementaciones del Swagger del program se puede mover aqui

            //services.AddSwaggerGen(c =>
            //{
            //    //Set the comments path for the Swagger JSON and UI.
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    c.IncludeXmlComments(xmlPath);
            //});

            //    services.AddSwaggerGen(c =>
            //    {
            //        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            //        //c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi Backend", Version = "v1" });
            //        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //        {
            //            Description = @"JWT Authorization header using the Bearer scheme. <br /> <br />
            //              Enter 'Bearer' [space] and then your token in the text input below.<br /> <br />
            //              Example: 'Bearer 12345abcdef'<br /> <br />",
            //            Name = "Authorization",
            //            In = ParameterLocation.Header,
            //            Type = SecuritySchemeType.ApiKey,
            //            Scheme = "Bearer"
            //        });
            //        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            //      {
            //{
            //  new OpenApiSecurityScheme
            //  {
            //        Reference = new OpenApiReference
            //          {
            //            Type = ReferenceType.SecurityScheme,
            //            Id = "Bearer"
            //          },
            //          Scheme = "oauth2",
            //          Name = "Bearer",
            //          In = ParameterLocation.Header,
            //        },
            //        new List<string>()
            //  }
            //        });
            //        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            //        {
            //            Version = "v1",
            //            Title = "Mi API",
            //            Description = "Ejemplo de documentación con Swagger en ASP.NET Core",
            //            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            //            {
            //                Name = "Joann Quintero",
            //                Email = "joann@example.com"
            //            }
            //        });
            //    });

            #endregion Implementaciones del Swagger del program se puede mover aqui

            return services;
        }
    }
}