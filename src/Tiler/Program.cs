using System;
using System.Drawing;
using System.IO;
using Tiler.Model;

namespace Tiler
{
    static class Program
    {
        static void Main(string[] args)
        {
			var imageToParse = args[0];
			imageToParse = @"C:\MarcoSoft\Retrosharp\LodeSharp\Assets\4853.png";
			var name = Path.GetFileNameWithoutExtension(imageToParse);
			name = "Level1.png";
			var folderName = Path.Combine(Path.GetDirectoryName(imageToParse), name);

			Bitmap bitmap = (Bitmap)Image.FromFile(imageToParse);

			var x1 = 0;
			var y1 = 0;
			var w = bitmap.Width;
			var h = bitmap.Height;
			x1 = 8;
			y1 = 32;
			w = 192;
			h = 120;

			var tw = 8;
			var th = 8;
			//tx = 8;
			//ty = 8;
	
			Bitmap bitmap1 = (Bitmap)new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Graphics gb1 = Graphics.FromImage(bitmap1);
			gb1.DrawImage(bitmap, 0, 0, new Rectangle(x1, y1, bitmap1.Width, bitmap1.Height), GraphicsUnit.Pixel);

			TileMap tileMap = TileMap.Parse(bitmap1, tw, th);
			tileMap.ExportToFolder(name, folderName);
		}
    }
}
