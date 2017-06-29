using System.Web;
using System.Web.Optimization;

namespace CryptoManager
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js").Include(
                        "~/Scripts/jquery-ui-1.12.1.js").Include(
                        "~/Scripts/jquery.numeric.js").Include(
                        "~/Scripts/jquery.form.js").Include(
                        "~/Scripts/noty/jquery.noty.js").Include(
                        "~/Scripts/noty/themes/default.js").Include(
                        "~/Scripts/noty/layouts/topCenter.js").Include(
                        "~/Scripts/DataTables/jquery.dataTables.min.js").Include(
                        "~/Scripts/jquery.blockUI.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css", "~/Content/DataTables/css/jquery.dataTables.min.css",
                      "~/Content/site.css"));
        }
    }
}
