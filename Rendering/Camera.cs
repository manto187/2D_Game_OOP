using System.Drawing;

namespace FirstDesktopApp.Rendering
{
    /// <summary>
    /// Camera system that follows a target and handles world-to-screen transformations.
    /// Single Responsibility: Only handles viewport and coordinate transformation.
    /// </summary>
    public class Camera
    {
        public PointF Position { get; set; }
        public SizeF ViewportSize { get; set; }
        public RectangleF WorldBounds { get; set; }

        public Camera(SizeF viewportSize)
        {
            ViewportSize = viewportSize;
            Position = PointF.Empty;
        }

        public void Follow(PointF targetPosition, SizeF targetSize)
        {
            float targetCenterX = targetPosition.X + targetSize.Width / 2;
            float targetCenterY = targetPosition.Y + targetSize.Height / 2;

            float newX = targetCenterX - ViewportSize.Width / 2;
            float newY = targetCenterY - ViewportSize.Height / 2;

            // Clamp to world bounds
            newX = Math.Max(WorldBounds.Left, Math.Min(newX, WorldBounds.Right - ViewportSize.Width));
            newY = Math.Max(WorldBounds.Top, Math.Min(newY, WorldBounds.Bottom - ViewportSize.Height));

            Position = new PointF(newX, newY);
        }

        public PointF WorldToScreen(PointF worldPosition)
        {
            return new PointF(worldPosition.X - Position.X, worldPosition.Y - Position.Y);
        }

        public RectangleF GetViewBounds()
        {
            return new RectangleF(Position, ViewportSize);
        }
    }
}
