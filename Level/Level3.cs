namespace FirstDesktopApp.Level
{
    /// <summary>
    /// Level 3 - Desert themed level with aggressive enemies that chase the player.
    /// Uses deserttileset for tiles, background, and decorations.
    /// Most difficult level with fast, aggressive enemies.
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
            // Ground is at row 7 (Y=448), enemies spawn at Y=368 (448-80 for enemy height)
            // All enemies are aggressive and will chase the player
            return new List<EnemySpawn>
            {
                // Fast aggressive Wraith_03 enemies throughout the level
                new EnemySpawn { X = 400, Y = 368, PatrolLeft = 0, PatrolRight = 800, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 800, Y = 368, PatrolLeft = 400, PatrolRight = 1200, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 1200, Y = 368, PatrolLeft = 800, PatrolRight = 1600, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 1600, Y = 368, PatrolLeft = 1200, PatrolRight = 2000, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 2000, Y = 368, PatrolLeft = 1600, PatrolRight = 2400, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 2400, Y = 368, PatrolLeft = 2000, PatrolRight = 2800, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 2800, Y = 368, PatrolLeft = 2400, PatrolRight = 3200, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 3200, Y = 368, PatrolLeft = 2800, PatrolRight = 3600, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 3600, Y = 368, PatrolLeft = 3200, PatrolRight = 4000, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 4000, Y = 368, PatrolLeft = 3600, PatrolRight = 4400, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 4400, Y = 368, PatrolLeft = 4000, PatrolRight = 4800, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 4800, Y = 368, PatrolLeft = 4400, PatrolRight = 5200, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 5200, Y = 368, PatrolLeft = 4800, PatrolRight = 5600, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 5600, Y = 368, PatrolLeft = 5200, PatrolRight = 6000, EnemyType = "Wraith_03", IsAggressive = true },
                new EnemySpawn { X = 6000, Y = 368, PatrolLeft = 5600, PatrolRight = 6400, EnemyType = "Wraith_03", IsAggressive = true },
            };
        }

        private static List<ObstacleSpawn> CreateObstacleSpawns()
        {
            // Ground is at row 7 (Y=448), obstacles sit on ground at Y=384 (448-64 for obstacle height)
            // Using desert objects: Bush (1), Bush (2), Cactus (1), Cactus (2), Cactus (3), 
            // Grass (1), Grass (2), Mushroom_1, SignArrow, StoneBlock, Tree
            return new List<ObstacleSpawn>
            {
                new ObstacleSpawn { X = 200, Y = 384, SpriteType = "Cactus (1)" },
                new ObstacleSpawn { X = 500, Y = 384, SpriteType = "StoneBlock" },
                new ObstacleSpawn { X = 800, Y = 384, SpriteType = "Cactus (2)" },
                new ObstacleSpawn { X = 1100, Y = 384, SpriteType = "Tree" },
                new ObstacleSpawn { X = 1400, Y = 384, SpriteType = "Bush (1)" },
                new ObstacleSpawn { X = 1700, Y = 384, SpriteType = "Cactus (3)" },
                new ObstacleSpawn { X = 2000, Y = 384, SpriteType = "SignArrow" },
                new ObstacleSpawn { X = 2300, Y = 384, SpriteType = "Grass (1)" },
                new ObstacleSpawn { X = 2600, Y = 384, SpriteType = "Cactus (1)" },
                new ObstacleSpawn { X = 2900, Y = 384, SpriteType = "Bush (2)" },
                new ObstacleSpawn { X = 3200, Y = 384, SpriteType = "StoneBlock" },
                new ObstacleSpawn { X = 3500, Y = 384, SpriteType = "Mushroom_1" },
                new ObstacleSpawn { X = 3800, Y = 384, SpriteType = "Cactus (2)" },
                new ObstacleSpawn { X = 4100, Y = 384, SpriteType = "Tree" },
                new ObstacleSpawn { X = 4400, Y = 384, SpriteType = "Grass (2)" },
                new ObstacleSpawn { X = 4700, Y = 384, SpriteType = "Cactus (3)" },
                new ObstacleSpawn { X = 5000, Y = 384, SpriteType = "Bush (1)" },
                new ObstacleSpawn { X = 5300, Y = 384, SpriteType = "StoneBlock" },
                new ObstacleSpawn { X = 5600, Y = 384, SpriteType = "Cactus (1)" },
                new ObstacleSpawn { X = 5900, Y = 384, SpriteType = "Tree" },
            };
        }

        private static List<HealthPackSpawn> CreateHealthPackSpawns()
        {
            // More health packs needed due to difficulty - placed strategically
            return new List<HealthPackSpawn>
            {
                new HealthPackSpawn { X = 350, Y = 340, HealAmount = 30 },
                new HealthPackSpawn { X = 900, Y = 340, HealAmount = 30 },
                new HealthPackSpawn { X = 1500, Y = 340, HealAmount = 35 },
                new HealthPackSpawn { X = 2100, Y = 340, HealAmount = 30 },
                new HealthPackSpawn { X = 2700, Y = 340, HealAmount = 35 },
                new HealthPackSpawn { X = 3300, Y = 340, HealAmount = 30 },
                new HealthPackSpawn { X = 3900, Y = 340, HealAmount = 35 },
                new HealthPackSpawn { X = 4500, Y = 340, HealAmount = 30 },
                new HealthPackSpawn { X = 5100, Y = 340, HealAmount = 35 },
                new HealthPackSpawn { X = 5700, Y = 340, HealAmount = 40 },
            };
        }
    }
}
