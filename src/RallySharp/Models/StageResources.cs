using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;

namespace RallySharp.Models
{
    public class StageResources
    {
        public string Title { get; private set; }
        public int[] DefaultLayer { get; private set; }
        public Vec Size { get; private set; }
        public Vec TileSize { get; private set; }
        public Bitmap Set { get; private set; }
        public int SetTilesPerRow { get; private set; }
        public Rectangle[] TileRectCache { get; private set; }
        public Bitmap SpriteSheet { get; private set; }
        public Dictionary<string, Rectangle> Frames { get; } = new Dictionary<string, Rectangle>();

        public static StageResources Get(int stageNumber = 1)
        {
            var stage = new StageResources();

            stage.Title = $"RallySharp";

            var mapStream = typeof(StageResources).Assembly.GetManifestResourceStream(typeof(StageResources), $"Level{stageNumber}.json");
            var mapBytes = new byte[mapStream.Length];
            mapStream.Read(mapBytes, 0, mapBytes.Length);
            var map = JsonSerializer.Deserialize<TileMap>(mapBytes);
            stage.DefaultLayer = map.Layers[0].Data;
            stage.Size = (map.Layers[0].Width, map.Layers[0].Height);

            var setStream = typeof(StageResources).Assembly.GetManifestResourceStream(typeof(StageResources), $"Level1.png-tileset.png");
            stage.Set = (Bitmap)Image.FromStream(setStream);
            stage.SetTilesPerRow = 8;
            stage.TileSize = (map.TileWidth, map.TileHeight);

            var tileRectCache = new List<Rectangle>();
            for (var ty = 0; ty < (stage.Set.Height / stage.TileSize.y); ty++)
            {
                for (var tx = 0; tx < (stage.Set.Width / stage.TileSize.x); tx++)
                {
                    tileRectCache.Add(new Rectangle((int)(tx * stage.TileSize.x), (int)(ty * stage.TileSize.y), (int)stage.TileSize.x, (int)stage.TileSize.y));
                }
            }
            stage.TileRectCache = tileRectCache.ToArray();

            var spriteSheetStream = typeof(StageResources).Assembly.GetManifestResourceStream(typeof(StageResources), $"car-spritesheet.png");
            stage.SpriteSheet = (Bitmap)Image.FromStream(spriteSheetStream);
            for(var i = 0; i<12; i++) stage.Frames[$"maincar-blue-{i}"] = new Rectangle(i*24, 0, 24, 24);

            return stage;
        }

    }
}
