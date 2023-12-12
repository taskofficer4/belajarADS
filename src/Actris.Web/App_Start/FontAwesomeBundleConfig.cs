using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Actris.Web.App_Start.FontAwesomeBundleConfig), "RegisterBundles")]

namespace Actris.Web.App_Start
{
	public class FontAwesomeBundleConfig
	{
		public static void RegisterBundles()
		{
            // Add @Styles.Render("~/Content/fontawesome") in the <head/> of your _Layout.cshtml
            // When <compilation debug="true" />, MVC will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
            BundleTable.Bundles.Add(new StyleBundle("~/Content/fontawesome/css/bundle").Include("~/Content/fontawesome/css/all.css", new CssRewriteUrlTransform()));
		}
	}
}