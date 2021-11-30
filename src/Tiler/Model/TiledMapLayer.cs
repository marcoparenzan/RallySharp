namespace Tiler.Model
{
	using System;
	using System.Text;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	public class TiledMapLayer : IXmlSerializable
	{
		public string Name
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

		public TiledMapLayerDataEncoding Encoding
		{
			get;
			set;
		}

		public int[] Tiles
		{
			get;
			set;
		}

		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString("name", Name);
			writer.WriteAttributeString("width", Width.ToString());
			writer.WriteAttributeString("height", Height.ToString());
			writer.WriteStartElement("data");
			writer.WriteAttributeString("encoding", Encoding.ToString().ToLower());
			StringBuilder sb = new StringBuilder();
			int x = 0;
			for (int i = 0; i < Tiles.Length; i++)
			{
				sb.Append($"{Tiles[i] + 1:000},");
				if (++x == Width)
				{
					x = 0;
					sb.AppendLine();
				}
			}
			string data2 = sb.ToString().Trim();
			data2 = data2.Substring(0, data2.Length - 1);
			writer.WriteString(data2);
			writer.WriteEndElement();
		}
	}
	
}
