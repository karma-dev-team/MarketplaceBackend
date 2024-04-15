using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KarmaMarketplace.Presentation.Web.Schemas.Filters
{
    public class StreamSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(Stream))
            {
                schema.Type = "string";
                schema.Format = "binary";
            }
        }
    }
}
