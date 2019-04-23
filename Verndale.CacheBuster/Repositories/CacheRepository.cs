using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.DataAbstraction;
using EPiServer.Framework.Cache;
using EPiServer.Logging;
using EPiServer.ServiceLocation;
using Verndale.CacheBuster.Models;

namespace Verndale.CacheBuster.Repositories
{
    [ServiceConfiguration(ServiceType = typeof(ICacheRepository))]
    public class CacheRepository : ICacheRepository
    {
        #region Properties

        private readonly ISynchronizedObjectInstanceCache Cache;
        private static readonly ILogger Logger = LogManager.GetLogger();

        #endregion

        #region Constructor

        public CacheRepository(ISynchronizedObjectInstanceCache cache)
        {
            //Assert.NotNull(cache, "Cache is null");

            Cache = cache;
        }

        public CacheRepository()
        {
                
        }
      
        #endregion

        public void Remove(string key)
        {
            try
            {
                Cache.RemoveLocal(key);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        public List<CacheItem> GetByTerm(string term)
        {
            var cacheItems = new List<CacheItem>();

            try
            {
                var cache = HttpRuntime.Cache;

                foreach (DictionaryEntry c in cache)
                {
                    if (c.Key.ToString().Contains(term))
                    {
                        var value = GetValue(c.Value);
                        cacheItems.Add(new CacheItem(c.Key.ToString(), c.Value.GetType().ToString(), value));
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return cacheItems;
        }

        public string GetValue(object obj)
        {
            var value = "";

            switch (obj)
            {
                case int type:
                    value = type.ToString();
                    break;
                case long type:
                    value = type.ToString();
                    break;
                case decimal type:
                    value = type.ToString();
                    break;
                case double type:
                    value = type.ToString();
                    break;
                case string type:
                    value = type;
                    break;
                case PropertyDefinition type:
                    value = type.Name;
                    break;
                case BlockType type:
                    value = type.Name;
                    break;
                case PageType type:
                    value = type.Name;
                    break;
                case ContentType type:
                    value = type.Name;
                    break;
                case List<PropertyDefinition> types:
                    value = string.Join(" | ", types.Select(p => p.Name));
                    break;
            }

            return value;
        }
    }
}