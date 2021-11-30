using System;
using System.Collections.Generic;
using System.Text;

namespace Tiler
{
	// Tiler.Tile
	using System;
	using Tiler;

	public class Tile
	{
		public TileMap TileMap
		{
			get;
			set;
		}

		public int X
		{
			get;
			set;
		}

		public int Y
		{
			get;
			set;
		}

		public byte[] Bytes
		{
			get;
			set;
		}

		public string Key
		{
			get;
			set;
		}

		internal void CalculateKey()
		{
			Key = Convert.ToBase64String(Bytes);
		}
	}
}
