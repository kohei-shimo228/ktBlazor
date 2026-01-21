using System.Text.Json.Serialization;

namespace Blazor1.Models;

public class WeatherApiResponse
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("forecasts")]
    public List<Forecast>? Forecasts { get; set; }

    [JsonPropertyName("location")]
    public Location? Location { get; set; }

    [JsonPropertyName("description")]
    public Description? Description { get; set; }
}

public class Forecast
{
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("dateLabel")]
    public string? DateLabel { get; set; }

    [JsonPropertyName("telop")]
    public string? Telop { get; set; }

    [JsonPropertyName("detail")]
    public Detail? Detail { get; set; }

    [JsonPropertyName("temperature")]
    public Temperature? Temperature { get; set; }

    [JsonPropertyName("chanceOfRain")]
    public ChanceOfRain? ChanceOfRain { get; set; }

    [JsonPropertyName("image")]
    public Image? Image { get; set; }
}

public class Detail
{
    [JsonPropertyName("weather")]
    public string? Weather { get; set; }

    [JsonPropertyName("wind")]
    public string? Wind { get; set; }

    [JsonPropertyName("wave")]
    public string? Wave { get; set; }
}

public class Temperature
{
    [JsonPropertyName("min")]
    public TemperatureValue? Min { get; set; }

    [JsonPropertyName("max")]
    public TemperatureValue? Max { get; set; }
}

public class TemperatureValue
{
    [JsonPropertyName("celsius")]
    public string? Celsius { get; set; }

    [JsonPropertyName("fahrenheit")]
    public string? Fahrenheit { get; set; }
}

public class ChanceOfRain
{
    [JsonPropertyName("T00_06")]
    public string? T00_06 { get; set; }

    [JsonPropertyName("T06_12")]
    public string? T06_12 { get; set; }

    [JsonPropertyName("T12_18")]
    public string? T12_18 { get; set; }

    [JsonPropertyName("T18_24")]
    public string? T18_24 { get; set; }
}

public class Image
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }
}

public class Location
{
    [JsonPropertyName("area")]
    public string? Area { get; set; }

    [JsonPropertyName("prefecture")]
    public string? Prefecture { get; set; }

    [JsonPropertyName("district")]
    public string? District { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }
}

public class Description
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("publicTime")]
    public string? PublicTime { get; set; }
}

public class CityInfo
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Prefecture { get; set; } = string.Empty;
}
