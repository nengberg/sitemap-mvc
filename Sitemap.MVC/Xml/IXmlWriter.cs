namespace Sitemap.MVC.Xml {
	public interface IXmlWriter {
		void WriteStartDocument();
		void WriteStartElement(string localName);
		void WriteStartElement(string localName, string @namespace);
		void WriteStartElement(string prefix, string localName, string @namespace);
		void WriteElementString(string localName, string value);
		void WriteEndElement();
		void WriteEndDocument();
		void Dispose();
		void Flush();
	}
}