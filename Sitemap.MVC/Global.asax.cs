using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Autofac;

namespace Sitemap.MVC {
	public class MvcApplication : HttpApplication {
		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			var containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterType<SiteMapWriterFactory>().As<SiteMapWriterFactory>().InstancePerRequest();

			containerBuilder.Register(c => {
				var siteMapStrategies = new Dictionary<int, ISiteMapStrategy>();
				return siteMapStrategies;
			});

			RouteTable.Routes.MapRoute("SiteMapIndex", "sitemap.index.xml", new { controller = "SiteMap", action = "WriteSiteMapIndexes" });
			RouteTable.Routes.MapRoute("SiteMap", "sitemap.xml", new { controller = "SiteMap", action = "WriteSiteMaps" });
		}
	}
}