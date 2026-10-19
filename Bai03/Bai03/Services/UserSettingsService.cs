using System;
using System.IO;
using System.Text.Json;

namespace Bai03.Services;

public class UserSettings
{
    public string Language { get; set; } = "vi"; // "vi" hoặc "en"
}

public static class UserSettingsService
{
    private static readonly string SettingsFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "usersettings.json"
    );

    public static UserSettings LoadSettings()
    {
        try
        {
            if (File.Exists(SettingsFilePath))
            {
                string json = File.ReadAllText(SettingsFilePath);
                var settings = JsonSerializer.Deserialize<UserSettings>(json);
                if (settings != null && (settings.Language == "vi" || settings.Language == "en"))
                {
                    return settings;
                }
            }
        }
        catch
        {
            // Nếu có lỗi đọc file, fallback về mặc định
        }

        return new UserSettings { Language = "vi" };
    }

    public static void SaveSettings(UserSettings settings)
    {
        try
        {
            string dir = Path.GetDirectoryName(SettingsFilePath)!;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFilePath, json);
        }
        catch
        {
            // Tránh crash nếu không có quyền ghi
        }
    }
}
