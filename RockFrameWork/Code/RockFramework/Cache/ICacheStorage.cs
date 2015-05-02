using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockFramework.Cache
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    interface ICacheStorage
    {

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        void Insert(string key, object value);

        /// <summary>
        /// 添加缓存【绝对过期时间=指定时间过期】
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiration">过期绝对时间</param>
        void Insert(string key, object value, DateTime expiration);

        /// <summary>
        /// 添加缓存【相对多久时间过期】
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiration">过期时间</param>
        void Insert(string key, object value, TimeSpan expiration);

        /// <summary>
        /// 根据key移除缓存
        /// </summary>
        /// <param name="key">key</param>
        void Remove(string key);

        /// <summary>
        /// 缓存是否存在key的value值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 获取key的缓存值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>缓存key对应的value</returns>
        object Get(string key);

        /// <summary>
        /// 获取所有缓存key List列表
        /// </summary>
        /// <returns>缓存 key List</returns>
        List<string> GetAllCacheKey();

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        void Flush();

    }
}
