namespace Nintex.UrlShortener.DataAccess.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;

    using Nintex.UrlShortener.DataAccess.ViewModels;
    using Nintex.UrlShortener.Model.Entities;
   

    public sealed class UrlShortenerCache
    {
        /// <summary>
        /// Sync object
        /// </summary>
        private static object root = new object();

        /// <summary>
        /// Cache object
        /// </summary>
        private static volatile UrlShortenerCache cache;

        /// <summary>
        /// For only one thread in one time access
        /// </summary>
        private readonly ReaderWriterLockSlim slimLock = new ReaderWriterLockSlim();

        /// <summary>
        /// All items
        /// </summary>
        private HashSet<ShortUrlVM> itemsList;


        /// <summary>
        /// Repository Interface
        /// </summary>
        UrlShortenerContext repository = new UrlShortenerContext();

        private UrlShortenerCache()
        {
            this.InitializeCache();
        }

        /// <summary>
        /// Gets Cache instance
        /// </summary>
        public static UrlShortenerCache Instance
        {
            get
            {
                if (cache == null)
                {
                    lock (root)
                    {
                        if (cache == null)
                        {
                            cache = new UrlShortenerCache();
                        }
                    }
                }

                return cache;
            }
        }

        /// <summary>
        /// GetShortUrl
        /// </summary>
        /// <param name="originalUrl"></param>
        /// <returns></returns>
        public ShortUrlVM GetShortUrl(string originalUrl)
        {
            this.slimLock.EnterWriteLock();
            var item = this.GetFromCacheByExpression(x => x.OriginalUrl == originalUrl);
            this.slimLock.ExitWriteLock();
            return item ?? null;
        }


        /// <summary>
        /// GetOriginalUrl
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        public ShortUrlVM GetOriginalUrl(string uniqueId)
        {
            this.slimLock.EnterWriteLock();
            var item = this.GetFromCacheByExpression(x => x.UniqueId == uniqueId);
            this.slimLock.ExitWriteLock();
            return item ?? null;    
        }

        /// <summary>
        /// GetFromCacheByExpression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private ShortUrlVM GetFromCacheByExpression(Expression<Func<ShortUrlVM, bool>> expression)
        {
            return itemsList.AsQueryable().FirstOrDefault(expression);
        }

        /// <summary>
        /// Reload Full Cache
        /// </summary>
        public void FullCacheReload()
        {
            this.slimLock.EnterWriteLock();
            this.InitializeCache();
            this.slimLock.ExitWriteLock();
        }

        /// <summary>
        /// Initialize Cache
        /// </summary>
        private void InitializeCache()
        {
            this.itemsList = new HashSet<ShortUrlVM>();
            var allItems = new EFRepository<UrlShortenerContext>(new UrlShortenerContext()).GetAll<ShortUrl>();
            foreach(var item in allItems)
            {
                this.itemsList.Add(new ShortUrlVM(item));
            }

        }
    }
}
