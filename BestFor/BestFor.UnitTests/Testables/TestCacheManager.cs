using System.Diagnostics.CodeAnalysis;
using BestFor.Services.Cache;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.UnitTests.Testables
{
    /// <summary>
    /// Simulates cache manager.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TestCacheManager
    {
        public Dictionary<string, object> Cache;

        public Mock<ICacheManager> CacheMock;

        public TestCacheManager()
        {
            Cache = new Dictionary<string, object>();
            CacheMock = new Mock<ICacheManager>();
            // Setup cache to store in dictionary
            CacheMock.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<object>()))
                .Callback((string key, object value) => Cache.Add(key, value))
                .Returns((string key, object value) => value);
            CacheMock.Setup(x => x.Get(It.IsAny<string>()))
                .Returns((string key) => Cache.ContainsKey(key) ? Cache[key] : null);
        }
        public void ClearCache()
        {
            Cache.Clear();
        }

    }
}
