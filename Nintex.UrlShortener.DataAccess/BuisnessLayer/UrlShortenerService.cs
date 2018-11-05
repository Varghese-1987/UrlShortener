namespace Nintex.UrlShortener.DataAccess.BuisnessLayer
{
    using System;

    using Nintex.UrlShortener.DataAccess.ViewModels;
    using Nintex.UrlShortener.Model.Entities;
    using Nintex.UrlShortener.DataAccess.Helpers;
    using Nintex.UrlShortener.DataAccess.Cache;

    public class UrlShortenerService : IUrlShortenerService
    {
        private IRepository repository;
        public UrlShortenerService(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// CreateShortUrl
        /// </summary>
        /// <param name="originalUrl"></param>
        /// <returns></returns>
        public ShortUrlVM CreateShortUrl(string originalUrl)
        {
            var existingShortUrl = repository.GetOne<ShortUrl>(x => x.OriginalUrl == originalUrl);
            if (existingShortUrl == null)
            {
                existingShortUrl = new ShortUrl()
                {
                    OriginalUrl = originalUrl,
                    UniqueId = this.GetUniqueId(UniqueIdHelper.GetUniqueId()),
                    CreatedDate = DateTime.UtcNow
                };
                repository.Create<ShortUrl>(existingShortUrl);
                repository.Save();

                //Update Cache
                UrlShortenerCache.Instance.FullCacheReload();
            }
            return new ShortUrlVM(existingShortUrl);
        }

        /// <summary>
        /// GetOriginalUrl
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        public ShortUrlVM GetOriginalUrl(string uniqueId)
        {
            var existingShortUrl = repository.GetOne<ShortUrl>(x => x.UniqueId.Equals(uniqueId));
            return existingShortUrl !=null? new ShortUrlVM (existingShortUrl):new ShortUrlVM();
        }

        /// <summary>
        /// GetShortUrl
        /// </summary>
        /// <param name="originalUrl"></param>
        /// <returns></returns>
        public ShortUrlVM GetShortUrl(string originalUrl)
        {
            var existingShortUrl = repository.GetOne<ShortUrl>(x => x.OriginalUrl.Equals(originalUrl));
            return existingShortUrl != null ? new ShortUrlVM(existingShortUrl) : new ShortUrlVM();
        }


        #region private methods
        /// <summary>
        /// Additional checking to ensure duplicate unique ids are not present
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        private string GetUniqueId(string uniqueId)
        {
            if (UrlShortenerCache.Instance.GetOriginalUrl(uniqueId) == null)
            {
                return uniqueId;
            }
            else
            {
                GetUniqueId(UniqueIdHelper.GetUniqueId());
            }
            return uniqueId;
        }
        #endregion
    }
}
