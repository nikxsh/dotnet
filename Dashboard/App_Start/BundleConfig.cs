using System.Web;
using System.Web.Optimization;

namespace Dashboard
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                      "~/Scripts/dashboard.js",
                      "~/Scripts/metisMenu.js"));

            bundles.Add(new ScriptBundle("~/bundles/dashBoard").Include(
                      "~/Scripts/raphael.js",
                      "~/Scripts/morris.js",
                      "~/Scripts/morris-data.js"));

            bundles.Add(new ScriptBundle("~/bundles/tables").Include(
                      "~/Scripts/jquery.dataTables.js",
                      "~/Scripts/dataTables.bootstrap.js",
                      "~/Scripts/dataTables.responsive.js"));

            bundles.Add(new ScriptBundle("~/bundles/flot").Include(
                      "~/Scripts/excanvas.js",
                      "~/Scripts/jquery.flot.js",
                      "~/Scripts/jquery.flot.pie.js",
                      "~/Scripts/jquery.flot.resize.js",
                      "~/Scripts/jquery.flot.time.js",
                      "~/Scripts/jquery.flot.tooltip.js",
                      "~/Scripts/flot-data.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/site.css",
                      "~/Content/css/metisMenu.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/css/morris.css"));

            bundles.Add(new StyleBundle("~/Content/tables").Include(
                      "~/Content/css/dataTables.bootstrap.css",
                      "~/Content/css/dataTables.responsive.css"));
        }
    }
}
