using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Linq;

namespace PruebaUdd.Swagger
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerExtension
    {
        /// <summary>
        /// Método que inicializa el componente swagger e incorpora la documentación y versiones
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSwaggerExtension(this IServiceCollection services, IConfiguration configuration)
        {
            IConfiguration option = configuration.GetSection(SwaggerSettings.SettingName);
            services.Configure<SwaggerSettings>(option);
            IServiceProvider provider = services.BuildServiceProvider();
            SwaggerSettings _swaggerSettings = GetConfiguration(provider);

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.ApiVersionReader = new HeaderApiVersionReader(_swaggerSettings?.HeadersVersions?.ToArray());
                o.AssumeDefaultVersionWhenUnspecified = true;
                if (!string.IsNullOrEmpty(_swaggerSettings?.SwaggerVersionConfiguration?.FirstOrDefault()?.Version))
                {
                    o.DefaultApiVersion = ApiVersion.Parse(_swaggerSettings?.SwaggerVersionConfiguration?.FirstOrDefault()?.Version);
                }
            });
            services.AddSwaggerGen(c =>
            {
                foreach (SwaggerVersionConfiguration version in _swaggerSettings?.SwaggerVersionConfiguration)
                {
                    c.SwaggerDoc(version.Version, _swaggerSettings.GetInfo(version?.Version));
                }
                c.DescribeAllParametersInCamelCase();
                c.EnableAnnotations();
                c.DocumentFilter<AddVersionHeader>();
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            });
        }

        /// <summary>
        /// MEthod to implements definition API swagger UI
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void UseSwaggerExtension(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            IServiceProvider provider = app.ApplicationServices;
            SwaggerSettings _swaggerSettings = GetConfiguration(provider);
            app.UsePathBase($"/{_swaggerSettings?.ServerPrefix}");
            app.UseStaticFiles();
            _ = app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> {
                        new OpenApiServer {
                            Description ="Uri Base Api",
                            Url = $"{_swaggerSettings.Scheme ?? httpReq.Scheme}://{httpReq.Host.Value}"
                        }
                    };
                });
            });
            app.UseSwaggerUI(c =>
            {
                foreach (SwaggerVersionConfiguration version in _swaggerSettings?.SwaggerVersionConfiguration)
                {
                    c.SwaggerEndpoint(version?.EndpointUrl, version?.EndpointDescription);
                }
                c.DisplayOperationId();
            });
        }

        /// <summary>
        /// method to obtain the configuration from the file
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        private static SwaggerSettings GetConfiguration(IServiceProvider provider)
        {
            var swaggerConfigurationOption = provider.GetRequiredService<IOptions<SwaggerSettings>>();
            return swaggerConfigurationOption.Value;
        }
    }
}

