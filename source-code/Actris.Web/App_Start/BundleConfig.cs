using System.Diagnostics;
using System.Web;
using System.Web.Optimization;

namespace Actris.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js",
                      "~/Scripts/Component/Common.js"));

            bundles.Add(new ScriptBundle("~/bundles/crud").Include(

                      "~/Scripts/Component/AdaptiveFilter.js",
                      "~/Scripts/Component/Grid.js",
                      "~/Scripts/Component/Crud.js",
                      "~/Scripts/Component/LookupModal.js"
                      ));


            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap/bootstrap.css")
                .IncludeDirectory("~/Styles/bootstrap", "*.css", true)
                .Include("~/Styles/Color.css")
                .Include("~/Styles/Layout.css")
                .Include("~/Styles/Layout-desktop.css")
                .Include("~/Styles/Layout-mobile.css")
                .Include("~/Styles/GridList.css")
            );


        }
    }
}
