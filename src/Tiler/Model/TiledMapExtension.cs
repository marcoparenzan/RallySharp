using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml.Serialization;

namespace Tiler.Model
{
	public static class TiledMapExtension
	{
		public static void ExportToFolder(this TileMap tileMap, string name, string folderName)
		{
			if (!Directory.Exists(folderName))
			{
				Directory.CreateDirectory(folderName);
			}
			string tiledSetBitmapFileName = $"{name}-tileset.png";
			string tiledSetBitmapPathName = Path.Combine(folderName, tiledSetBitmapFileName);
			string tiledSetFileName = $"{name}.tsx";
			string tiledSetPathName = Path.Combine(folderName, tiledSetFileName);
			string tiledMapFileName = $"{name}.tmx";
			string tiledMapPathName = Path.Combine(folderName, tiledMapFileName);
			try
			{
				int tiledSetBitmapWidth = 256;
				int tiledSetColumns = tiledSetBitmapWidth / tileMap.TileWidth;
				int tiledSetBitmapHeight = tileMap.TileSet.Values.Count / tiledSetColumns * tileMap.TileHeight;
				if (tiledSetBitmapHeight == 0)
				{
					tiledSetBitmapHeight = tileMap.TileHeight;
				}
				Bitmap tiledSetBitmap = new Bitmap(tiledSetBitmapWidth, tiledSetBitmapHeight, PixelFormat.Format32bppArgb);
				Graphics tiledSetGraphics = Graphics.FromImage(tiledSetBitmap);
				int targetX = 0;
				int targetY = 0;
				Dictionary<Tile, int> tilesInTheSet = new Dictionary<Tile, int>();
				int tileCount = 0;
				foreach (Tile tile in tileMap.TileSet.Values)
				{
					tiledSetGraphics.DrawImage(tileMap.Bitmap, targetX, targetY, new Rectangle(tileMap.OffsetX + tile.X, tileMap.OffsetY + tile.Y, tileMap.TileWidth, tileMap.TileHeight), GraphicsUnit.Pixel);
					targetX += tileMap.TileWidth;
					if (targetX >= tiledSetBitmap.Width)
					{
						targetX = 0;
						targetY += tileMap.TileHeight;
					}
					tilesInTheSet.Add(tile, tileCount);
					tileCount++;
				}
				tiledSetBitmap.Save(tiledSetBitmapPathName);
				TiledSet tiledSet = new TiledSet
				{
					Name = name,
					TileWidth = tileMap.TileWidth,
					TileHeight = tileMap.TileHeight,
					TileCount = tileCount,
					Columns = tiledSetColumns,
					Image = new TiledSetImage
					{
						Width = tiledSetBitmapWidth,
						Height = tiledSetBitmapHeight,
						Source = tiledSetBitmapFileName
					}
				};
				XmlSerializer tiledSetSerializer = new XmlSerializer(typeof(TiledSet));
				FileStream tiledSetStream = new FileStream(tiledSetPathName, FileMode.Create);
				tiledSetSerializer.Serialize(tiledSetStream, tiledSet);
				tiledSetStream.Close();
				TiledMap tiledMap = new TiledMap
				{
					RenderOrder = TiledMapRenderOrder.RightDown,
					Width = tileMap.Width,
					Height = tileMap.Height,
					TileWidth = tileMap.TileWidth,
					TileHeight = tileMap.TileHeight,
					TileSet = new TiledSetReference
					{
						Source = tiledSetFileName
					}
				};
				TiledMapLayer tiledMapLayer = new TiledMapLayer();
				tiledMapLayer.Name = name;
				tiledMapLayer.Width = tileMap.Width;
				tiledMapLayer.Height = tileMap.Height;
				tiledMapLayer.Encoding = TiledMapLayerDataEncoding.CSV;
				tiledMapLayer.Tiles = new int[tileMap.Width * tileMap.Height];
				TiledMapLayer layer = tiledMapLayer;
				int i = 0;
				for (int y = 0; y < tileMap.Tiles.GetLength(0); y++)
				{
					for (int x = 0; x < tileMap.Tiles.GetLength(1); x++)
					{
						layer.Tiles[i++] = tilesInTheSet[tileMap.Tiles[y, x]];
					}
				}
				tiledMap.Layers = new TiledMapLayer[1]
				{
				layer
				};
				XmlSerializer tiledMapSerializer = new XmlSerializer(typeof(TiledMap));
				FileStream tiledMapStream = new FileStream(tiledMapPathName, FileMode.Create);
				tiledMapSerializer.Serialize(tiledMapStream, tiledMap);
				tiledMapStream.Close();
			}
			catch (Exception)
			{
				if (File.Exists(tiledMapPathName))
				{
					File.Delete(tiledMapPathName);
				}
				if (File.Exists(tiledSetPathName))
				{
					File.Delete(tiledSetPathName);
				}
				if (File.Exists(tiledSetBitmapPathName))
				{
					File.Delete(tiledSetBitmapPathName);
				}
			}
			finally
			{
			}
		}
	}
}
