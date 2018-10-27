namespace Nintex.UrlShortener.UI.Controllers
{
    using System.Web.Mvc;

    using Nintex.UrlShortener.DataAccess.BuisnessLayer;
    using Nintex.UrlShortener.DataAccess.Cache;

    public class HomeController : Controller
    {
        private IUrlShortenerService urlShortenerService;
        public HomeController(IUrlShortenerService urlShortenerService)
        {
            this.urlShortenerService = urlShortenerService;
        }
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GetShortUrl
        /// </summary>
        /// <param name="originalUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetShortUrl(string originalUrl)
        {
            var shortUrl = UrlShortenerCache.Instance.GetShortUrl(originalUrl);
            if (shortUrl == null)
            {
                shortUrl = this.urlShortenerService.CreateShortUrl(originalUrl);
            }
            shortUrl.ShortUrl = string.Format("{0}://{1}/{2}", Request.Url.Scheme, Request.Url.Authority, shortUrl.UniqueId);
            return this.Json(shortUrl.ShortUrl, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetOriginalUrl
        /// </summary>
        /// <param name="inputUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOriginalUrl(string inputUrl)
        {
            var result = string.Empty;
            var siteUrl= string.Format("{0}://{1}/", Request.Url.Scheme, Request.Url.Authority);
            var shortUrl = UrlShortenerCache.Instance.GetOriginalUrl(inputUrl.Replace(siteUrl,""));
            result = shortUrl == null ? "Url Not Found" : shortUrl.OriginalUrl;
            return this.Json(new { IsSuccess= shortUrl != null, OriginalUrl= result}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetOriginalUrl
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public ActionResult Click(string segment)
        {
            var shortUrl = UrlShortenerCache.Instance.GetOriginalUrl(segment);
            if (shortUrl != null) {
                return this.RedirectPermanent(shortUrl.OriginalUrl);
            }
            return View("~/Views/Shared/Error404.cshtml");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Url Shortener Demo!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}