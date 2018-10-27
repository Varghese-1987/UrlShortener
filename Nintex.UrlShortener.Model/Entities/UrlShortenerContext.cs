
namespace Nintex.UrlShortener.Model.Entities
{
    using System.Data.Entity;

    public  class UrlShortenerContext:DbContext
    {
        public UrlShortenerContext():base("UrlShortener")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Gets or sets ShortUrls
        /// </summary>
        public virtual DbSet<ShortUrl> ShortUrls { get; set; }
    }
}
