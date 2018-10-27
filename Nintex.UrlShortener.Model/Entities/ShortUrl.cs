namespace Nintex.UrlShortener.Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UrlShortener.ShortUrl")]
    public class ShortUrl
    {
        /// <summary>
        /// Gets Or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets Or Sets UniqueId
        /// </summary>
        [Required]
        [StringLength(22)]
        public string UniqueId { get; set; }

        /// <summary>
        /// Gets Or Sets Original Url
        /// </summary>
        [Required]
        [StringLength(2083)]
        public string OriginalUrl { get; set; }

        /// <summary>
        /// Gets Or Sets Created Date
        /// </summary>
        [Required]
        public DateTime CreatedDate { get; set; }

    }
}
