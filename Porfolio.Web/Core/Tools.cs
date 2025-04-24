using System.Text.Json;

namespace Porfolio.Web.Core;

public static class Tools
{
    public static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public static string DateTimeConverterToCvDate(DateTime? date)
    {
        if(date == null) return string.Empty;

        return date?.ToString("MMM yyyy", System.Globalization.CultureInfo.InvariantCulture)!;
    }
}
