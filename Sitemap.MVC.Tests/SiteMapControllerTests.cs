using FakeItEasy;

using FluentAssertions;

using NUnit.Framework;

namespace Sitemap.MVC.Tests {
	[TestFixture]
	public class SiteMapControllerTests {
		[Test]
		public void WriteSiteMapIndexes_WithSiteMapWriterFactory_ReturnsActionResultOfTypeSiteMapIndexResult() {
			var siteMapWriterFactory = A.Fake<SiteMapWriterFactory>();
			var siteMapController = new SiteMapController(siteMapWriterFactory);

			var actionResult = siteMapController.WriteSiteMapIndexes();

			actionResult.Should().BeOfType<SiteMapIndexResult>();
		}

		[Test]
		public void WriteSiteMaps_WithSiteMapWriterFactoryAndType_ReturnsActionResultOfTypeSiteMapResult() {
			var siteMapWriterFactory = A.Fake<SiteMapWriterFactory>();
			var siteMapController = new SiteMapController(siteMapWriterFactory);

			var actionResult = siteMapController.WriteSiteMaps(1);

			actionResult.Should().BeOfType<SiteMapResult>();
		}
	}
}