using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RH_GRH
{
    /// <summary>
    /// Génère des icônes modernes vectorielles
    /// </summary>
    public static class ModernIcons
    {
        public static Bitmap CreateUserIcon(int size, Color color)
        {
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                using (Pen pen = new Pen(color, size / 12f))
                {
                    // Tête
                    int headSize = size / 3;
                    g.DrawEllipse(pen, size / 2 - headSize / 2, size / 5, headSize, headSize);

                    // Corps
                    GraphicsPath bodyPath = new GraphicsPath();
                    bodyPath.AddArc(size / 6, size / 2, size * 2 / 3, size / 2, 0, 180);
                    g.DrawPath(pen, bodyPath);
                }
            }
            return bmp;
        }

        public static Bitmap CreateLockIcon(int size, Color color)
        {
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                using (Pen pen = new Pen(color, size / 12f))
                {
                    // Cadenas (partie haute - arc)
                    g.DrawArc(pen, size / 4, size / 6, size / 2, size / 2, 180, 180);

                    // Cadenas (partie basse - rectangle)
                    using (SolidBrush brush = new SolidBrush(color))
                    {
                        RectangleF lockBody = new RectangleF(size / 5, size / 2, size * 3 / 5, size * 2 / 5);
                        g.FillRoundedRectangle(brush, lockBody, size / 10);

                        // Trou de serrure
                        using (SolidBrush keyholeBrush = new SolidBrush(Color.FromArgb(30, 41, 59)))
                        {
                            g.FillEllipse(keyholeBrush, size * 9 / 20, size * 11 / 20, size / 10, size / 10);
                            g.FillRectangle(keyholeBrush, size * 9 / 20 + size / 40, size * 13 / 20, size / 20, size / 8);
                        }
                    }
                }
            }
            return bmp;
        }

        public static Bitmap CreateCheckIcon(int size, Color color)
        {
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                using (Pen pen = new Pen(color, size / 8f))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;

                    Point[] checkPoints = new Point[]
                    {
                        new Point(size / 5, size / 2),
                        new Point(size * 2 / 5, size * 3 / 4),
                        new Point(size * 4 / 5, size / 4)
                    };

                    g.DrawLines(pen, checkPoints);
                }
            }
            return bmp;
        }

        // Extension method pour dessiner un rectangle arrondi
        private static void FillRoundedRectangle(this Graphics g, Brush brush, RectangleF bounds, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            g.FillPath(brush, path);
        }
    }
}
