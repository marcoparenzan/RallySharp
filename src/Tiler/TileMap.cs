using System.Collections.Generic;
using System.Drawing;

namespace Tiler
{
	public class TileMap
	{
		public Bitmap Bitmap
		{
			get;
			private set;
		}

		public Tile[,] Tiles
		{
			get;
			set;
		}

		public Dictionary<string, Tile> TileSet
		{
			get;
			set;
		}

		public int Width
		{
			get;
			set;
		}

		public int Height
		{
			get;
			set;
		}

		public int TileWidth
		{
			get;
			set;
		}

		public int TileHeight
		{
			get;
			set;
		}

		public int OffsetX
		{
			get;
			set;
		}

		public int OffsetY
		{
			get;
			set;
		}

		public static TileMap Parse(Bitmap bitmap, int tileWidth, int tileHeight, Dictionary<string, Tile> tileSet = null)
		{
			if (tileSet == null)
			{
				tileSet = new Dictionary<string, Tile>();
			}
			TileMap tileMap = new TileMap
			{
				Bitmap = bitmap,
				TileHeight = tileHeight,
				TileWidth = tileWidth,
				Width = bitmap.Width / tileWidth,
				Height = bitmap.Height / tileHeight,
				OffsetX = 0,
				OffsetY = 0,
				TileSet = tileSet
			};
			tileMap.Tiles = new Tile[tileMap.Height, tileMap.Width];
			for (int y = 0; y < tileMap.Height; y++)
			{
				int y2 = y * tileHeight;
				for (int x = 0; x < tileMap.Width; x++)
				{
					int x2 = x * tileWidth;
					Tile tile2 = new Tile();
					tile2.X = x2;
					tile2.Y = y2;
					tile2.TileMap = tileMap;
					tile2.Bytes = new byte[4 * tileWidth * tileHeight];
					Tile tile = tile2;
					int i = 0;
					for (int yy = 0; yy < tileHeight; yy++)
					{
						for (int xx = 0; xx < tileWidth; xx++)
						{
							int y3 = y2 + yy;
							int x3 = x2 + xx;
							Color pixel = bitmap.GetPixel(tileMap.OffsetX + x3, tileMap.OffsetY + y3);
							tile.Bytes[i++] = pixel.R;
							tile.Bytes[i++] = pixel.B;
							tile.Bytes[i++] = pixel.G;
							tile.Bytes[i++] = pixel.A;
						}
					}
					tile.CalculateKey();
					if (tileSet.ContainsKey(tile.Key))
					{
						Tile existingTile = tileSet[tile.Key];
						tileMap.Tiles[y, x] = existingTile;
					}
					else
					{
						tileMap.Tiles[y, x] = tile;
						tileSet.Add(tile.Key, tile);
					}
				}
			}
			return tileMap;
		}
	}
}