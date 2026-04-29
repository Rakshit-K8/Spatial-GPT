using NetTopologySuite.Geometries;

namespace SpatialGpt.Api.Models;

public class Property
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public long Price { get; set; }
    public string City { get; set; } = string.Empty;
    public string? Area { get; set; }
    public bool Furnished { get; set; }
    public Point? Geom { get; set; }
}