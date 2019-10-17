using System;
using System.Threading;
using System.Threading.Tasks;

using Xunit;
using Some.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace Some.UnitTest.Services
{
    public class SomeService_It_Should_CreateSomething
    {
        public ISomeService myClass;
        public MockCache cache;

        public class MockCache : IDistributedCache
        {
            public byte[] Get(string key)
            {
                throw new System.NotImplementedException();
            }

            public Task<byte[]> GetAsync(string key, CancellationToken token = default)
            {
                throw new System.NotImplementedException();
            }

            public void Refresh(string key)
            {
                throw new System.NotImplementedException();
            }

            public Task RefreshAsync(string key, CancellationToken token = default)
            {
                throw new System.NotImplementedException();
            }

            public void Remove(string key)
            {
                throw new System.NotImplementedException();
            }

            public Task RemoveAsync(string key, CancellationToken token = default)
            {
                throw new System.NotImplementedException();
            }

            public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
            {
                throw new System.NotImplementedException();
            }

            public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
            {
                throw new System.NotImplementedException();
            }
        }
    

        [Fact]
        public void Test1()
        {
            MockCache _cache = new MockCache();

            ISomeService myClass = new SomeService(_cache);

            var result = myClass.CreateSomething("doggy", "catty");

            Assert.IsType(Type.GetType("System.String"), result.id);
            Assert.Equal("doggy", result.firstName);
            Assert.Equal("catty", result.lastName);
        }
    }
}