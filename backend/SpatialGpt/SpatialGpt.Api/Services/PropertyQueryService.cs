using Microsoft.EntityFrameworkCore;
using SpatialGpt.Api.Data;
using SpatialGpt.Api.Models;
using System.Text.Json;

namespace SpatialGpt.Api.Services;

public class PropertyQueryService
{
    private readonly AppDbContext _context;

    public PropertyQueryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<object> QueryAsync(QueryFilter filter)
    {
        var query = _context.Properties.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Type))
            query = query.Where(p => p.Type == filter.Type);

        if (filter.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);

        var properties = await query
            .Take(200)
            .ToListAsync();

        // Build GeoJSON FeatureCollection manually
        var features = properties.Select(p => new
        {
            type = "Feature",
            geometry = p.Geom == null ? null : new
            {
                type = "Point",
                coordinates = new[] { p.Geom.X, p.Geom.Y }
            },
            properties = new
            {
                id = p.Id,
                title = p.Title,
                type = p.Type,
                price = p.Price,
                city = p.City,
                area = p.Area,
                furnished = p.Furnished
            }
        });

        return new
        {
            type = "FeatureCollection",
            features
        };
    }
}