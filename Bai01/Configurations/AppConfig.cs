using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Bai_01.Configurations;

public class AppConfig
{
    public ConnectionStringsConfig ConnectionStrings { get; set; } = new();
}

public class ConnectionStringsConfig
{
    public string DefaultConnection { get; set; } = string.Empty;
}

public static class AppConfigManager
{
    private static AppConfig? _current;

    public static AppConfig Current
    {
        get
        {
            if (_current == null)
            {
                Reload();
            }
            return _current!;
        }
    }

    public static void Reload()
    {
        var basePath = AppContext.BaseDirectory;
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";

        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

        var configuration = builder.Build();
        _current = configuration.Get<AppConfig>() ?? new AppConfig();
    }

    public static string ConnectionString => Current.ConnectionStrings.DefaultConnection;
}
