using System;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Mardis.Engine.Framework
{
    public class RedisCache
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(
            () => ConnectionMultiplexer.Connect("engine.redis.cache.windows.net:6380,password=0+gPTLWo+jkZGVZdrgcafHTkQi7cm50gXuBI/vGcrCg=,ssl=True,abortConnect=False,ConnectTimeout=1800000000 , KeepAlive = 120")
            
            );
        //() => ConnectionMultiplexer.Connect("localhost"));

        private static ConnectionMultiplexer Connection => LazyConnection.Value;

        // protected static IDatabase Cache = Connection.GetDatabase();

        public T Get<T>(string key)
        {
            IDatabase Cache = Connection.GetDatabase();
            RedisValue objJson = Cache.StringGet(key);
            T result = default(T);
            if (objJson.HasValue)
            {
                result = JsonConvert.DeserializeObject<T>(objJson);
            }
            return result;
        }

        public void Set(string key, object value)
        {
            IDatabase Cache = Connection.GetDatabase();
            Cache.StringSet(key, JsonConvert.SerializeObject(value));
        }
    }
}
