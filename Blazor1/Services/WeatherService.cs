using System.Net.Http.Json;
using Blazor1.Models;

namespace Blazor1.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private const string ApiBaseUrl = "https://weather.tsukumijima.net/api/forecast";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://weather.tsukumijima.net/");
    }

    public async Task<WeatherApiResponse?> GetWeatherForecastAsync(string cityId)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<WeatherApiResponse>($"{ApiBaseUrl}?city={cityId}");
            return response;
        }
        catch (HttpRequestException)
        {
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static List<CityInfo> GetMajorCities()
    {
        return new List<CityInfo>
        {
            new CityInfo { Id = "130010", Name = "東京", Prefecture = "東京都" },
            new CityInfo { Id = "270000", Name = "大阪", Prefecture = "大阪府" },
            new CityInfo { Id = "230010", Name = "名古屋", Prefecture = "愛知県" },
            new CityInfo { Id = "400010", Name = "福岡", Prefecture = "福岡県" },
            new CityInfo { Id = "016010", Name = "札幌", Prefecture = "北海道" },
            new CityInfo { Id = "040010", Name = "仙台", Prefecture = "宮城県" },
            new CityInfo { Id = "340010", Name = "広島", Prefecture = "広島県" },
            new CityInfo { Id = "260010", Name = "京都", Prefecture = "京都府" },
            new CityInfo { Id = "140010", Name = "横浜", Prefecture = "神奈川県" },
            new CityInfo { Id = "280010", Name = "神戸", Prefecture = "兵庫県" },
            new CityInfo { Id = "110010", Name = "さいたま", Prefecture = "埼玉県" },
            new CityInfo { Id = "220010", Name = "静岡", Prefecture = "静岡県" },
            new CityInfo { Id = "380010", Name = "松山", Prefecture = "愛媛県" },
            new CityInfo { Id = "390010", Name = "高知", Prefecture = "高知県" },
            new CityInfo { Id = "370000", Name = "高松", Prefecture = "香川県" }
        };
    }
}
