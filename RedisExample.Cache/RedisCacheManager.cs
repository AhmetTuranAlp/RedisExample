using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisExample.Cache
{
    public class RedisCacheManager
    {
        private RedisCacheOptions options;
        public RedisCacheManager()
        {
            options = new RedisCacheOptions
            {
                Configuration = "localhost:6379",
                InstanceName = ""
            };
        }



        /// <summary>
        /// Data Okuma
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> GetAll<T>(string key)
        {
            using (var redisCache = new RedisCache(options))
            {
                var DataCache = redisCache.Get(key);
                if (DataCache != null)
                {
                    var json = Encoding.UTF8.GetString(DataCache);
                    var asd= JsonConvert.DeserializeObject<List<T>>(json);
                    return asd;
                }

                return default(List<T>);
            }
        }

        /// <summary>
        /// Data Ekleme, Düzenleme
        /// </summary>
        /// <param name="key"></param>
        /// <param name="valueObject"></param>
        /// <param name="expiration"></param>
        public void Set(string key, object valueObject, int expiration)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiration)
            };

            using (var redisCache = new RedisCache(options))
            {
                var valueString = JsonConvert.SerializeObject(valueObject);
                redisCache.SetString(key, valueString, cacheOptions);
            }
        }

        /// <summary>
        /// Data Silme
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            using (var redisCache = new RedisCache(options))
            {
                redisCache.Remove(key);
            }
        }

        /// <summary>
        /// Data Silme (InstanceName verilen deger kayatılarıun önüne eklenmektedir. Bu ek ile silme işlemi yapılmaktadır.)
        /// </summary>
        /// <param name="pattern"></param>
        public void RemoveByPattern(string pattern)
        {
            using (var redisConnection = ConnectionMultiplexer.Connect(options.Configuration))
            {
                var redisServer = redisConnection.GetServer(redisConnection.GetEndPoints().First());
                var redisDatabase = redisConnection.GetDatabase();

                foreach (var key in redisServer.Keys(pattern: pattern))
                {
                    redisDatabase.KeyDelete(key);
                }
            }
        }
    }
}
