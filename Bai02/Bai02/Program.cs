using Microsoft.Extensions.Logging;
using Serilog;

namespace QL_NhaTro;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        var logDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "QL_NhaTro", "logs");
        Directory.CreateDirectory(logDirectory);
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.File(
                Path.Combine(logDirectory, "ql-nhatro-.log"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 14,
                shared: true,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => ReportFatalError(e.Exception, "WinForms UI thread");
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            var exception = e.ExceptionObject as Exception
                ?? new Exception($"Unhandled non-Exception object: {e.ExceptionObject}");
            ReportFatalError(exception, "AppDomain");
        };

        try
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormKhachThue());
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void ReportFatalError(Exception exception, string source)
    {
        try
        {
            Log.Fatal(exception, "Unhandled application error from {Source}", source);
        }
        catch
        {
            // Logging must never cause a second failure while handling the first one.
        }

        try
        {
            MessageBox.Show(
                "Đã xảy ra lỗi, thông tin đã được ghi lại. Bạn có thể tiếp tục thao tác.",
                "Quản lý nhà trọ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        catch
        {
            // AppDomain shutdown exceptions can arrive when a UI message box is no longer available.
        }
    }
}
