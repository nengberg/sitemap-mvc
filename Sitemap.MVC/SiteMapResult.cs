using System.Web.Mvc;

namespace Sitemap.MVC {
	public class SiteMapResult : ActionResult {
		private readonly SiteMapWriterFactory siteMapWriterFactory;
		private readonly int type;

		public SiteMapResult(SiteMapWriterFactory siteMapWriterFactory, int type) {
			this.siteMapWriterFactory = siteMapWriterFactory;
			this.type = type;
		}

		public override void ExecuteResult(ControllerContext context) {
			context.HttpContext.Response.ContentType = "text/xml";
			this.siteMapWriterFactory.WriteSiteMaps(this.type);
		}
	}
}