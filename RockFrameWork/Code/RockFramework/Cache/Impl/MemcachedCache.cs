using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockFramework.Cache.Impl
{

    /// <summary>
    /// 分布式Memcached缓存实现
    /// </summary>
    public class MemcachedCache : ICacheStorage
    {
        public void Insert(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(string key, object value, DateTime expiration)
        {
            throw new NotImplementedException();
        }

        public void Insert(string key, object value, TimeSpan expiration)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllCacheKey()
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }
    }
}
