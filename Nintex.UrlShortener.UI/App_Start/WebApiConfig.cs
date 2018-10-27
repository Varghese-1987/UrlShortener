namespace Nintex.UrlShortener.UI
{
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "API/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
