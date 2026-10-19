using System.Net.Http.Json;
using System.Text.Json;
using Serilog;

namespace QL_NhaTro.Services;

public sealed class AddressApiClient
{
    private static readonly HttpClient Client = new()
    {
        BaseAddress = new Uri("https://provinces.open-api.vn/api/v2/"),
        Timeout = TimeSpan.FromSeconds(10)
    };

    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<IReadOnlyList<AddressItem>> GetProvincesAsync(CancellationToken cancellationToken = default)
    {
        using var response = await Client.GetAsync("", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<AddressItem>>(JsonOptions, cancellationToken)
            ?? throw new JsonException("API không trả về danh sách tỉnh/thành.");
    }

    public async Task<IReadOnlyList<AddressItem>> GetWardsAsync(int provinceCode, CancellationToken cancellationToken = default)
    {
        using var response = await Client.GetAsync($"p/{provinceCode}?depth=2", cancellationToken);
        response.EnsureSuccessStatusCode();
        var province = await response.Content.ReadFromJsonAsync<AddressProvince>(JsonOptions, cancellationToken)
            ?? throw new JsonException("API không trả về thông tin tỉnh/thành.");
        return province.Wards ?? [];
    }

    public void LogApiWarning(Exception exception, string operation) =>
        Log.Warning(exception, "Không tải được dữ liệu địa chỉ từ API: {Operation}", operation);
}

public class AddressItem
{
    public int Code { get; set; }
    public string Name { get; set; } = "";
    public override string ToString() => Name;
}

public sealed class AddressProvince : AddressItem
{
    public List<AddressItem>? Wards { get; set; }
}
