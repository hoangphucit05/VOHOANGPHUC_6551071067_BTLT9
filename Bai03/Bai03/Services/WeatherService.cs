using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bai03.Services;

public class WeatherResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Suggestion { get; set; }
    public bool OutOfRange { get; set; }
    public string? LocationName { get; set; }
    public DateOnly TargetDate { get; set; }
    public int? WeatherCode { get; set; }
    public string WeatherDescriptionVi { get; set; } = string.Empty;
    public string WeatherDescriptionEn { get; set; } = string.Empty;
    public double? TempMin { get; set; }
    public double? TempMax { get; set; }
    public int? PrecipProbability { get; set; }
    public bool ShouldWarnRain => PrecipProbability.HasValue && PrecipProbability.Value >= 60;

    public string GetDescription(string lang) => lang == "en" ? WeatherDescriptionEn : WeatherDescriptionVi;
}

public static class WeatherService
{
    private static readonly HttpClient HttpClient;

    static WeatherService()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };

        HttpClient = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromSeconds(8)
        };
        HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd("OutdoorTripPlanner/1.0");
    }

    public static string RemoveDiacritics(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return text;
        string normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (char c in normalized)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC).Replace("đ", "d").Replace("Đ", "D");
    }

    public static async Task<WeatherResult> GetForecastAsync(string diaDiem, DateOnly ngayDi)
    {
        var result = new WeatherResult
        {
            TargetDate = ngayDi,
            LocationName = diaDiem
        };

        if (string.IsNullOrWhiteSpace(diaDiem))
        {
            result.Success = false;
            result.ErrorMessage = "Vui lòng nhập địa điểm trước khi xem dự báo thời tiết.";
            return result;
        }

        try
        {
            // Bước 1: Geocoding để lấy tọa độ latitude, longitude
            (double? lat, double? lon, string? resolvedName) = await ResolveCoordinatesAsync(diaDiem);

            if (!lat.HasValue || !lon.HasValue)
            {
                // Thử với tên không dấu nếu có dấu tiếng Việt
                string unaccented = RemoveDiacritics(diaDiem);
                if (!string.Equals(unaccented, diaDiem, StringComparison.OrdinalIgnoreCase))
                {
                    (lat, lon, resolvedName) = await ResolveCoordinatesAsync(unaccented);
                }
            }

            if (!lat.HasValue || !lon.HasValue)
            {
                result.Success = false;
                result.ErrorMessage = $"Không tìm thấy địa điểm \"{diaDiem}\".";
                result.Suggestion = "Gợi ý: Hãy thử nhập tên không dấu (ví dụ: Da Lat, Vung Tau, Hoi An, Sapa...)";
                return result;
            }

            // Bước 2: Gọi Forecast API với 16 ngày dự báo
            // Lưu ý: Domain chuẩn là open-meteo.com, hỗ trợ fallback cả openmeteo.com
            string latStr = lat.Value.ToString(CultureInfo.InvariantCulture);
            string lonStr = lon.Value.ToString(CultureInfo.InvariantCulture);
            string queryParams = $"latitude={latStr}&longitude={lonStr}&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_probability_max&timezone=auto&forecast_days=16";

            string forecastUrl = $"https://api.open-meteo.com/v1/forecast?{queryParams}";
            string? jsonForecast = null;

            try
            {
                jsonForecast = await HttpClient.GetStringAsync(forecastUrl);
            }
            catch
            {
                // Fallback nếu máy chủ dùng openmeteo.com
                string fallbackUrl = $"https://api.openmeteo.com/v1/forecast?{queryParams}";
                jsonForecast = await HttpClient.GetStringAsync(fallbackUrl);
            }

            using var doc = JsonDocument.Parse(jsonForecast);
            var root = doc.RootElement;

            if (!root.TryGetProperty("daily", out var daily))
            {
                result.Success = false;
                result.ErrorMessage = "Dữ liệu dự báo thời tiết không khả dụng từ máy chủ.";
                return result;
            }

            var timeArray = daily.GetProperty("time");
            int foundIndex = -1;
            string targetDateStr = ngayDi.ToString("yyyy-MM-dd");

            int index = 0;
            foreach (var item in timeArray.EnumerateArray())
            {
                if (item.GetString() == targetDateStr)
                {
                    foundIndex = index;
                    break;
                }
                index++;
            }

            if (foundIndex == -1)
            {
                result.Success = true;
                result.OutOfRange = true;
                result.ErrorMessage = "Chưa có dữ liệu dự báo (chỉ hỗ trợ trong vòng 16 ngày tới).";
                return result;
            }

            // Lấy các thông số thời tiết tại chỉ số foundIndex
            var weatherCodeArray = daily.GetProperty("weather_code");
            var tempMaxArray = daily.GetProperty("temperature_2m_max");
            var tempMinArray = daily.GetProperty("temperature_2m_min");
            var precipProbArray = daily.GetProperty("precipitation_probability_max");

            // Đọc an toàn Weather Code
            if (weatherCodeArray[foundIndex].ValueKind == JsonValueKind.Number &&
                weatherCodeArray[foundIndex].TryGetInt32(out int code))
            {
                result.WeatherCode = code;
            }

            // Đọc an toàn Nhiệt độ Max
            if (tempMaxArray[foundIndex].ValueKind == JsonValueKind.Number &&
                tempMaxArray[foundIndex].TryGetDouble(out double tMax))
            {
                result.TempMax = tMax;
            }

            // Đọc an toàn Nhiệt độ Min
            if (tempMinArray[foundIndex].ValueKind == JsonValueKind.Number &&
                tempMinArray[foundIndex].TryGetDouble(out double tMin))
            {
                result.TempMin = tMin;
            }

            // Đọc an toàn Khả năng mưa
            if (precipProbArray[foundIndex].ValueKind == JsonValueKind.Number)
            {
                if (precipProbArray[foundIndex].TryGetInt32(out int pInt))
                {
                    result.PrecipProbability = pInt;
                }
                else if (precipProbArray[foundIndex].TryGetDouble(out double pDouble))
                {
                    result.PrecipProbability = (int)Math.Round(pDouble);
                }
            }
            else
            {
                result.PrecipProbability = 0;
            }

            if (result.WeatherCode.HasValue)
            {
                (result.WeatherDescriptionVi, result.WeatherDescriptionEn) = GetWeatherDescriptions(result.WeatherCode.Value);
            }
            else
            {
                result.WeatherDescriptionVi = "Có mây";
                result.WeatherDescriptionEn = "Partly cloudy";
            }

            result.Success = true;
            return result;
        }
        catch (TaskCanceledException)
        {
            result.Success = false;
            result.ErrorMessage = "Quá thời gian kết nối (8 giây) tới máy chủ thời tiết. Vui lòng thử lại!";
            return result;
        }
        catch (HttpRequestException)
        {
            result.Success = false;
            result.ErrorMessage = "Không thể kết nối Internet hoặc mạng bị ngắt. Vui lòng kiểm tra lại kết nối mạng!";
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = $"Lỗi khi tải thông tin thời tiết: {ex.Message}";
            return result;
        }
    }

    private static async Task<(double? lat, double? lon, string? name)> ResolveCoordinatesAsync(string query)
    {
        string encoded = Uri.EscapeDataString(query.Trim());

        // Ưu tiên open-meteo.com, fallback openmeteo.com
        string[] urls = new[]
        {
            $"https://geocoding-api.open-meteo.com/v1/search?name={encoded}&count=1&language=vi",
            $"https://geocoding-api.openmeteo.com/v1/search?name={encoded}&count=1&language=vi"
        };

        foreach (var url in urls)
        {
            try
            {
                string json = await HttpClient.GetStringAsync(url);
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("results", out var results) && results.GetArrayLength() > 0)
                {
                    var first = results[0];
                    double lat = first.GetProperty("latitude").GetDouble();
                    double lon = first.GetProperty("longitude").GetDouble();
                    string name = first.TryGetProperty("name", out var nameProp) ? nameProp.GetString() ?? query : query;
                    return (lat, lon, name);
                }
            }
            catch
            {
                // Tiếp tục thử URL tiếp theo
            }
        }

        return (null, null, null);
    }

    private static (string vi, string en) GetWeatherDescriptions(int code)
    {
        return code switch
        {
            0 => ("Trời quang đãng", "Clear sky"),
            1 => ("Hầu như quang đãng", "Mainly clear"),
            2 => ("Có mây rải rác", "Partly cloudy"),
            3 => ("Nhiều mây, u ám", "Overcast"),
            45 => ("Sương mù", "Fog"),
            48 => ("Sương mù đọng băng", "Depositing rime fog"),
            51 => ("Mưa phùn nhẹ", "Light drizzle"),
            53 => ("Mưa phùn vừa", "Moderate drizzle"),
            55 => ("Mưa phùn dày đặc", "Dense drizzle"),
            56 or 57 => ("Mưa phùn băng giá", "Freezing drizzle"),
            61 => ("Mưa rào nhẹ", "Slight rain"),
            63 => ("Mưa vừa", "Moderate rain"),
            65 => ("Mưa to", "Heavy rain"),
            66 or 67 => ("Mưa lạnh buốt", "Freezing rain"),
            71 or 73 or 75 or 77 => ("Tuyết rơi", "Snow fall"),
            80 => ("Mưa rào nhẹ", "Slight rain showers"),
            81 => ("Mưa rào vừa", "Moderate rain showers"),
            82 => ("Mưa rào rất to", "Violent rain showers"),
            85 or 86 => ("Mưa tuyết", "Snow showers"),
            95 => ("Dông bão", "Thunderstorm"),
            96 or 99 => ("Dông bão kèm mưa đá", "Thunderstorm with hail"),
            _ => ("Thời tiết bình thường", "Normal weather")
        };
    }
}
