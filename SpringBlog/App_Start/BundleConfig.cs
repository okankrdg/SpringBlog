using System.Web;
using System.Web.Optimization;

namespace SpringBlog
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            bundles.Add(new ScriptBundle("~/bundles/jquery", "https://code.jquery.com/jquery-3.4.1.min.js").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.

            bundles.Add(new ScriptBundle("~/bundles/bootstrap", "https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.bundle.min.js").Include(
                      "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss", "https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css").Include(
                      "~/Content/bootstrap.css")); 

            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/Site.css"));

            //normalde yazmaya gerek yok, test aşamasında kullanılabilir.
            #if DEBUG
               BundleTable.EnableOptimizations = false;
            #else
              BundleTable.EnableOptimizations = true;
            #endif
        }
    }
}
