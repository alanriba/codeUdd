using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PruebaUdd.Swagger
{
    [ExcludeFromCodeCoverage]
    public class AddVersionHeader : IDocumentFilter
    {
        /// <summary>
        /// Método para agregar parámetros al Header del servicio
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            string version = swaggerDoc.Info.Version;
            List<OperationType> operationTypes = Enum.GetValues(typeof(OperationType)).OfType<OperationType>().ToList();
            foreach (KeyValuePair<string, OpenApiPathItem> item in swaggerDoc.Paths)
            {
                var value = item.Value;
                foreach (OperationType type in operationTypes)
                {
                    if (value.Operations.ContainsKey(type))
                    {
                        if (value.Operations[type].Parameters == null)
                        {
                            value.Operations[type].Parameters = new List<OpenApiParameter>();
                        }
                        value.Operations[type].Parameters.Add(new OpenApiParameter
                        {
                            Name = "Api-Version",
                            In = ParameterLocation.Header,
                            Description = "API version",
                            Required = false,
                            Schema = new OpenApiSchema
                            {
                                Type = "string",
                                Default = new OpenApiString(version)
                            }
                        });
                    }
                }
            }
        }
    }

}