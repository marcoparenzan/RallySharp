using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Tiler.Model
{
	[XmlRoot("map")]
	public class TiledMap
	{
		[XmlAttribute("version")]
		public string Version
		{
			get;
			set;
		} = "1.0";


		[XmlAttribute("tiledversion")]
		public string TiledVersion
		{
			get;
			set;
		} = "1.0.1";


		[XmlAttribute("orientation")]
		public TiledMapOrientation Orientation
		{
			get;
			set;
		} = TiledMapOrientation.Orthogonal;


		[XmlAttribute("renderorder")]
		public TiledMapRenderOrder RenderOrder
		{
			get;
			set;
		} = TiledMapRenderOrder.RightDown;


		[XmlAttribute("width")]
		public int Width
		{
			get;
			set;
		}

		[XmlAttribute("height")]
		public int Height
		{
			get;
			set;
		}

		[XmlAttribute("tilewidth")]
		public int TileWidth
		{
			get;
			set;
		}

		[XmlAttribute("tileheight")]
		public int TileHeight
		{
			get;
			set;
		}

		[XmlAttribute("nextobjectid")]
		public int NextObjectId
		{
			get;
			set;
		} = 1;


		[XmlElement("tileset")]
		public TiledSetReference TileSet
		{
			get;
			set;
		}

		[XmlElement("layer")]
		public TiledMapLayer[] Layers
		{
			get;
			set;
		}
	}
}
