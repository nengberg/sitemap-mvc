using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;

using FakeItEasy;

using nJupiter.Web;

using NUnit.Framework;

using Sitemap.MVC.Xml;

namespace Sitemap.MVC.Tests {
	[TestFixture]
	public class SiteMapWriterFactoryTests {
		[Test]
		public void WriteIndex_NoSiteMapStrategiesRegistered_ACallToXmlWritersWriteStartDocumentShouldBeMade() {
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteIndex();

			A.CallTo(() => xmlWriter.WriteStartDocument()).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_NoSiteMapStrategiesRegistered_ACallToXmlWritersWriteEndDocumentShouldBeMade() {
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteIndex();

			A.CallTo(() => xmlWriter.WriteEndDocument()).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_NoSiteMapStrategiesRegistered_ACallToXmlWritersWriteStartElementWithSitemapindexShouldBeMade() {
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteIndex();

			A.CallTo(() => xmlWriter.WriteStartElement("sitemapindex", A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_NoSiteMapStrategiesRegistered_ACallToXmlWritersWriteEndElementShouldBeMade() {
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteIndex();

			A.CallTo(() => xmlWriter.WriteEndElement()).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_NoSiteMapStrategiesRegistered_ACallToXmlWritersWriteStartElementForSitemapShouldNotBeMade() {
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteIndex();

			A.CallTo(() => xmlWriter.WriteStartElement("sitemap")).MustNotHaveHappened();
		}

		[Test]
		public void WriteIndex_OneSiteMapStrategyRegistered_ACallToXmlWritersWriteStartElementForSitemapShouldBeMade() {
			var siteMapStrategy = A.Fake<ISiteMapStrategy>();
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			siteMapStrategies.Add(0, siteMapStrategy);
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var httpContextBase = A.Fake<HttpContextBase>();
			var httpRequestBase = A.Fake<HttpRequestBase>();
			A.CallTo(() => httpRequestBase.Url).Returns(new Uri("http://abc.se"));
			A.CallTo(() => httpContextBase.Request).Returns(httpRequestBase);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory, httpContextBase);

			siteMapWriterFactory.WriteIndex();

			A.CallTo(() => xmlWriter.WriteStartElement("sitemap")).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_OneSiteMapStrategyRegistered_ACallToXmlWritersWriteElementStringForLocShouldBeMade() {
			var siteMapStrategy = A.Fake<ISiteMapStrategy>();
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			siteMapStrategies.Add(0, siteMapStrategy);
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var httpContextBase = A.Fake<HttpContextBase>();
			var httpRequestBase = A.Fake<HttpRequestBase>();
			A.CallTo(() => httpRequestBase.Url).Returns(new Uri("http://abc.se"));
			A.CallTo(() => httpContextBase.Request).Returns(httpRequestBase);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory, httpContextBase);

			siteMapWriterFactory.WriteIndex();

			A.CallTo(() => xmlWriter.WriteElementString("loc", A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_OneSiteMapStrategyRegistered_XmlWriterWritesCorrectValueForElementStringLoc() {
			var siteMapStrategy = A.Fake<ISiteMapStrategy>();
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			siteMapStrategies.Add(0, siteMapStrategy);
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var httpContextBase = A.Fake<HttpContextBase>();
			var httpRequestBase = A.Fake<HttpRequestBase>();
			A.CallTo(() => httpRequestBase.Url).Returns(new Uri("http://abc.se"));
			A.CallTo(() => httpContextBase.Request).Returns(httpRequestBase);
			var urlHandler = A.Fake<IUrlHandler>();
			A.CallTo(() => urlHandler.AddQueryKeyValue("/sitemap.xml", "type", "0")).Returns("/sitemap.xml?type=0");
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory, httpContextBase, urlHandler);

			siteMapWriterFactory.WriteIndex();

			A.CallTo(() => xmlWriter.WriteElementString("loc", "http://abc.se/sitemap.xml?type=0")).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_InputtedTypeIsNotPresentInDictionaryOfStrategies_ACallToXmlWritersWriteStartElementForUrlShouldNotBeMade() {
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteSiteMaps(5);

			A.CallTo(() => xmlWriter.WriteStartElement("url")).MustNotHaveHappened();
		}

		[Test]
		public void WriteIndex_InputtedTypeIsNotPresentInDictionaryOfStrategies_ACallToXmlWritersWriteStartDocumentForUrlShouldBeMade() {
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteSiteMaps(5);

			A.CallTo(() => xmlWriter.WriteStartDocument()).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_InputtedTypeIsNotPresentInDictionaryOfStrategies_ACallToXmlWritersWriteStartElementForUrlsetShouldBeMade() {
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteSiteMaps(5);

			A.CallTo(() => xmlWriter.WriteStartElement("urlset", A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_InputtedTypeIsNotPresentInDictionaryOfStrategies_ACallToXmlWritersWriteEndElementShouldBeMade() {
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteSiteMaps(5);

			A.CallTo(() => xmlWriter.WriteEndElement()).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_InputtedTypeIsNotPresentInDictionaryOfStrategies_ACallToXmlWritersWriteEndDocumentShouldBeMade() {
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteSiteMaps(5);

			A.CallTo(() => xmlWriter.WriteEndDocument()).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_InputtedTypeIsPresentInDictionaryOfStrategies_ACallToStrategysExecuteMethodShouldBeMade() {
			var siteMapStrategy = A.Fake<ISiteMapStrategy>();
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			siteMapStrategies.Add(0, siteMapStrategy);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, A.Fake<IXmlWriterFactory>());

			siteMapWriterFactory.WriteSiteMaps(0);

			A.CallTo(() => siteMapStrategy.Execute()).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_StrategyPresentInDictionaryExecuteMethodReturnsReturnsOneSiteMapItem_ACallToXmlWritersWriteStartElementForUrlShouldBeMade() {
			var siteMapStrategy = A.Fake<ISiteMapStrategy>();
			var siteMapItems = new List<SiteMapItem>();
			siteMapItems.Add(new SiteMapItem());
			A.CallTo(() => siteMapStrategy.Execute()).Returns(siteMapItems);
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			siteMapStrategies.Add(0, siteMapStrategy);
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteSiteMaps(0);

			A.CallTo(() => xmlWriter.WriteStartElement("url")).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_StrategyPresentInDictionaryExecuteMethodReturnsReturnsOneSiteMapItem_ACallToXmlWritersWriteElementStringForLocShouldBeMade() {
			var siteMapStrategy = A.Fake<ISiteMapStrategy>();
			var siteMapItems = new List<SiteMapItem>();
			siteMapItems.Add(new SiteMapItem());
			A.CallTo(() => siteMapStrategy.Execute()).Returns(siteMapItems);
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			siteMapStrategies.Add(0, siteMapStrategy);
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteSiteMaps(0);

			A.CallTo(() => xmlWriter.WriteElementString("loc", A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_StrategyPresentInDictionaryExecuteMethodReturnsReturnsOneSiteMapItem_XmlWriterWritesCorrectValueForElementStringLoc() {
			var siteMapStrategy = A.Fake<ISiteMapStrategy>();
			var siteMapItems = new List<SiteMapItem>();
			siteMapItems.Add(new SiteMapItem() { Url = "http://ab.ef" });
			A.CallTo(() => siteMapStrategy.Execute()).Returns(siteMapItems);
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			siteMapStrategies.Add(0, siteMapStrategy);
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteSiteMaps(0);

			A.CallTo(() => xmlWriter.WriteElementString("loc", "http://ab.ef")).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_StrategyPresentInDictionaryExecuteMethodReturnsReturnsOneSiteMapItemWithoutLastModified_ACallToXmlWritersWriteElementStringForLastmodShouldNotBeMade() {
			var siteMapStrategy = A.Fake<ISiteMapStrategy>();
			var siteMapItems = new List<SiteMapItem>();
			siteMapItems.Add(new SiteMapItem());
			A.CallTo(() => siteMapStrategy.Execute()).Returns(siteMapItems);
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			siteMapStrategies.Add(0, siteMapStrategy);
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteSiteMaps(0);

			A.CallTo(() => xmlWriter.WriteElementString("lastmod", A<string>.Ignored)).MustNotHaveHappened();
		}

		[Test]
		public void WriteIndex_StrategyPresentInDictionaryExecuteMethodReturnsReturnsOneSiteMapItemWithLastModified_ACallToXmlWritersWriteElementStringForLastmodShouldBeMade() {
			var siteMapStrategy = A.Fake<ISiteMapStrategy>();
			var siteMapItems = new List<SiteMapItem>();
			siteMapItems.Add(new SiteMapItem() { LastModified = DateTime.Now });
			A.CallTo(() => siteMapStrategy.Execute()).Returns(siteMapItems);
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			siteMapStrategies.Add(0, siteMapStrategy);
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteSiteMaps(0);

			A.CallTo(() => xmlWriter.WriteElementString("lastmod", A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Test]
		public void WriteIndex_StrategyPresentInDictionaryExecuteMethodReturnsReturnsOneSiteMapItemWithLastModified_XmlWriterWritesCorrectValueForElementStringLastmod() {
			var siteMapStrategy = A.Fake<ISiteMapStrategy>();
			var siteMapItems = new List<SiteMapItem>();
			var lastModified = new DateTime(1111, 1, 1);
			siteMapItems.Add(new SiteMapItem() { LastModified = lastModified });
			A.CallTo(() => siteMapStrategy.Execute()).Returns(siteMapItems);
			var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
			siteMapStrategies.Add(0, siteMapStrategy);
			var xmlWriter = A.Fake<IXmlWriter>();
			var xmlWriterFactory = MakeXmlFactoryThatReturnsXmlWriter(xmlWriter);
			var siteMapWriterFactory = MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory);

			siteMapWriterFactory.WriteSiteMaps(0);

			A.CallTo(() => xmlWriter.WriteElementString("lastmod", lastModified.ToString("yyyy-MM-ddTHH:mm:sszzz", DateTimeFormatInfo.InvariantInfo)))
				.MustHaveHappened(Repeated.Exactly.Once);
		}

		private static SiteMapWriterFactory MakeSiteMapWriterFactory(Dictionary<int, ISiteMapStrategy> siteMapStrategies, IXmlWriterFactory xmlWriterFactory) {
			return MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory, A.Fake<HttpContextBase>());
		}

		private static SiteMapWriterFactory MakeSiteMapWriterFactory(Dictionary<int, ISiteMapStrategy> siteMapStrategies, IXmlWriterFactory xmlWriterFactory, HttpContextBase httpContextBase) {
			return MakeSiteMapWriterFactory(siteMapStrategies, xmlWriterFactory, httpContextBase, A.Fake<IUrlHandler>());
		}

		private static SiteMapWriterFactory MakeSiteMapWriterFactory(Dictionary<int, ISiteMapStrategy> siteMapStrategies, IXmlWriterFactory xmlWriterFactory, HttpContextBase httpContextBase,
			IUrlHandler urlHandler) {
			return new SiteMapWriterFactory(siteMapStrategies, xmlWriterFactory, httpContextBase, urlHandler);
		}

		private static IXmlWriterFactory MakeXmlFactoryThatReturnsXmlWriter(IXmlWriter xmlWriter) {
			var xmlWriterFactory = A.Fake<IXmlWriterFactory>();
			A.CallTo(() => xmlWriterFactory.Create()).Returns(xmlWriter);
			return xmlWriterFactory;
		}
	}
}