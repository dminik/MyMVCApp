namespace ServiceLayer.Cache
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Caching;
	using System.Threading.Tasks;
	using System.Web;
	using System.Web.Helpers;

	/// <summary>
	/// A cached collection of key value pairs. This can be used to cache objects internally on the server.
	/// </summary>
	public sealed class CacheServiceInMemory : ICacheService
	{
		private readonly MemoryCache memoryCache;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheServiceInMemory"/> class using the default <see cref="MemoryCache"/>.
		/// </summary>
		public CacheServiceInMemory()
		{
			this.memoryCache = MemoryCache.Default;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheServiceInMemory"/> class.
		/// </summary>
		/// <param name="memoryCache">The memory cache.</param>
		public CacheServiceInMemory(MemoryCache memoryCache)
		{
			this.memoryCache = memoryCache;
		}

		#endregion

		#region Public Properties

		public T GetCached<T>(string key, Func<T> getter, string @group) 
			where T : class
		{
			var localCache = HttpRuntime.Cache;
			var result = (T)localCache.Get(key);

			if (result == null)
			{
				result = this.Get<T>(key, group);

				if (result == null)
				{
					result = getter();
					this.AddOrUpdate(key, result, group);
					localCache.Insert(key, result);
				}
			}

			return result;
		}


		/// <summary>
		/// Gets the total number of cache entries in the cache.
		/// </summary>
		public long Count(string group)
		{
			return this.memoryCache.Count(x => x.Key.StartsWith(group));			
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Inserts a cache entry into the cache by using a key, a value and no expiration.
		/// </summary>
		/// <typeparam name="T">The type of the cache entry value.</typeparam>
		/// <param name="key">A unique identifier for the cache entry to insert.</param>
		/// <param name="value">The data for the cache entry.</param>
		/// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
		/// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
		public void AddOrUpdate<T>(
			string key,
			T value,
			string group,
			Action<string, T> afterItemRemoved = null,
			Action<string, T> beforeItemRemoved = null) where T : class
		{
			this.AddOrUpdate(key, value, group, null, null, afterItemRemoved, beforeItemRemoved);
		}

		/// <summary>
		/// Inserts a cache entry into the cache by using a key, a value and absolute expiration.
		/// </summary>
		/// <typeparam name="T">The type of the cache entry value.</typeparam>
		/// <param name="key">A unique identifier for the cache entry to insert.</param>
		/// <param name="value">The data for the cache entry.</param>
		/// <param name="absoluteExpiration">The absolute expiration time, after which the cache entry will expire.</param>
		/// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
		/// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
		public void AddOrUpdate<T>(
			string key,
			T value,
			string group,
			DateTimeOffset absoluteExpiration,
			Action<string, T> afterItemRemoved = null,
			Action<string, T> beforeItemRemoved = null) where T : class
		{
			this.AddOrUpdate(key, value, group, absoluteExpiration, null, afterItemRemoved, beforeItemRemoved);
		}

		/// <summary>
		/// Inserts a cache entry into the cache by using a key, a value and sliding expiration.
		/// </summary>
		/// <typeparam name="T">The type of the cache entry value.</typeparam>
		/// <param name="key">A unique identifier for the cache entry to insert.</param>
		/// <param name="value">The data for the cache entry.</param>
		/// <param name="slidingExpiration">The sliding expiration time, after which the cache entry will expire.</param>
		/// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
		/// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
		public void AddOrUpdate<T>(
			string key,
			T value,
			string group,
			TimeSpan slidingExpiration,
			Action<string, T> afterItemRemoved = null,
			Action<string, T> beforeItemRemoved = null) where T : class
		{
			this.AddOrUpdate(key, value, group, null, slidingExpiration, afterItemRemoved, beforeItemRemoved);
		}

		/// <summary>
		/// Clears all cache entry items from the cache.
		/// </summary>
		public void Clear(string group)
		{
			var keysToDel = this.memoryCache.Where(x => x.Key.StartsWith(group)).Select(item => item.Key).ToList();
			keysToDel.ForEach(x=>this.memoryCache.Remove(x));			
		}

		/// <summary>
		/// Determines whether a cache entry exists in the cache with the specified key.
		/// </summary>
		/// <param name="key">A unique identifier for the cache entry.</param>
		/// <returns><c>true</c> if a cache entry exists, otherwise <c>false</c>.</returns>
		/// <exception cref="System.ArgumentNullException">key is null.</exception>
		public bool Contains(string key, string group)
		{
			return this.memoryCache.Contains(group + key);
		}

		/// <summary>
		/// Gets an entry from the cache with the specified key.
		/// </summary>
		/// <typeparam name="T">The type of the cache entry to get.</typeparam>
		/// <param name="key">A unique identifier for the cache entry to get.</param>
		/// <returns>A reference to the cache entry that is identified by key, if the entry exists; otherwise, <c>null</c>.</returns>
		/// <exception cref="System.ArgumentNullException">key is null.</exception>
		public T Get<T>(string key, string group) where T : class
		{
			return (T)this.memoryCache.Get(group + key);
		}

		public IEnumerable<T> GetByGroupKey<T>(string group) where T : class
		{
			return this.memoryCache.Where(x => x.Key.StartsWith(group)).Select(y=>y.Value as T).ToList();			
		}

		

		/// <summary>
		/// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
		/// cache entry into the cache by using a key, a value and no expiration and returns it.
		/// </summary>
		/// <typeparam name="T">The type of the cache entry value.</typeparam>
		/// <param name="key">A unique identifier for the cache entry to insert.</param>
		/// <param name="value">The data for the cache entry.</param>
		/// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
		/// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
		/// <returns>A reference to the cache entry that is identified by key.</returns>
		public T GetOrAdd<T>(
			string key,
			Func<T> value,
			string group,
			Action<string, T> afterItemRemoved = null,
			Action<string, T> beforeItemRemoved = null) where T : class
		{
			return this.GetOrAdd(key, value, group, null, null, afterItemRemoved, beforeItemRemoved);
		}

		/// <summary>
		/// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
		/// cache entry into the cache by using a key, a value and absolute expiration and returns it.
		/// </summary>
		/// <typeparam name="T">The type of the cache entry value.</typeparam>
		/// <param name="key">A unique identifier for the cache entry to insert.</param>
		/// <param name="value">The data for the cache entry.</param>
		/// <param name="absoluteExpiration">The absolute expiration time, after which the cache entry will expire.</param>
		/// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
		/// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
		/// <returns>A reference to the cache entry that is identified by key.</returns>
		public T GetOrAdd<T>(
			string key,
			Func<T> value,
			string group,
			DateTimeOffset absoluteExpiration,
			Action<string, T> afterItemRemoved = null,
			Action<string, T> beforeItemRemoved = null) where T : class
		{
			return this.GetOrAdd(key, value, group, absoluteExpiration, null, afterItemRemoved, beforeItemRemoved);
		}

		/// <summary>
		/// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
		/// cache entry into the cache by using a key, a value and sliding expiration and returns it.
		/// </summary>
		/// <typeparam name="T">The type of the cache entry value.</typeparam>
		/// <param name="key">A unique identifier for the cache entry to insert.</param>
		/// <param name="value">The data for the cache entry.</param>
		/// <param name="slidingExpiration">The sliding expiration time, after which the cache entry will expire.</param>
		/// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
		/// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
		/// <returns>A reference to the cache entry that is identified by key.</returns>
		public T GetOrAdd<T>(
			string key,
			Func<T> value,
			string group,
			TimeSpan slidingExpiration,
			Action<string, T> afterItemRemoved = null,
			Action<string, T> beforeItemRemoved = null) where T : class
		{
			return this.GetOrAdd(key, value, group, null, slidingExpiration, afterItemRemoved, beforeItemRemoved);
		}

		/// <summary>
		/// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
		/// cache entry into the cache by using a key, a value and no expiration and returns it.
		/// </summary>
		/// <typeparam name="T">The type of the cache entry value.</typeparam>
		/// <param name="key">A unique identifier for the cache entry to insert.</param>
		/// <param name="value">A function that asynchronously gets the data for the cache entry.</param>
		/// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
		/// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
		/// <returns>A reference to the cache entry that is identified by key.</returns>
		public Task<T> GetOrAddAsync<T>(
			string key,
			Func<Task<T>> value,
			string group,
			Action<string, T> afterItemRemoved = null,
			Action<string, T> beforeItemRemoved = null) where T : class
		{
			return this.GetOrAddAsync(key, value, group, null, null, afterItemRemoved, beforeItemRemoved);
		}

		/// <summary>
		/// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
		/// cache entry into the cache by using a key, a value and absolute expiration and returns it.
		/// </summary>
		/// <typeparam name="T">The type of the cache entry value.</typeparam>
		/// <param name="key">A unique identifier for the cache entry to insert.</param>
		/// <param name="value">A function that asynchronously gets the data for the cache entry.</param>
		/// <param name="absoluteExpiration">The absolute expiration time, after which the cache entry will expire.</param>
		/// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
		/// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
		/// <returns>A reference to the cache entry that is identified by key.</returns>
		public Task<T> GetOrAddAsync<T>(
			string key,
			Func<Task<T>> value,
			string group,
			DateTimeOffset absoluteExpiration,
			Action<string, T> afterItemRemoved = null,
			Action<string, T> beforeItemRemoved = null) where T : class
		{
			return this.GetOrAddAsync(key, value, group, absoluteExpiration, null, afterItemRemoved, beforeItemRemoved);
		}

		/// <summary>
		/// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
		/// cache entry into the cache by using a key, a value and sliding expiration and returns it.
		/// </summary>
		/// <typeparam name="T">The type of the cache entry value.</typeparam>
		/// <param name="key">A unique identifier for the cache entry to insert.</param>
		/// <param name="value">A function that asynchronously gets the data for the cache entry.</param>
		/// <param name="slidingExpiration">The sliding expiration time, after which the cache entry will expire.</param>
		/// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
		/// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
		/// <returns>A reference to the cache entry that is identified by key.</returns>
		public Task<T> GetOrAddAsync<T>(
			string key,
			Func<Task<T>> value,
			string group,
			TimeSpan slidingExpiration,
			Action<string, T> afterItemRemoved = null,
			Action<string, T> beforeItemRemoved = null) where T : class
		{
			return this.GetOrAddAsync(key, value, null, slidingExpiration, afterItemRemoved, beforeItemRemoved);
		}

		/// <summary>
		/// Removes a cache entry from the cache with the specified key.
		/// </summary>
		/// <param name="key">A unique identifier for the cache entry to remove.</param>
		/// <exception cref="System.ArgumentNullException">key is null.</exception>
		public void Remove(string key, string group)
		{
			this.memoryCache.Remove(group + key);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
		/// cache entry into the cache by using a key, a value, a type of expiration and returns it.
		/// </summary>
		/// <typeparam name="T">The type of the cache entry value.</typeparam>
		/// <param name="key">A unique identifier for the cache entry to insert.</param>
		/// <param name="getValue">A function that gets the data for the cache entry.</param>
		/// <param name="absoluteExpiration">The absolute expiration time, after which the cache entry will expire.</param>
		/// <param name="slidingExpiration">The sliding expiration time, after which the cache entry will expire.</param>
		/// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
		/// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
		/// <returns>A reference to the cache entry that is identified by key.</returns>
		private T GetOrAdd<T>(
			string key,
			Func<T> getValue,
			string group,
			DateTimeOffset? absoluteExpiration,
			TimeSpan? slidingExpiration,
			Action<string, T> afterItemRemoved,
			Action<string, T> beforeItemRemoved) where T : class
		{
			T value = this.Get<T>(key, group);

			if (value == null)
			{
				value = getValue();
				this.AddOrUpdate(
					key,
					value,
					group,
					absoluteExpiration,
					slidingExpiration,
					afterItemRemoved,
					beforeItemRemoved);
			}

			return value;
		}

		/// <summary>
		/// Gets an entry from the cache with the specified key, or if the entry does not exist, inserts a
		/// cache entry into the cache by using a key, a value, a type of expiration and returns it.
		/// </summary>
		/// <typeparam name="T">The type of the cache entry value.</typeparam>
		/// <param name="key">A unique identifier for the cache entry to insert.</param>
		/// <param name="getValue">A function that asynchronously gets the data for the cache entry.</param>
		/// <param name="absoluteExpiration">The absolute expiration time, after which the cache entry will expire.</param>
		/// <param name="slidingExpiration">The sliding expiration time, after which the cache entry will expire.</param>
		/// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
		/// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
		/// <returns>A reference to the cache entry that is identified by key.</returns>
		private async Task<T> GetOrAddAsync<T>(
			string key,
			Func<Task<T>> getValue,
			string group,
			DateTimeOffset? absoluteExpiration,
			TimeSpan? slidingExpiration,
			Action<string, T> afterItemRemoved,
			Action<string, T> beforeItemRemoved) where T : class
		{
			T value = this.Get<T>(key, group);

			if (value == null)
			{
				value = await getValue();
				this.AddOrUpdate(
					key,
					value,
					group,
					absoluteExpiration,
					slidingExpiration,
					afterItemRemoved,
					beforeItemRemoved);
			}

			return value;
		}

		/// <summary>
		/// Inserts a cache entry into the cache by using a key, a value and an expiration type.
		/// </summary>
		/// <typeparam name="T">The type of the cache entry value.</typeparam>
		/// <param name="key">A unique identifier for the cache entry to insert.</param>
		/// <param name="value">The data for the cache entry.</param>
		/// <param name="group"></param>
		/// <param name="absoluteExpiration">The absolute expiration time, after which the cache entry will expire.</param>
		/// <param name="slidingExpiration">The sliding expiration time, after which the cache entry will expire.</param>
		/// <param name="afterItemRemoved">The action to perform after the cache entry has been removed.</param>
		/// <param name="beforeItemRemoved">The action to perform before the cache entry has been removed.</param>
		private void AddOrUpdate<T>(
			string key,
			T value,
			string group,
			DateTimeOffset? absoluteExpiration,
			TimeSpan? slidingExpiration,
			Action<string, T> afterItemRemoved,
			Action<string, T> beforeItemRemoved) where T : class
		{
			CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();

			if (absoluteExpiration.HasValue)
			{
				cacheItemPolicy.AbsoluteExpiration = absoluteExpiration.Value;
			}

			if (slidingExpiration.HasValue)
			{
				cacheItemPolicy.SlidingExpiration = slidingExpiration.Value;
			}

			if (afterItemRemoved != null)
			{
				cacheItemPolicy.RemovedCallback = x => afterItemRemoved(
					x.CacheItem.Key,
					(T)x.CacheItem.Value);
			}

			if (beforeItemRemoved != null)
			{
				cacheItemPolicy.UpdateCallback = x => beforeItemRemoved(
					x.UpdatedCacheItem.Key,
					(T)x.UpdatedCacheItem.Value);
			}

			this.memoryCache.Set(group + key, value, cacheItemPolicy);
		}

		#endregion
	}
}