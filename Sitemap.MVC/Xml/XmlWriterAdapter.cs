using System;
using System.Xml;

namespace Sitemap.MVC.Xml {
	public class XmlWriterAdapter : IXmlWriter {
		private readonly XmlWriter xmlWriter;

		public XmlWriterAdapter(XmlWriter xmlWriter) {
			this.xmlWriter = xmlWriter;
		}

		public void WriteStartDocument() {
			this.xmlWriter.WriteStartDocument();
		}

		public void WriteStartElement(string localName) {
			WriteStartElement(null, localName, null);
		}

		public void WriteStartElement(string localName, string @namespace) {
			WriteStartElement(null, localName, @namespace);
		}

		public void WriteStartElement(string prefix, string localName, string @namespace) {
			this.xmlWriter.WriteStartElement(prefix, localName, @namespace);
		}

		public void WriteElementString(string localName, string value) {
			this.xmlWriter.WriteElementString(localName, value);
		}

		public void WriteEndElement() {
			this.xmlWriter.WriteEndElement();
		}

		public void WriteEndDocument() {
			this.xmlWriter.WriteEndDocument();
		}

		public void Dispose() {
			((IDisposable)this.xmlWriter).Dispose();
		}

		public void Flush() {
			this.xmlWriter.Flush();
		}
	}
}