namespace Nintex.UrlShortener.DataAccess.Helpers
{
    using System;
    public static class UniqueIdHelper
    {
        public static string GetUniqueId()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "").Replace("/", "").Replace("+", "");
        }
    }
}
