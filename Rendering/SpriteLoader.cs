namespace FirstDesktopApp.Rendering
{
    /// <summary>
    /// Loads sprite animations from the Resources folder.
    /// Single Responsibility: Only handles sprite loading.
    /// </summary>
    public static class SpriteLoader
    {
        public static AnimatedSprite LoadPlayerSprites(string resourcePath)
        {
            var animated = new AnimatedSprite();
            var pngPath = Path.Combine(resourcePath, "png");

            animated.AddAnimation("Idle", LoadAnimation(pngPath, "Idle", 10, 0.1f));
            animated.AddAnimation("Run", LoadAnimation(pngPath, "Run", 8, 0.08f));
            animated.AddAnimation("Jump", LoadAnimation(pngPath, "Jump", 10, 0.1f));
            animated.AddAnimation("Shoot", LoadAnimation(pngPath, "Shoot", 3, 0.15f));
            animated.AddAnimation("Melee", LoadAnimation(pngPath, "Melee", 7, 0.08f));
            animated.AddAnimation("Dead", LoadAnimation(pngPath, "Dead", 10, 0.1f));
            animated.AddAnimation("Slide", LoadAnimation(pngPath, "Slide", 5, 0.1f));

            return animated;
        }

        private static SpriteAnimation LoadAnimation(string basePath, string prefix, int frameCount, float frameTime)
        {
            var frames = new List<Image>();
            for (int i = 1; i <= frameCount; i++)
            {
                var path = Path.Combine(basePath, $"{prefix} ({i}).png");
                if (File.Exists(path))
                    frames.Add(Image.FromFile(path));
            }
            return new SpriteAnimation(frames.ToArray(), frameTime);
        }
    }
}
