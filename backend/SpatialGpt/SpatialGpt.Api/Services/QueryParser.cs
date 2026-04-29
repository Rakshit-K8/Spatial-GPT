using System.Text.RegularExpressions;
using SpatialGpt.Api.Models;

namespace SpatialGpt.Api.Services;

public class QueryParser
{
    public QueryFilter Parse(string text)
    {
        var filter = new QueryFilter();

        // Match property type: 1BHK, 2BHK, 3BHK
        var typeMatch = Regex.Match(text, @"[123]BHK", RegexOptions.IgnoreCase);
        if (typeMatch.Success)
            filter.Type = typeMatch.Value.ToUpper();

        // Match price in Lakhs: "under 50L", "below 40 lakh", "upto 60L"
        var lakhMatch = Regex.Match(text, @"(?:under|below|upto)\s*(\d+)\s*(?:L|lakh)", RegexOptions.IgnoreCase);
        if (lakhMatch.Success)
            filter.MaxPrice = long.Parse(lakhMatch.Groups[1].Value) * 100_000;

        // Match price in Crores: "under 1Cr", "below 2 crore"
        var croreMatch = Regex.Match(text, @"(?:under|below|upto)\s*(\d+)\s*(?:Cr|crore)", RegexOptions.IgnoreCase);
        if (croreMatch.Success)
            filter.MaxPrice = long.Parse(croreMatch.Groups[1].Value) * 10_000_000;

        return filter;
    }
}