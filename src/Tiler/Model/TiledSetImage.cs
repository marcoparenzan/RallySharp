namespace Tiler.Model
{
	using System.Xml.Serialization;

	public class TiledSetImage
	{
		[XmlAttribute("source")]
		public string Source
		{
			get;
			set;
		}

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
	}
}
