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

		protected readonly string ObjectKeyPrefix = typeof(T).Name + "_";
		protected readonly string FlagKeyPrefix = string.Format("flag_{0}_", typeof(T).Name);

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
			CacheService.AddOrUpdate(entity.Id.ToString(), entity, this.ObjectKeyPrefix);
		}

		public override void Update(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			
			base.Update(entity);
			CacheService.AddOrUpdate(entity.Id.ToString(), entity, this.ObjectKeyPrefix);
		}

		public override void Delete(TKeyType id)
		{			
			CacheService.Remove(id.ToString(), this.ObjectKeyPrefix);
			base.Delete(id);
		}

		public override IEnumerable<T> GetAll()
		{			
			var isGetAllOccuredCacheKey = "IsGetAllOccuredCacheKey";
			var isCacheContainAll = CacheService.Get<object>(isGetAllOccuredCacheKey, FlagKeyPrefix) != null;

			if (!isCacheContainAll)
			{
				var all = this.Repository.GetAll();
				foreach (var item in all)
				{
					CacheService.AddOrUpdate(item.Id.ToString(), item, this.ObjectKeyPrefix);
				}

				CacheService.AddOrUpdate(isGetAllOccuredCacheKey, new object(), FlagKeyPrefix);
				return all.ToList();
			}
			else
			{
				return CacheService.GetByGroupKey<T>(this.ObjectKeyPrefix);
			}
		}

		public override T GetById(TKeyType id)
		{
			var item = CacheService.Get<T>(id.ToString(), ObjectKeyPrefix);

			if (item == null)
			{
				item = this.Repository.GetByKey(id);
				CacheService.AddOrUpdate(id.ToString(), item, this.ObjectKeyPrefix);
			}

			return item;
		}		
	}
}