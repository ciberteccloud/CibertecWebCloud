using System.Web.Optimization;

namespace Cibertec.Angular
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/site")
                .Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap")
                .IncludeDirectory("~/Libraries/bootstrap/css", "*.css"));
            
            bundles.Add(new ScriptBundle("~/bundles/required")
                .Include("~/Libraries/jquery.js")
                .Include("~/Libraries/bootstrap/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/Libraries/angular/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/dependencies")
                .IncludeDirectory("~/Libraries/angular/dependencies", "*.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/App/app.js")
               .IncludeDirectory("~/App/shared", "*.js", true)          
               .Include("~/App/app.routes.js")
               .Include("~/App/app.controller.js")
               .Include("~/App/app.config.js")
               .IncludeDirectory("~/App/components", "*.js", true)
               .IncludeDirectory("~/App/private", "*.js", true)
               .IncludeDirectory("~/App/public", "*.js", true));


            bundles.Add(new DynamicFolderBundle("js", "*.js", false, new JsMinify()));
            bundles.Add(new DynamicFolderBundle("css", "*.css", false, new CssMinify()));

#if DEBUG
            BundleTable.EnableOptimizations = true;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
