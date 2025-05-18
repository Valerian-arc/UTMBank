using System.Web.Optimization;

namespace Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap-grid.css",
                "~/Content/bootstrap-grid.min.css",
                "~/Content/bootstrap-grid.rtl.css",
                "~/Content/bootstrap-grid.rtl.min.css",
                "~/Content/bootstrap-reboot.css",
                "~/Content/bootstrap-reboot.min.css",
                "~/Content/bootstrap-reboot.rtl.css",
                "~/Content/bootstrap-reboot.rtl.min.css",
                "~/Content/bootstrap-utilities.css",
                "~/Content/bootstrap-utilities.min.css",
                "~/Content/bootstrap-utilities.rtl.css",
                "~/Content/bootstrap-utilities.rtl.min.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap.rtl.css",
                "~/Content/bootstrap.rtl.min.css",
                "~/Content/bootstrap.rtl.min.css.map",
                "~/Content/font-awesome.css",
                "~/Content/font-awesome.min.css",
                "~/Content/sb-admin-2.css",
                "~/Content/sb-admin-2.min.css",
                "~/Content/site.css"
            ));

            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                "~/fonts/all.css",
                "~/fonts/all.min.css",
                "~/fonts/brands.css",
                "~/fonts/brands.min.css",
                "~/fonts/fontawesome.css",
                "~/fonts/fontawesome.min.css",
                "~/fonts/regular.css",
                "~/fonts/regular.min.css",
                "~/fonts/solid.css",
                "~/fonts/solid.min.css",
                "~/fonts/svg-with-js.css",
                "~/fonts/svg-with-js.min.css",
                "~/fonts/v4-shims.css",
                "~/fonts/v4-shims.min.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/allscripts").Include(
                "~/Content/Scripts/bootstrap.bundle.js",
                "~/Content/Scripts/bootstrap.bundle.min.js",
                "~/Content/Scripts/bootstrap.esm.js",
                "~/Content/Scripts/bootstrap.esm.min.js",
                "~/Content/Scripts/bootstrap.js",
                "~/Content/Scripts/bootstrap.min.js",
                "~/Content/Scripts/jquery-3.7.1.intellisense.js",
                "~/Content/Scripts/jquery-3.7.1.js",
                "~/Content/Scripts/jquery-3.7.1.min.js",
                "~/Content/Scripts/jquery-3.7.1.slim.js",
                "~/Content/Scripts/jquery-3.7.1.slim.min.js",
                "~/Content/Scripts/jquery-ui-1.14.1.js",
                "~/Content/Scripts/jquery-ui-1.14.1.min.js",
                "~/Content/Scripts/jquery.easing.compatibility.js",
                "~/Content/Scripts/jquery.easing.js",
                "~/Content/Scripts/jquery.easing.min.js",
                "~/Content/Scripts/jquery.js",
                "~/Content/Scripts/jquery.slim.js",
                "~/Content/Scripts/jquery.slim.min.js",
                "~/Content/Scripts/jquery.validate-vsdoc.js",
                "~/Content/Scripts/jquery.validate.js",
                "~/Content/Scripts/jquery.validate.min.js",
                "~/Content/Scripts/sb-admin-2.js",
                "~/Content/Scripts/sb-admin-2.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/site.css"));

            BundleTable.EnableOptimizations = true;
         
        }
    }
}