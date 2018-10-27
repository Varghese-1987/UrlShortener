namespace Nintex.UrlShortener.UI
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using log4net.Config;
    
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Log 4 net
        /// </summary>
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Application start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            XmlConfigurator.Configure();

        }
        /// <summary>
        /// Application error
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Log.ErrorFormat("Current user name is {0}", HttpContext.Current.User != null && HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated ? HttpContext.Current.User.Identity.Name : "unauthorized");
            Log.Error(string.Format("Error page is {0}", HttpContext.Current.Request.Url.ToString()), exception);
            Log.Info(Environment.NewLine);
        }
    }
}
