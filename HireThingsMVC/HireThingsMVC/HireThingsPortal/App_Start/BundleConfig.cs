using System.Web;
using System.Web.Optimization;

namespace HireThingsPortal
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

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Customjs").Include(
                      "~/Scripts/jquery-1.10.2.js",
                      "~/Scripts/jquery.validate.js",
                      "~/Scripts/jquery.validate.unobtrusive.js",
                      "~/Content/assets/js/modernizr.js",
                      "~/Content/assets/js/jquery.js",
                      "~/Content/assets/js/bootstrap.js",
                      "~/Content/assets/js/nanoscroller.js",
                      "~/Content/assets/js/theme.js"                                                                
                      ));
            bundles.Add(new ScriptBundle("~/bundles/FrontendCustomjs").Include(
                   "~/Scripts/jquery-1.10.2.js",
                   "~/Scripts/jquery.validate.js",
                   "~/Scripts/jquery.validate.unobtrusive.js",
                   "~/Content/assets/js/modernizr.js",
                  
                   "~/Content/assets/js/nanoscroller.js"
                 
                   ));

            bundles.Add(new StyleBundle("~/Content/Customcss").Include(
                      "~/Content/assets/css/font-awesome.css",
                      "~/Content/assets/css/bootstrap.css",
                      "~/Content/assets/css/style.css",
                     "~/Content/assets/css/custom.css",
                     "~/Content/assets/css/icon-custom.css",
                     "~/Content/bootstrap-datetimepicker.min.css"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/probeType.js").Include(
                "~/Scripts/AppJS/probeType.js"));
            bundles.Add(new ScriptBundle("~/bundles/required.js").Include(
               "~/Scripts/AppJS/globals/hirethings.js",
               "~/Scripts/AppJS/globals/hirethings.utilities.js",
                "~/Scripts/AppJS/globals/hirethings.global.js",
                "~/Scripts/jquery.maskedinput.min.js",
                "~/Scripts/moment.min.js",
                "~/Scripts/bootstrap-datetimepicker.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/probe.js").Include(
                "~/Scripts/AppJS/probe.js"));

            bundles.Add(new ScriptBundle("~/bundles/devise.js").Include(
               "~/Scripts/AppJS/devise.js"));
        }
    }
}
