using System.Web.Mvc;

namespace Sitemap.MVC {
	public class SiteMapController : Controller {
		private readonly SiteMapWriterFactory siteMapWriterFactory;

		public SiteMapController(SiteMapWriterFactory siteMapWriterFactory) {
			this.siteMapWriterFactory = siteMapWriterFactory;
		}

		public ActionResult WriteSiteMapIndexes() {
			return new SiteMapIndexResult(this.siteMapWriterFactory);
		}

		public ActionResult WriteSiteMaps(int type) {
			return new SiteMapResult(this.siteMapWriterFactory, type);
		}
	}
}