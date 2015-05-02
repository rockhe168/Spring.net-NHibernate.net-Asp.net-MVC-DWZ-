using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace RockFramework.Cache.Impl
{
    /// <summary>
    /// 默认采用asp.net中Cache对象
    /// </summary>
    public class DefaultCacheAdapter:ICacheStorage
    {
        /// <summary>
        /// 当前请求上下文
        /// </summary>
        private static HttpContext context = null;

        /// <summary>
        /// 初始化当前上下文
        /// </summary>
        static  DefaultCacheAdapter()
        {
            context = HttpContext.Current;
        }


        #region ICacheStorage上下文

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Insert(string key, object value)
        {
            context.Cache.Insert(key,value);
        }

        /// <summary>
        /// 添加缓存【绝对过期时间=指定时间过期】(默认滑动时间为20分钟)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiration">过期绝对时间</param>
        public void Insert(string key, object value, DateTime expiration)
        {
            context.Cache.Insert(key,value,null,expiration,TimeSpan.FromMinutes(20), CacheItemPriority.Normal,null);
        }

        /// <summary>
        /// 添加缓存【相对多久时间过期】
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiration">过期时间</param>
        public void Insert(string key, object value, TimeSpan expiration)
        {
            context.Cache.Insert(key,value,null,DateTime.MaxValue,expiration,CacheItemPriority.Normal,null);
        }

        /// <summary>
        /// 根据key移除缓存
        /// </summary>
        /// <param name="key">key</param>
        public void Remove(string key)
        {
            if(Exists(key))
               context.Cache.Remove(key);
        }

        /// <summary>
        /// 缓存是否存在key的value值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (context.Cache[key] == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取key的缓存值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>缓存key对应的value</returns>
        public object Get(string key)
        {
            return context.Cache[key];
        }

        /// <summary>
        /// 获取所有缓存key List列表
        /// </summary>
        /// <returns>缓存 key List</returns>
        public List<string> GetAllCacheKey()
        {
            var list = new List<string>();

            var keys=context.Cache.GetEnumerator();

            while (keys.MoveNext())
            {
                list.Add(keys.Key.ToString());
            }

            return list;

        }

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public void Flush()
        {
            var list = GetAllCacheKey();

            foreach (var key in list)
            {
                Remove(key);
            }
        }

        #endregion
    }
}
