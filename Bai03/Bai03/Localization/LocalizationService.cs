using System;
using System.Collections.Generic;
using System.Globalization;

namespace Bai03.Localization;

public static class LocalizationService
{
    public static string CurrentLanguage { get; set; } = "vi";

    private static readonly CultureInfo ViCulture = new CultureInfo("vi-VN");
    private static readonly CultureInfo EnCulture = new CultureInfo("en-US");

    public static CultureInfo CurrentCulture => CurrentLanguage == "en" ? EnCulture : ViCulture;

    public static string DateFormat => CurrentLanguage == "en" ? "MM/dd/yyyy" : "dd/MM/yyyy";

    public static string FormatMoney(decimal amount)
    {
        if (CurrentLanguage == "en")
        {
            return amount.ToString("C0", EnCulture);
        }
        else
        {
            return amount.ToString("#,##0", ViCulture) + " đ";
        }
    }

    public static string FormatDate(DateOnly date)
    {
        return date.ToString(DateFormat, CurrentCulture);
    }

    private static readonly Dictionary<string, (string vi, string en)> Strings = new()
    {
        { "AppTitle", ("Kế hoạch dã ngoại cuối tuần", "Weekend Picnic Planner") },
        { "HeaderTitle", ("Kế hoạch dã ngoại cuối tuần", "Weekend Picnic Planner") },
        { "LanguageLabel", ("Ngôn ngữ", "Language") },
        { "TripInfoGroup", ("Thông tin chuyến đi", "Trip Information") },
        { "TripName", ("Tên chuyến", "Trip Name") },
        { "Destination", ("Địa điểm", "Destination") },
        { "DepartureDate", ("Ngày đi", "Departure Date") },
        { "PeopleCount", ("Số người", "People") },
        { "Budget", ("Ngân sách", "Budget") },
        { "Vehicle", ("Phương tiện", "Vehicle") },
        { "Completed", ("Đã hoàn thành", "Completed") },
        { "Notes", ("Ghi chú", "Notes") },
        { "BtnAdd", ("Thêm", "Add") },
        { "BtnUpdate", ("Sửa", "Update") },
        { "BtnDelete", ("Xóa", "Delete") },
        { "BtnReset", ("Làm mới", "Reset") },
        { "BtnCheckWeather", ("Xem thời tiết", "Check Weather") },
        { "WeatherTitleDefault", ("Dự báo thời tiết", "Weather Forecast") },
        { "WeatherTitleFormat", ("Dự báo thời tiết ngày {0} - {1}", "Weather forecast on {0} - {1}") },
        { "WeatherTempFormat", ("Nhiệt độ: {0}°C - {1}°C", "Temperature: {0}°C - {1}°C") },
        { "WeatherRainFormat", ("Khả năng mưa: {0}%", "Rain probability: {0}%") },
        { "WeatherRainWarning", ("Nên mang áo mưa hoặc cân nhắc dời lịch", "Should bring a raincoat or consider rescheduling") },
        { "WeatherGoodNotice", ("Thời tiết thuận lợi cho chuyến đi", "Favorable weather for the trip") },
        { "WeatherOutOfRange", ("Chưa có dữ liệu dự báo (chỉ hỗ trợ trong vòng 16 ngày tới)", "No forecast data available (only within 16 days)") },
        { "WeatherLoading", ("Đang tải dữ liệu thời tiết...", "Loading weather data...") },
        { "SearchPlaceholder", ("Tìm địa điểm...", "Search destination...") },
        { "SearchFilterOnlyIncomplete", ("Chỉ hiện chuyến chưa hoàn thành", "Show only incomplete trips") },
        { "BtnSearch", ("Tìm", "Search") },
        { "BtnShowAll", ("Tất cả", "Show All") },
        { "ColId", ("Mã", "ID") },
        { "ColTripName", ("Tên chuyến", "Trip Name") },
        { "ColDestination", ("Địa điểm", "Destination") },
        { "ColDepartureDate", ("Ngày đi", "Departure Date") },
        { "ColPeople", ("Số người", "People") },
        { "ColBudget", ("Ngân sách", "Budget") },
        { "ColVehicle", ("Phương tiện", "Vehicle") },
        { "ColCompleted", ("Hoàn thành", "Completed") },
        { "MsgSelectTripFirst", ("Vui lòng chọn một chuyến đi từ danh sách.", "Please select a trip from the list.") },
        { "MsgEnterTripName", ("Tên chuyến đi không được để trống.", "Trip name cannot be empty.") },
        { "MsgEnterDestination", ("Địa điểm không được để trống.", "Destination cannot be empty.") },
        { "MsgInvalidDepartureDate", ("Ngày đi không được nhỏ hơn hôm nay khi thêm mới chuyến đi.", "Departure date cannot be in the past when creating a new trip.") },
        { "MsgInvalidPeople", ("Số người tham gia phải lớn hơn hoặc bằng 1.", "Number of people must be at least 1.") },
        { "MsgInvalidBudget", ("Ngân sách không được nhỏ hơn 0.", "Budget cannot be negative.") },
        { "MsgConfirmDelete", ("Bạn có chắc chắn muốn xóa chuyến đi \"{0}\" không?", "Are you sure you want to delete trip \"{0}\"?") },
        { "MsgConfirmDeleteTitle", ("Xác nhận xóa", "Confirm Delete") },
        { "MsgAddSuccess", ("Thêm chuyến đi thành công!", "Trip added successfully!") },
        { "MsgUpdateSuccess", ("Cập nhật chuyến đi thành công!", "Trip updated successfully!") },
        { "MsgDeleteSuccess", ("Xóa chuyến đi thành công!", "Trip deleted successfully!") },
        { "MsgNotification", ("Thông báo", "Notification") },
        { "MsgError", ("Lỗi", "Error") },
        { "VehicleMotorbike", ("Xe máy", "Motorbike") },
        { "VehicleCar", ("Ô tô", "Car") },
        { "VehicleBus", ("Xe khách", "Bus") },
        { "VehicleTrain", ("Tàu hỏa", "Train") }
    };

    public static string Get(string key)
    {
        if (Strings.TryGetValue(key, out var pair))
        {
            return CurrentLanguage == "en" ? pair.en : pair.vi;
        }
        return key;
    }

    public static string GetVehicleLocalized(string vehicle)
    {
        return vehicle switch
        {
            "Xe máy" or "Motorbike" => CurrentLanguage == "en" ? "Motorbike" : "Xe máy",
            "Ô tô" or "Car" => CurrentLanguage == "en" ? "Car" : "Ô tô",
            "Xe khách" or "Bus" => CurrentLanguage == "en" ? "Bus" : "Xe khách",
            "Tàu hỏa" or "Train" => CurrentLanguage == "en" ? "Train" : "Tàu hỏa",
            _ => vehicle
        };
    }

    public static string GetVehicleDbValue(string displayVehicle)
    {
        return displayVehicle switch
        {
            "Motorbike" => "Xe máy",
            "Car" => "Ô tô",
            "Bus" => "Xe khách",
            "Train" => "Tàu hỏa",
            _ => displayVehicle
        };
    }
}
