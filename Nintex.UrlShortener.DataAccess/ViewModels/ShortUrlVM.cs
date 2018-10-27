namespace Nintex.UrlShortener.DataAccess.ViewModels
{
    using Nintex.UrlShortener.Model.Entities;
    public class ShortUrlVM
    {

        public ShortUrlVM(ShortUrl shortUrl)
        {
            this.UniqueId = shortUrl.UniqueId;
            this.OriginalUrl = shortUrl.OriginalUrl;
        }

        public ShortUrlVM()
        {

        }
        /// <summary>
        /// Gets Or Sets UniqueId
        /// </summary>
        public string  UniqueId { get; set; }

        /// <summary>
        /// Gets Or Sets Original Url
        /// </summary>
        public string  OriginalUrl { get; set; }

        /// <summary>
        /// Gets Or Sets Short Url
        /// </summary>
        public string ShortUrl { get; set; }
    }
}
