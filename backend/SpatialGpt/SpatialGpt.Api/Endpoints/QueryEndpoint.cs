using SpatialGpt.Api.Models;
using SpatialGpt.Api.Services;

namespace SpatialGpt.Api.Endpoints;

public static class QueryEndpoint
{
    public static void MapQueryEndpoints(this WebApplication app)
    {
        app.MapPost("/api/query", async (QueryRequest request, QueryParser parser, PropertyQueryService queryService) =>
        {
            if (string.IsNullOrWhiteSpace(request.Text))
                return Results.BadRequest("Query text cannot be empty.");

            var filter = parser.Parse(request.Text);
            var result = await queryService.QueryAsync(filter);

            return Results.Ok(result);
        })
        .WithName("Query");
    }
}