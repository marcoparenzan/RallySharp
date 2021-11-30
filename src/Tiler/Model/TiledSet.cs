namespace Tiler.Model
{
	using System.Xml.Serialization;

	[XmlRoot("tileset")]
	public class TiledSet
	{
		[XmlAttribute("name")]
		public string Name
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

		[XmlAttribute("tilecount")]
		public int TileCount
		{
			get;
			set;
		}

		[XmlAttribute("columns")]
		public int Columns
		{
			get;
			set;
		}

		[XmlElement("image")]
		public TiledSetImage Image
		{
			get;
			set;
		}
	}

}
