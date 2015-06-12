using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	using System.Web.Caching;

	using ServiceLayer.Cache;

	class RedisCacheDependency : CacheDependency
	{
		public RedisCacheDependency(string key)
			: base()
		{
			CacheServiceRedis.Client.GetSubscriber().Subscribe("__keyspace@0__:" + key, (c, v) =>
			{
				this.NotifyDependencyChanged(new object(), EventArgs.Empty);
			});
		}
	}
}
