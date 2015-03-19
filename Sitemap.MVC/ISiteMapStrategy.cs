using System.Collections.Generic;

namespace Sitemap.MVC {
	public interface ISiteMapStrategy {
		IEnumerable<SiteMapItem> Execute();
	}
}