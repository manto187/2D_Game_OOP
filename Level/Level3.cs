namespace FirstDesktopApp.Level
{
    /// <summary>
    /// Level 3 - Desert themed level with aggressive enemies that chase the player.
    /// HARDEST LEVEL - Many fast enemies, few obstacles, minimal health packs.
    /// </summary>
    public static class Level3
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
                TilesetName = "deserttileset"
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
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                // Row 3: High platforms
                { 0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0 },
                // Row 4: Empty
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                // Row 5: Mid platforms
                { 0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0 },
                // Row 6: Empty
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                // Row 7: Ground with more gaps (desert sand)
                { 1,1,1,0,0,0,1,1,0,0,0,1,1,1,0,0,0,1,1,0,0,0,1,1,1,0,0,0,1,1,0,0,0,1,1,1,0,0,0,1,1,0,0,0,1,1,1,0,0,0,1,1,0,0,0,1,1,1,0,0,0,1,1,0,0,0,1,1,1,0,0,0,1,1,0,0,0,1,1,1,0,0,0,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1 },
                // Row 8-10: Underground (desert rock)
                { 2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2 },
                { 2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2 },
                { 2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,2,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2 },
            };
        }

        private static List<EnemySpawn> CreateEnemySpawns()
        {
            // MANY aggressive enemies - 25 total for maximum difficulty
            return new List<EnemySpawn>
            {
                // Wave 1: Near spawn
                new EnemySpawn { X = 300, Y = 368, PatrolLeft = 0, PatrolRight = 600, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 450, Y = 368, PatrolLeft = 200, PatrolRight = 700, EnemyType = "Wraith_03", IsAggressive = true },
                
                // Wave 2
                new EnemySpawn { X = 700, Y = 368, PatrolLeft = 400, PatrolRight = 1000, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 900, Y = 368, PatrolLeft = 600, PatrolRight = 1200, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 1100, Y = 368, PatrolLeft = 800, PatrolRight = 1400, EnemyType = "Wraith_03", IsAggressive = true },
                
                // Wave 3
                new EnemySpawn { X = 1300, Y = 368, PatrolLeft = 1000, PatrolRight = 1600, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 1500, Y = 368, PatrolLeft = 1200, PatrolRight = 1800, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 1700, Y = 368, PatrolLeft = 1400, PatrolRight = 2000, EnemyType = "Wraith_03", IsAggressive = true },
                
                // Wave 4
                new EnemySpawn { X = 1900, Y = 368, PatrolLeft = 1600, PatrolRight = 2200, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 2100, Y = 368, PatrolLeft = 1800, PatrolRight = 2400, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 2300, Y = 368, PatrolLeft = 2000, PatrolRight = 2600, EnemyType = "Wraith_03", IsAggressive = true },
                
                // Wave 5
                new EnemySpawn { X = 2500, Y = 368, PatrolLeft = 2200, PatrolRight = 2800, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 2700, Y = 368, PatrolLeft = 2400, PatrolRight = 3000, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 2900, Y = 368, PatrolLeft = 2600, PatrolRight = 3200, EnemyType = "Wraith_03", IsAggressive = true },
                
                // Wave 6
                new EnemySpawn { X = 3100, Y = 368, PatrolLeft = 2800, PatrolRight = 3400, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 3300, Y = 368, PatrolLeft = 3000, PatrolRight = 3600, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 3500, Y = 368, PatrolLeft = 3200, PatrolRight = 3800, EnemyType = "Wraith_03", IsAggressive = true },
                
                // Wave 7
                new EnemySpawn { X = 3700, Y = 368, PatrolLeft = 3400, PatrolRight = 4000, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 3900, Y = 368, PatrolLeft = 3600, PatrolRight = 4200, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 4100, Y = 368, PatrolLeft = 3800, PatrolRight = 4400, EnemyType = "Wraith_03", IsAggressive = true },
                
                // Wave 8 - Final gauntlet
                new EnemySpawn { X = 4400, Y = 368, PatrolLeft = 4100, PatrolRight = 4700, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 4700, Y = 368, PatrolLeft = 4400, PatrolRight = 5000, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 5000, Y = 368, PatrolLeft = 4700, PatrolRight = 5300, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 5300, Y = 368, PatrolLeft = 5000, PatrolRight = 5600, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 5600, Y = 368, PatrolLeft = 5300, PatrolRight = 6000, EnemyType = "Wraith_03", IsAggressive = true },
            };
        }

        private static List<ObstacleSpawn> CreateObstacleSpawns()
        {
            // Minimal obstacles - only 5 for decoration, no cover for player
            return new List<ObstacleSpawn>
            {
                new ObstacleSpawn { X = 600, Y = 384, SpriteType = "Cactus (1)" },
                new ObstacleSpawn { X = 2000, Y = 384, SpriteType = "Cactus (2)" },
                new ObstacleSpawn { X = 3500, Y = 384, SpriteType = "Cactus (3)" },
                new ObstacleSpawn { X = 5000, Y = 384, SpriteType = "Tree" },
                new ObstacleSpawn { X = 6000, Y = 384, SpriteType = "SignArrow" },
            };
        }

        private static List<HealthPackSpawn> CreateHealthPackSpawns()
        {
            // Only 3 health packs for the entire level - very scarce
            return new List<HealthPackSpawn>
            {
                new HealthPackSpawn { X = 1500, Y = 340, HealAmount = 45 },
                new HealthPackSpawn { X = 3200, Y = 340, HealAmount = 45 },
                new HealthPackSpawn { X = 5200, Y = 340, HealAmount = 45 },
            };
        }
    }
}
