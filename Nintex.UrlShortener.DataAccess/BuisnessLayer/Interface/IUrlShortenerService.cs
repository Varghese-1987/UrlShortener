namespace Nintex.UrlShortener.DataAccess.BuisnessLayer
{
    using Nintex.UrlShortener.DataAccess.ViewModels;
    public interface IUrlShortenerService
    {
        #region CRUD

        /// <summary>
        /// GetShortUrl
        /// </summary>
        /// <param name="originalUrl"></param>
        /// <returns></returns>
        ShortUrlVM GetShortUrl(string originalUrl);

        /// <summary>
        /// GetOriginalUrl
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        ShortUrlVM GetOriginalUrl(string uniqueId);

        /// <summary>
        /// CreateShortUrl
        /// </summary>
        /// <param name="originalUrl"></param>
        /// <returns></returns>
        ShortUrlVM CreateShortUrl(string originalUrl);

        #endregion
    }
}
