namespace ServiceLayer.Common
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	using ServiceLayer.Cache;

	public abstract class CachedEntityService<T, TKeyType> : EntityService<T, TKeyType>
		where T : Entity<TKeyType>
	{
		protected readonly ICacheService CacheService;

		protected readonly string KeyPrefix = typeof(T).Name;

		protected CachedEntityService(ICacheService cacheService, IGenericRepository<T, TKeyType> repository, IUnitOfWork unitOfWork)
			: base(repository, unitOfWork)
		{			
			this.CacheService = cacheService;
		}
		
		public override void Create(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			
			base.Create(entity);
			CacheService.AddOrUpdate(entity.Id.ToString(), entity, KeyPrefix);
		}

		public override void Update(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			
			base.Update(entity);
			CacheService.AddOrUpdate(entity.Id.ToString(), entity, KeyPrefix);
		}

		public override void Delete(TKeyType id)
		{
			base.Delete(id);

			CacheService.Remove(id.ToString(), KeyPrefix);
			base.Delete(id);
		}

		public override IEnumerable<T> GetAll()
		{			
			var isGetAllOccuredCacheKey = KeyPrefix + "isGetAllOccuredCacheKey";
			var isCacheContainAll = CacheService.Get<object>(isGetAllOccuredCacheKey, KeyPrefix) != null;

			if (!isCacheContainAll)
			{
				var all = this.Repository.GetAll();
				foreach (var item in all)
				{
					CacheService.AddOrUpdate(item.Id.ToString(), item, KeyPrefix);
				}

				CacheService.AddOrUpdate(isGetAllOccuredCacheKey, new object(), KeyPrefix);
				return all.ToList();
			}
			else
			{
				return CacheService.GetByGroupKey<T>(KeyPrefix);
			}
		}

		public override T GetById(TKeyType id)
		{
			return this.Repository.GetByKey(id);
		}		
	}
}