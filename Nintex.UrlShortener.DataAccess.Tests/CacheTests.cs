namespace Nintex.UrlShortener.DataAccess.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Nintex.UrlShortener.DataAccess.Cache;

    [TestClass]
    public class CacheTests
    {
        [TestMethod]
        public void UrlShortenerCacheReloadTest()
        {
            UrlShortenerCache.Instance.FullCacheReload();
            Assert.IsTrue(true);
        }
    }
}
