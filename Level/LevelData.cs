namespace FirstDesktopApp.Level
{
    /// <summary>
    /// Contains level layout data.
    /// </summary>
    public class LevelData
    {
        public int[,] TileMap { get; set; } = new int[0, 0];
        public int TileSize { get; set; } = 64;
        public PointF PlayerSpawn { get; set; }
        public List<EnemySpawn> EnemySpawns { get; set; } = new();
        public List<ObstacleSpawn> ObstacleSpawns { get; set; } = new();
        public List<HealthPackSpawn> HealthPackSpawns { get; set; } = new();
        public float FallDeathY { get; set; } = 800;
        public string TilesetName { get; set; } = "freetileset";
        public int Width => TileMap.GetLength(1);
        public int Height => TileMap.GetLength(0);
    }

    public class EnemySpawn
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float PatrolLeft { get; set; }
        public float PatrolRight { get; set; }
        public string EnemyType { get; set; } = "Wraith_01";
        public bool IsAggressive { get; set; } = false; // For Level 3 aggressive enemies
    }

    public class ObstacleSpawn
    {
        public float X { get; set; }
        public float Y { get; set; }
        public string SpriteType { get; set; } = "Crate";
    }

    public class HealthPackSpawn
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int HealAmount { get; set; } = 25;
    }
}
