using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Cache
{
	using System.Web;
	using System.Web.Caching;
	using System.Web.Helpers;

	using StackExchange.Redis;

	public class CacheServiceRedis : ICacheService
	{
		public static readonly ConnectionMultiplexer Client = ConnectionMultiplexer.Connect("localhost");

		public static CacheDependency CreateDependency(string key)
		{
			return new RedisCacheDependency(key);
		}

		public T GetCached<T>(string pKey, Func<T> getter, string @group)
			where T : class
		{
			var key = @group + pKey;

			var localCache = HttpRuntime.Cache;
			var result = (T) localCache.Get(key);
			if (result != null) 
				return result;

			var redisDb = Client.GetDatabase();

			var value = redisDb.StringGet(key);
			if (!value.IsNullOrEmpty)
			{
				result = Json.Decode<T>(value);
				localCache.Insert(key, result, CreateDependency(key));
				return result;
			}

			result = getter();

			redisDb.StringSet(key, Json.Encode(result));
			localCache.Insert(key, result, CreateDependency(key));
			return result;
		}


		public static void DeleteKey(string key)
		{
			HttpRuntime.Cache.Remove(key);
			var redisDb = Client.GetDatabase();
			redisDb.KeyDelete(key);
		}

		#region ICacheService

		//public long Count(string @group)
		//{
		//	throw new NotImplementedException();
		//}

		//public void AddOrUpdate<T>(string key, T value, string @group, Action<string, T> afterItemRemoved = null, Action<string, T> beforeItemRemoved = null) where T : class
		//{
		//	var redisDb = Client.GetDatabase();
		//	var localCache = HttpRuntime.Cache;

		//	redisDb.StringSet(key, Json.Encode(value));
		//	localCache.Insert(key, value, CreateDependency(key));
		//}

		//public void AddOrUpdate<T>(
		//	string key,
		//	T value,
		//	string @group,
		//	DateTimeOffset absoluteExpiration,
		//	Action<string, T> afterItemRemoved = null,
		//	Action<string, T> beforeItemRemoved = null) where T : class
		//{
		//	throw new NotImplementedException();
		//}

		//public void AddOrUpdate<T>(
		//	string key,
		//	T value,
		//	string @group,
		//	TimeSpan slidingExpiration,
		//	Action<string, T> afterItemRemoved = null,
		//	Action<string, T> beforeItemRemoved = null) where T : class
		//{
		//	throw new NotImplementedException();
		//}

		//public void Clear(string @group)
		//{
		//	throw new NotImplementedException();
		//}

		//public bool Contains(string key, string @group)
		//{
		//	throw new NotImplementedException();
		//}

		//public T Get<T>(string key, string @group) where T : class
		//{
		//	throw new NotImplementedException();
		//}

		//public IEnumerable<T> GetByGroupKey<T>(string @group) where T : class
		//{
		//	throw new NotImplementedException();
		//}

		//public T GetOrAdd<T>(string key, Func<T> value, string @group, Action<string, T> afterItemRemoved = null, Action<string, T> beforeItemRemoved = null) where T : class
		//{
		//	var localCache = HttpRuntime.Cache;
		//	var result = (T)localCache.Get(key);

		//	if (result != null)
		//		return result;

		//	var redisDb = Client.GetDatabase();

		//	var value = redisDb.StringGet(key);

		//	if (!value.IsNullOrEmpty)
		//	{
		//		result = Json.Decode<T>(value);
		//		localCache.Insert(key, result, CreateDependency(key));
		//		return result;
		//	}

		//	result = getter();
		//}

		//public T GetOrAdd<T>(
		//	string key,
		//	Func<T> value,
		//	string @group,
		//	DateTimeOffset absoluteExpiration,
		//	Action<string, T> afterItemRemoved = null,
		//	Action<string, T> beforeItemRemoved = null) where T : class
		//{
		//	throw new NotImplementedException();
		//}

		//public T GetOrAdd<T>(
		//	string key,
		//	Func<T> value,
		//	string @group,
		//	TimeSpan slidingExpiration,
		//	Action<string, T> afterItemRemoved = null,
		//	Action<string, T> beforeItemRemoved = null) where T : class
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<T> GetOrAddAsync<T>(
		//	string key,
		//	Func<Task<T>> value,
		//	string @group,
		//	Action<string, T> afterItemRemoved = null,
		//	Action<string, T> beforeItemRemoved = null) where T : class
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<T> GetOrAddAsync<T>(
		//	string key,
		//	Func<Task<T>> value,
		//	string @group,
		//	DateTimeOffset absoluteExpiration,
		//	Action<string, T> afterItemRemoved = null,
		//	Action<string, T> beforeItemRemoved = null) where T : class
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<T> GetOrAddAsync<T>(
		//	string key,
		//	Func<Task<T>> value,
		//	string @group,
		//	TimeSpan slidingExpiration,
		//	Action<string, T> afterItemRemoved = null,
		//	Action<string, T> beforeItemRemoved = null) where T : class
		//{
		//	throw new NotImplementedException();
		//}

		//public void Remove(string key, string @group)
		//{
		//	throw new NotImplementedException();
		//}
		#endregion
	}
}
