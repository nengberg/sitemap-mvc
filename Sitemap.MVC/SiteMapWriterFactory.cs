using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;

using nJupiter.Web;

using Sitemap.MVC.Xml;

namespace Sitemap.MVC {
	public class SiteMapWriterFactory {
		private const string SiteMapNameSpace = "http://www.sitemaps.org/schemas/sitemap/0.9";

		private readonly Dictionary<int, ISiteMapStrategy> siteMapStrategies;
		private readonly IXmlWriterFactory xmlWriterFactory;
		private readonly HttpContextBase httpContextBase;
		private readonly IUrlHandler urlHandler;

		public SiteMapWriterFactory(Dictionary<int, ISiteMapStrategy> siteMapStrategies, IXmlWriterFactory xmlWriterFactory, HttpContextBase httpContextBase, IUrlHandler urlHandler) {
			this.siteMapStrategies = siteMapStrategies;
			this.xmlWriterFactory = xmlWriterFactory;
			this.httpContextBase = httpContextBase;
			this.urlHandler = urlHandler;
		}

		public void WriteIndex() {
			var xmlWriter = this.xmlWriterFactory.Create();
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("sitemapindex", SiteMapNameSpace);
			foreach(var siteMapStrategy in this.siteMapStrategies) {
				WriteSiteMapIndex(xmlWriter, siteMapStrategy.Key);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndDocument();
			xmlWriter.Flush();
		}

		private void WriteSiteMapIndex(IXmlWriter xmlWriter, int strategyIndex) {
			xmlWriter.WriteStartElement("sitemap");
			var url = this.httpContextBase.Request.Url;
			var queryParamForStrategy = this.urlHandler.AddQueryKeyValue("/sitemap.xml", "type", strategyIndex.ToString(CultureInfo.InvariantCulture));
			xmlWriter.WriteElementString("loc", new Uri(url, queryParamForStrategy).AbsoluteUri);
			xmlWriter.WriteEndElement();
		}

		public void WriteSiteMaps(int type) {
			var xmlWriter = this.xmlWriterFactory.Create();
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("urlset", SiteMapNameSpace);
			if(this.siteMapStrategies.ContainsKey(type)) {
				var strategy = this.siteMapStrategies[type];
				var siteMapItems = strategy.Execute();
				foreach(var siteMapItem in siteMapItems) {
					WriteSiteMap(xmlWriter, siteMapItem);
				}
			}
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndDocument();
			xmlWriter.Flush();
		}

		private static void WriteSiteMap(IXmlWriter xmlWriter, SiteMapItem siteMapItem) {
			xmlWriter.WriteStartElement("url");
			xmlWriter.WriteElementString("loc", siteMapItem.Url);
			if(siteMapItem.LastModified.HasValue) {
				var lastModified = siteMapItem.LastModified.Value.ToString("yyyy-MM-ddTHH:mm:sszzz", DateTimeFormatInfo.InvariantInfo);
				xmlWriter.WriteElementString("lastmod", lastModified);
			}
			xmlWriter.WriteEndElement();
		}
	}
}