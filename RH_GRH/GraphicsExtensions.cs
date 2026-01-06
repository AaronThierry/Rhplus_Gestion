using System.Drawing;
using System.Drawing.Drawing2D;

namespace RH_GRH
{
    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null) return;
            if (brush == null) return;

            using (GraphicsPath path = CreateRoundedRectanglePath(bounds, cornerRadius))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPath(brush, path);
            }
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null) return;
            if (pen == null) return;

            using (GraphicsPath path = CreateRoundedRectanglePath(bounds, cornerRadius))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawPath(pen, path);
            }
        }

        public static GraphicsPath CreateRoundedRectanglePath(Rectangle bounds, int cornerRadius)
        {
            int diameter = cornerRadius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (cornerRadius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // Coin supérieur gauche
            path.AddArc(arc, 180, 90);

            // Coin supérieur droit
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Coin inférieur droit
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Coin inférieur gauche
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}
