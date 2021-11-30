namespace Tiler.Model
{
	using System.Xml.Serialization;

	public class TiledSetReference
	{
		[XmlAttribute("firstgid")]
		public int FirstGid
		{
			get;
			set;
		} = 1;


		[XmlAttribute("source")]
		public string Source
		{
			get;
			set;
		}
	}
}
