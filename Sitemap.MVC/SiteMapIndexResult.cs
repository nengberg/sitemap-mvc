using System.Web.Mvc;

namespace Sitemap.MVC {
	public class SiteMapIndexResult : ActionResult {
		private readonly SiteMapWriterFactory siteMapWriterFactory;

		public SiteMapIndexResult(SiteMapWriterFactory siteMapWriterFactory) {
			this.siteMapWriterFactory = siteMapWriterFactory;
		}

		public override void ExecuteResult(ControllerContext context) {
			context.HttpContext.Response.ContentType = "text/xml";
			this.siteMapWriterFactory.WriteIndex();
		}
	}
}