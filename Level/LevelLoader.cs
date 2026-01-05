using FirstDesktopApp.Entities;
using FirstDesktopApp.Rendering;

namespace FirstDesktopApp.Level
{
    /// <summary>
    /// Responsible for loading and building levels with support for multiple tilesets.
    /// </summary>
    public class LevelLoader
    {
        private readonly Dictionary<string, Dictionary<int, Image>> _tileSprites = new();
        private readonly Dictionary<string, Dictionary<string, Image>> _objectSprites = new();
        private readonly Dictionary<string, Dictionary<string, Image[]>> _wraithFrames = new();
        private readonly string _resourcePath;
        private string _currentTileset = "freetileset";

        public LevelLoader(string resourcePath)
        {
            _resourcePath = resourcePath;
            LoadTileset("freetileset");
            LoadTileset("wintertileset");
            PreloadWraithFrames();
        }

        private void LoadTileset(string tilesetName)
        {
            _tileSprites[tilesetName] = new Dictionary<int, Image>();
            _objectSprites[tilesetName] = new Dictionary<string, Image>();

            // Load tiles
            for (int i = 1; i <= 18; i++)
            {
                var path = Path.Combine(_resourcePath, tilesetName, "png", "Tiles", $"{i}.png");
                if (File.Exists(path))
                    _tileSprites[tilesetName][i] = Image.FromFile(path);
            }

            // Load objects based on tileset
            string[] objects = tilesetName == "wintertileset" 
                ? new[] { "Crate", "Crystal", "IceBox", "Igloo", "Sign_1", "Sign_2", "SnowMan", "Stone", "Tree_1", "Tree_2" }
                : new[] { "Crate", "Stone", "Bush (1)", "Bush (2)", "Tree_1", "Tree_2", "Sign_1" };

            foreach (var obj in objects)
            {
                var path = Path.Combine(_resourcePath, tilesetName, "png", "Object", $"{obj}.png");
                if (File.Exists(path))
                    _objectSprites[tilesetName][obj] = Image.FromFile(path);
            }
        }

        private void PreloadWraithFrames()
        {
            string[] wraithTypes = { "Wraith_01", "Wraith_02", "Wraith_03" };
            foreach (var type in wraithTypes)
            {
                var frames = LoadAllWraithAnimations(type);
                if (frames.Count > 0)
                    _wraithFrames[type] = frames;
            }
        }

        private Dictionary<string, Image[]> LoadAllWraithAnimations(string wraithType)
        {
            var basePath = Path.Combine(_resourcePath, wraithType, "PNG Sequences");
            var result = new Dictionary<string, Image[]>();

            if (!Directory.Exists(basePath))
                return result;

            var animations = new Dictionary<string, (string folder, string prefix)>
            {
                { "Idle", ("Idle", $"{wraithType}_Idle_") },
                { "Walking", ("Walking", $"{wraithType}_Moving Forward_") },
                { "Attacking", ("Attacking", $"{wraithType}_Attack_") },
                { "Dying", ("Dying", $"{wraithType}_Dying_") },
                { "Hurt", ("Hurt", $"{wraithType}_Hurt_") },
            };

            foreach (var anim in animations)
            {
                var frames = LoadAnimationFrames(basePath, anim.Value.folder, anim.Value.prefix);
                if (frames.Length > 0)
                    result[anim.Key] = frames;
            }

            return result;
        }

        private Image[] LoadAnimationFrames(string basePath, string folder, string prefix)
        {
            var folderPath = Path.Combine(basePath, folder);
            if (!Directory.Exists(folderPath))
                return Array.Empty<Image>();

            var frames = new List<Image>();
            for (int i = 0; i < 20; i++)
            {
                var filePath = Path.Combine(folderPath, $"{prefix}{i:D3}.png");
                if (!File.Exists(filePath))
                    break;
                frames.Add(Image.FromFile(filePath));
            }
            return frames.ToArray();
        }

        public void SetTileset(string tilesetName)
        {
            _currentTileset = tilesetName;
        }

        public List<Tile> BuildTiles(LevelData level)
        {
            var tileset = level.TilesetName ?? _currentTileset;
            var tiles = new List<Tile>();
            
            if (!_tileSprites.ContainsKey(tileset))
                tileset = "freetileset";

            for (int row = 0; row < level.Height; row++)
            {
                for (int col = 0; col < level.Width; col++)
                {
                    int tileId = level.TileMap[row, col];
                    if (tileId > 0)
                    {
                        var sprite = _tileSprites[tileset].GetValueOrDefault(tileId);
                        tiles.Add(new Tile(col * level.TileSize, row * level.TileSize, level.TileSize, TileType.Solid, sprite));
                    }
                }
            }
            return tiles;
        }

        public List<Obstacle> BuildObstacles(LevelData level)
        {
            var tileset = level.TilesetName ?? _currentTileset;
            var obstacles = new List<Obstacle>();
            
            if (!_objectSprites.ContainsKey(tileset))
                tileset = "freetileset";

            foreach (var spawn in level.ObstacleSpawns)
            {
                if (_objectSprites[tileset].TryGetValue(spawn.SpriteType, out var sprite))
                    obstacles.Add(new Obstacle(spawn.X, spawn.Y, 64, 64, sprite));
            }
            return obstacles;
        }

        public void ApplyWraithAnimations(Enemy enemy, string wraithType)
        {
            if (_wraithFrames.TryGetValue(wraithType, out var animations))
            {
                foreach (var anim in animations)
                    enemy.SetAnimationFrames(anim.Key, anim.Value);
            }
        }

        public Image? GetBackgroundImage(string? tilesetName = null)
        {
            var tileset = tilesetName ?? _currentTileset;
            var path = Path.Combine(_resourcePath, tileset, "png", "BG", "BG.png");
            return File.Exists(path) ? Image.FromFile(path) : null;
        }

        public Image? GetObjectSprite(string name, string? tilesetName = null)
        {
            var tileset = tilesetName ?? _currentTileset;
            if (_objectSprites.ContainsKey(tileset))
                return _objectSprites[tileset].GetValueOrDefault(name);
            return null;
        }
    }
}
