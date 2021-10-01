using System;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using RaspberryPresenceStatus.Models.Enuns;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RaspberryPresenceStatus.SwaggerFilters
{
    public class PresenceStatusFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            parameter.Schema.Enum = Enum.GetNames(typeof(PresenceStatusEnum))
                                            .Cast<IOpenApiAny>()
                                            .ToList();
        }
    }
}