using System.Web;
using System.Web.Optimization;

namespace SwachhBharatAbhiyan.CMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif


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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/BootStrapCSS").Include(
                       "~/Content/bootstrap-3.3.6-dist/css/bootstrap.css",
                       "~/Content/bootstrap-3.3.6-dist/css/font-awesome.min.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/BootStrapJS").Include(
                        "~/Content/bootstrap-3.3.6-dist/js/bootstrap.js"
                ));

            bundles.Add(new StyleBundle("~/Content/SplashCSS").Include(
                       "~/Content/Splash/animate.css",
                       "~/Content/Splash/font-awesome.min.css",
                       "~/Content/Splash/main.css",
                       "~/Content/Splash/owl.carousel.css",
                       "~/Content/Splash/owl.theme.css",
                       "~/Content/Splash/responsive.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/SpalshJS").Include(
                        "~/Scripts/Splash/main.js",
                        "~/Scripts/Splash/owl.carousel.min.js",
                        "~/Scripts/Splash/plugins.js",
                        "~/Scripts/Splash/wow.min.js"
                ));


            bundles.Add(new StyleBundle("~/Content/LoginCSS").Include(
                  "~/Content/Account/Login.css"
           ));

            bundles.Add(new ScriptBundle("~/Content/LoginJS").Include(
                        "~/Scripts/Account/TweenLite.min.js"
                ));

            bundles.Add(new StyleBundle("~/jqGrid/css").Include(
                     "~/Content/jqGrid/css/ui.jqgrid.css",
                     "~/Content/jqGrid/css/jquery-ui.css"
                     ));

            bundles.Add(new ScriptBundle("~/jqGrid/scripts").Include(
                    "~/Content/jqGrid/js/i18n/grid.locale-en.js",
                    "~/Content/jqGrid/js/jquery.jqGrid.min.js",
                    "~/Content/jqGrid/js/jquery.jqGrid.src.js"
                    ));

            bundles.Add(new ScriptBundle("~/colorbox/scripts").Include(
               "~/Content/colourbox/jquery.colorbox.js"
               ));

            bundles.Add(new StyleBundle("~/colorbox/css").Include(
                   "~/Content/colourbox/colorbox.css"
                ));

            bundles.Add(new StyleBundle("~/Content/ValidationCss").Include(
                   "~/Content/JqueryValidationEngine/validationEngine.jquery.css"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/ValidationScripts").Include(
                "~/Scripts/jquery.validationEngine.js",
                "~/Scripts/jquery.validationEngine-en.js",
                "~/Scripts/jquery.CustomevalidationEngine-en.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/AlertifyScripts").Include(
                    "~/Content/css/appy-platter/alertify/lib/alertify.min.js",
                    "~/Content/css/alertify/js/alertify.js",
                    "~/Content/css/prityy_photo/js/lightbox-plus-jquery.js"
                ));
            bundles.Add(new StyleBundle("~/Content/AlertifyCss").Include(
                    "~/Content/css/alertify/alertify.core.css",
                    "~/Content/css/alertify/alertify.default.css",
                    "~/Content/css/prityy_photo/css/lightbox.css"
                ));

           // theme assets
            bundles.Add(new ScriptBundle("~/Scripts/ThemeScripts").Include(
               "~/Content/theme-assets/vendor/jquery/jquery.min.js",
                "~/Content/theme-assets/vendor/popper.js/umd/popper.min.js",
                 "~/Content/theme-assets/vendor/bootstrap/js/bootstrap.min.js",
                 "~/Content/theme-assets/vendor/jquery.cookie/jquery.cookie.js",
               "~/Content/theme-assets/vendor/chart.js/Chart.min.js",
                  "~/Content/theme-assets/vendor/jquery-validation/jquery.validate.min.js",
               "~/Content/theme-assets/js/front.js",
                "~/Content/theme-assets/js/MyCustom.js"

               ));

         


            bundles.Add(new StyleBundle("~/Content/ThemeCss").Include(
                    "~/Content/theme-assets/vendor/bootstrap/css/bootstrap.min.css",
                    "~/Content/theme-assets/vendor/font-awesome/css/font-awesome.min.css",
                    "~/Content/theme-assets/css/fontastic.css",
                    "~/Content/theme-assets/css/style.default.css",
                    "~/Content/theme-assets/css/custom.css",
                    "~/Content/theme-assets/css/MyCustom.css",
                     "~/Content/bootstrap-3.3.6-dist/css/font-awesome.min.css"
                ));


            //Liquid Theme Assets

            bundles.Add(new StyleBundle("~/Liquid/Content/ThemeCss").Include(
                 "~/Areas/Liquid/Content/theme-assets/vendor/bootstrap/css/bootstrap.min.css",
                 "~/Areas/Liquid/Content/theme-assets/vendor/font-awesome/css/font-awesome.min.css",
                 "~/Areas/Liquid/Content/theme-assets/css/fontastic.css",
                 "~/Areas/Liquid/Content/theme-assets/css/style.default.css",
                 "~/Areas/Liquid/Content/theme-assets/css/custom.css",
                 "~/Areas/Liquid/Content/theme-assets/css/MyCustom.css"
             ));

            //Street Theme Assets

            bundles.Add(new StyleBundle("~/Street/Content/ThemeCss").Include(
                 "~/Areas/Street/Content/theme-assets/vendor/bootstrap/css/bootstrap.min.css",
                 "~/Areas/Street/Content/theme-assets/vendor/font-awesome/css/font-awesome.min.css",
                 "~/Areas/Street/Content/theme-assets/css/fontastic.css",
                 "~/Areas/Street/Content/theme-assets/css/style.default.css",
                 "~/Areas/Street/Content/theme-assets/css/custom.css",
                 "~/Areas/Street/Content/theme-assets/css/MyCustom.css"
             ));

            //Index Datatable assets
            bundles.Add(new ScriptBundle("~/Scripts/IndexScript").Include(
                   "~/Content/Datatable-assets/js/jquery.min.js",
                   "~/Content/Datatable-assets/js/jquery-3.3.1.js",
                   "~/Content/Datatable-assets/js/bootstrap.min.js",
                   "~/Content/Datatable-assets/js/jquery.dataTables.min.js",
                   "~/Content/Datatable-assets/js/dataTables.bootstrap4.min.js",
                   "~/Content/Datatable-assets/js/dataTables.responsive.min.js",
                   "~/Content/Datatable-assets/js/responsive.bootstrap4.min.js",
                    "~/Content/theme-assets/js/MyCustom.js"
               ));
            bundles.Add(new StyleBundle("~/Content/IndexCSS").Include(
                 "~/Content/Datatable-assets/css/bootstrap.css",
                    "~/Content/Datatable-assets/css/dataTables.bootstrap4.min.css",
                    "~/Content/Datatable-assets/css/responsive.bootstrap4.min.css",
                    "~/Content/css/datepicker/css/datepicker.css",
                    "~/Content/css/index_style.css",
                    "~/Content/theme-assets/css/MyCustom.css"
                ));
            
            //Add/Edit page assets
            bundles.Add(new ScriptBundle("~/Scripts/AddEditScript").Include(
                        "~/Scripts/jquery-1.10.2.min.js",
                        "~/Content/ImageSizeValidation/ImageSizeValidation.js"
                         ));
            bundles.Add(new StyleBundle("~/Content/AddEditCSS").Include(
                    "~/Content/css/appy-platter/css/font-awesome.min.css",
                    "~/Content/bootstrap.min.css",
                    "~/Content/css/prityy_photo/css/lightbox.css",
                    "~/Content/css/custom_style.css",
                    "~/Content/css/add_edit.css"
                ));

           


        }
    }
}
