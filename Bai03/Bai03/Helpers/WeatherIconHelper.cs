using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Bai03.Helpers;

public static class WeatherIconHelper
{
    public static Image CreateWeatherIcon(int? weatherCode, int width = 80, int height = 80)
    {
        var bmp = new Bitmap(width, height);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);

            if (!weatherCode.HasValue)
            {
                // Mặc định hoặc không rõ: icon dấu hỏi mây mờ
                DrawCloud(g, 10, 20, 60, 36, Color.FromArgb(180, 190, 200));
                return bmp;
            }

            int code = weatherCode.Value;

            if (code == 0) // Trời quang
            {
                DrawSun(g, width / 2, height / 2, 22, Color.FromArgb(255, 179, 0));
            }
            else if (code >= 1 && code <= 3) // Có mây
            {
                DrawSun(g, width / 2 + 12, height / 2 - 12, 14, Color.FromArgb(255, 179, 0));
                DrawCloud(g, 10, 26, 58, 34, Color.FromArgb(170, 195, 215));
            }
            else if (code == 45 || code == 48) // Sương mù
            {
                DrawCloud(g, 12, 15, 56, 32, Color.FromArgb(185, 195, 205));
                using (var pen = new Pen(Color.FromArgb(160, 175, 190), 3f))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    g.DrawLine(pen, 15, 55, 65, 55);
                    g.DrawLine(pen, 20, 63, 60, 63);
                    g.DrawLine(pen, 25, 71, 55, 71);
                }
            }
            else if ((code >= 51 && code <= 67) || (code >= 80 && code <= 82)) // Mưa
            {
                DrawCloud(g, 10, 14, 60, 36, Color.FromArgb(110, 130, 150));
                // Giọt mưa
                using (var pen = new Pen(Color.FromArgb(52, 152, 219), 3f))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    g.DrawLine(pen, 22, 54, 18, 66);
                    g.DrawLine(pen, 36, 54, 32, 66);
                    g.DrawLine(pen, 50, 54, 46, 66);
                    g.DrawLine(pen, 62, 54, 58, 66);
                }
            }
            else if (code >= 95 && code <= 99) // Dông
            {
                DrawCloud(g, 10, 12, 60, 36, Color.FromArgb(80, 95, 115));
                // Tia sét
                using (var brush = new SolidBrush(Color.FromArgb(241, 196, 15)))
                {
                    PointF[] bolt = new PointF[]
                    {
                        new PointF(38, 48),
                        new PointF(46, 48),
                        new PointF(34, 62),
                        new PointF(42, 62),
                        new PointF(30, 76),
                        new PointF(38, 60),
                        new PointF(30, 60)
                    };
                    g.FillPolygon(brush, bolt);
                }
            }
            else // Các loại khác (ví dụ tuyết)
            {
                DrawCloud(g, 10, 20, 60, 36, Color.FromArgb(150, 170, 190));
            }
        }
        return bmp;
    }

    private static void DrawSun(Graphics g, float cx, float cy, float radius, Color color)
    {
        using (var brush = new SolidBrush(color))
        {
            g.FillEllipse(brush, cx - radius, cy - radius, radius * 2, radius * 2);
        }

        using (var pen = new Pen(color, 2.5f))
        {
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            for (int i = 0; i < 8; i++)
            {
                double angle = i * Math.PI / 4;
                float x1 = cx + (float)(Math.Cos(angle) * (radius + 4));
                float y1 = cy + (float)(Math.Sin(angle) * (radius + 4));
                float x2 = cx + (float)(Math.Cos(angle) * (radius + 9));
                float y2 = cy + (float)(Math.Sin(angle) * (radius + 9));
                g.DrawLine(pen, x1, y1, x2, y2);
            }
        }
    }

    private static void DrawCloud(Graphics g, float x, float y, float w, float h, Color color)
    {
        using (var brush = new SolidBrush(color))
        {
            // Vẽ các khối tròn bồng bềnh tạo nên đám mây
            g.FillEllipse(brush, x + w * 0.15f, y + h * 0.1f, w * 0.45f, h * 0.7f);
            g.FillEllipse(brush, x + w * 0.45f, y, w * 0.45f, h * 0.8f);
            g.FillEllipse(brush, x + w * 0.05f, y + h * 0.35f, w * 0.4f, h * 0.6f);
            g.FillEllipse(brush, x + w * 0.55f, y + h * 0.35f, w * 0.4f, h * 0.6f);
            g.FillRectangle(brush, x + w * 0.2f, y + h * 0.45f, w * 0.6f, h * 0.5f);
        }
    }
}
