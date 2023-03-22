using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PruebaUdd.Swagger
{
    [ExcludeFromCodeCoverage]
    public class SwaggerSettings
    {
        public static string SettingName = "SwaggerConfiguration";
        public string Title { get; set; }
        public string ContactName { get; set; }
        public string ContactUrl { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string ServerPrefix { get; set; }
        public string Scheme { get; set; }
        public IEnumerable<string> HeadersVersions { get; set; }
        public IEnumerable<SwaggerVersionConfiguration> SwaggerVersionConfiguration { get; set; }

        /// <summary>
        /// Información de contacto para la definición swagger
        /// </summary>
        /// <param name="version"></param>
        /// <returns>OpenApiInfo</returns>
        public OpenApiInfo GetInfo(string version)
        {
            OpenApiInfo info = new OpenApiInfo
            {
                Contact = new OpenApiContact
                {
                    Email = Email ?? "arivera@ideasimple.cl",
                    Name = ContactName ?? "Alex Rivera",
                    Url = new Uri(ContactUrl ?? @"http://www.ideasimple.cl")
                },
                Description = Description,
                Title = Title,
                Version = version
            };
            return info;
        }
    }
}