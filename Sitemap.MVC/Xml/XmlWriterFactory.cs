using System.Web;
using System.Xml;

namespace Sitemap.MVC.Xml {
	public class XmlWriterFactory : IXmlWriterFactory {
		private readonly HttpContextBase httpContextBase;

		public XmlWriterFactory(HttpContextBase httpContextBase) {
			this.httpContextBase = httpContextBase;
		}

		public IXmlWriter Create() {
			return new XmlWriterAdapter(XmlWriter.Create(this.httpContextBase.Response.OutputStream, new XmlWriterSettings() { Indent = true }));
		}
	}
}