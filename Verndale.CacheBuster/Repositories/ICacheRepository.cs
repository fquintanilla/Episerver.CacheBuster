using System.Collections.Generic;
using Verndale.CacheBuster.Models;

namespace Verndale.CacheBuster.Repositories
{
    public interface ICacheRepository
    {
        void Remove(string key);

        string GetValue(object obj);

        List<CacheItem> GetByTerm(string term);
    }
}