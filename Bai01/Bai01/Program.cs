using System;
using System.IO;
using System.Windows.Forms;
using Serilog;

namespace Bai_01
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Thiết lập Serilog ghi file theo ngày vào thư mục Logs cạnh file .exe
            var logsDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");
            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }

            var logFilePath = Path.Combine(logsDirectory, "log-.txt");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(
                    path: logFilePath,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();

            try
            {
                Log.Information("Ứng dụng Sổ chi tiêu cá nhân khởi động.");
                ApplicationConfiguration.Initialize();
                Application.Run(new FormChiTieu());
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Ứng dụng gặp sự cố không thể tiếp tục.");
                MessageBox.Show("Đã xảy ra lỗi nghiêm trọng: " + ex.Message, "Lỗi ứng dụng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Log.Information("Ứng dụng kết thúc.");
                Log.CloseAndFlush();
            }
        }
    }
}