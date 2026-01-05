namespace FirstDesktopApp.Level
{
    /// <summary>
    /// Level 2 - Winter themed level with harder enemies and winter objects.
    /// Uses wintertileset for tiles, background, and decorations.
    /// </summary>
    public static class Level2
    {
        public static LevelData Create()
        {
            return new LevelData
            {
                TileSize = 64,
                PlayerSpawn = new PointF(128, 368),
                FallDeathY = 800,
                TileMap = CreateTileMap(),
                EnemySpawns = CreateEnemySpawns(),
                ObstacleSpawns = CreateObstacleSpawns(),
                HealthPackSpawns = CreateHealthPackSpawns(),
                TilesetName = "wintertileset"
            };
        }

        private static int[,] CreateTileMap()
        {
            return new int[,]
            {
                // Row 0-1: Sky
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                // Row 2: Very high platforms
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                // Row 3: High platforms
                { 0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                // Row 4: Empty
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                // Row 5: Mid platforms
                { 0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0 },
                // Row 6: Empty
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                // Row 7: Ground with gaps (ice/snow ground)
                { 1,1,1,1,0,0,0,1,1,1,0,0,0,1,1,1,0,0,0,1,1,1,1,0,0,0,1,1,1,0,0,0,1,1,1,0,0,0,0,1,1,1,1,0,0,0,1,1,1,0,0,0,1,1,1,0,0,0,0,1,1,1,1,0,0,0,1,1,1,0,0,0,1,1,1,0,0,0,0,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                // Row 8-10: Underground (frozen ground)
                { 2,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,0,0,0,0,2,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,0,0,0,0,2,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,0,0,0,0,2,2,2,2,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2 },
                { 2,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,0,0,0,0,2,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,0,0,0,0,2,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,0,0,0,0,2,2,2,2,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2 },
                { 2,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,0,0,0,0,2,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,0,0,0,0,2,2,2,2,0,0,0,2,2,2,0,0,0,2,2,2,0,0,0,0,2,2,2,2,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2 },
            };
        }

        private static List<EnemySpawn> CreateEnemySpawns()
        {
            // Ground is at row 7 (Y=448), enemies spawn at Y=368 (448-80 for enemy height)
            // More challenging with mix of enemy types
            return new List<EnemySpawn>
            {
                new EnemySpawn { X = 300, Y = 368, PatrolLeft = 0, PatrolRight = 250, EnemyType = "Wraith_02" },
                new EnemySpawn { X = 900, Y = 368, PatrolLeft = 832, PatrolRight = 1000, EnemyType = "Wraith_01" },
                new EnemySpawn { X = 1400, Y = 368, PatrolLeft = 1216, PatrolRight = 1550, EnemyType = "Wraith_03" },
                new EnemySpawn { X = 2000, Y = 368, PatrolLeft = 1800, PatrolRight = 2200, EnemyType = "Wraith_02" },
                new EnemySpawn { X = 2700, Y = 368, PatrolLeft = 2500, PatrolRight = 2900, EnemyType = "Wraith_03" },
                new EnemySpawn { X = 3400, Y = 368, PatrolLeft = 3200, PatrolRight = 3600, EnemyType = "Wraith_02" },
                new EnemySpawn { X = 4100, Y = 368, PatrolLeft = 3900, PatrolRight = 4300, EnemyType = "Wraith_03" },
                new EnemySpawn { X = 4800, Y = 368, PatrolLeft = 4600, PatrolRight = 5000, EnemyType = "Wraith_02" },
                new EnemySpawn { X = 5500, Y = 368, PatrolLeft = 5300, PatrolRight = 5800, EnemyType = "Wraith_03" },
                new EnemySpawn { X = 6100, Y = 368, PatrolLeft = 5900, PatrolRight = 6300, EnemyType = "Wraith_03" },
            };
        }

        private static List<ObstacleSpawn> CreateObstacleSpawns()
        {
            // Ground is at row 7 (Y=448), obstacles sit on ground at Y=384 (448-64 for obstacle height)
            // Using all winter objects: Crate, Crystal, IceBox, Igloo, Sign_1, Sign_2, SnowMan, Stone, Tree_1, Tree_2
            return new List<ObstacleSpawn>
            {
                new ObstacleSpawn { X = 150, Y = 384, SpriteType = "SnowMan" },
                new ObstacleSpawn { X = 500, Y = 384, SpriteType = "Crystal" },
                new ObstacleSpawn { X = 850, Y = 384, SpriteType = "IceBox" },
                new ObstacleSpawn { X = 1200, Y = 384, SpriteType = "Tree_1" },
                new ObstacleSpawn { X = 1600, Y = 384, SpriteType = "Igloo" },
                new ObstacleSpawn { X = 2100, Y = 384, SpriteType = "Stone" },
                new ObstacleSpawn { X = 2500, Y = 384, SpriteType = "Sign_1" },
                new ObstacleSpawn { X = 2900, Y = 384, SpriteType = "Crystal" },
                new ObstacleSpawn { X = 3300, Y = 384, SpriteType = "Tree_2" },
                new ObstacleSpawn { X = 3700, Y = 384, SpriteType = "SnowMan" },
                new ObstacleSpawn { X = 4100, Y = 384, SpriteType = "IceBox" },
                new ObstacleSpawn { X = 4500, Y = 384, SpriteType = "Crate" },
                new ObstacleSpawn { X = 4900, Y = 384, SpriteType = "Sign_2" },
                new ObstacleSpawn { X = 5400, Y = 384, SpriteType = "Crystal" },
                new ObstacleSpawn { X = 5900, Y = 384, SpriteType = "Igloo" },
            };
        }

        private static List<HealthPackSpawn> CreateHealthPackSpawns()
        {
            // Health packs placed at strategic locations
            return new List<HealthPackSpawn>
            {
                new HealthPackSpawn { X = 700, Y = 340, HealAmount = 25 },
                new HealthPackSpawn { X = 1800, Y = 340, HealAmount = 25 },
                new HealthPackSpawn { X = 3000, Y = 340, HealAmount = 30 },
                new HealthPackSpawn { X = 4300, Y = 340, HealAmount = 25 },
                new HealthPackSpawn { X = 5600, Y = 340, HealAmount = 30 },
            };
        }
    }
}
