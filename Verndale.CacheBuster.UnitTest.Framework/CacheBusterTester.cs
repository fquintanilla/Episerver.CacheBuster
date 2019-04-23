using System.Linq;
using EPiServer.Framework.Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Verndale.CacheBuster.Repositories;

namespace Verndale.CacheBuster.UnitTest.Framework
{
    [TestClass]
    public class CacheBusterTester
    {
        protected HttpRuntimeCache Cache;
        protected ICacheRepository Repository;

        [TestInitialize]
        public void Setup()
        {
            Cache = new HttpRuntimeCache();
            
            Cache.Insert("test1", "hello world", CacheEvictionPolicy.Empty);
            Cache.Insert("test2", "hello world 2", CacheEvictionPolicy.Empty);
            Cache.Insert("test3", "hello world 3", CacheEvictionPolicy.Empty);
            Cache.Insert("test4", "hello world 4", CacheEvictionPolicy.Empty);
            Cache.Insert("test5", "hello world 5", CacheEvictionPolicy.Empty);
            Cache.Insert("test6", "hello world 6", CacheEvictionPolicy.Empty);
            Cache.Insert("test7", "hello world 7", CacheEvictionPolicy.Empty);
            Cache.Insert("test8", "hello world 8", CacheEvictionPolicy.Empty);
            Cache.Insert("test9", "hello world 9", CacheEvictionPolicy.Empty);
            Cache.Insert("test10", "hello world 10", CacheEvictionPolicy.Empty);
            Cache.Insert("test11", "hello world 11", CacheEvictionPolicy.Empty);
            Cache.Insert("test12", "hello world 12", CacheEvictionPolicy.Empty);
            Cache.Insert("test13", "hello world 13", CacheEvictionPolicy.Empty);
            Cache.Insert("test14", "hello world 14", CacheEvictionPolicy.Empty);
            Cache.Insert("test15", "hello world", CacheEvictionPolicy.Empty);
           
            Repository = new CacheRepository();
        }

        [TestMethod]
        public void Should_retrieve_test_items()
        {
            Assert.AreEqual(15, Repository.GetByTerm("test").Count);
        }

        [TestMethod]
        public void Should_not_retrieve_items()
        {
            Assert.AreEqual(0, Repository.GetByTerm("invalidtokenfortesting").Count);
        }

        [TestMethod]
        public void Should_return_hello_world()
        {
            Assert.AreEqual("hello world", Repository.GetByTerm("test15").First().Value);
        }

        [TestMethod]
        public void Should_return_string_as_datatype()
        {
            Assert.AreEqual("System.String", Repository.GetByTerm("test15").First().Type);
        }

        [TestMethod]
        public void Should_return_test15_as_key()
        {
            Assert.AreEqual("test15", Repository.GetByTerm("test15").First().Key);
        }
    }
}
