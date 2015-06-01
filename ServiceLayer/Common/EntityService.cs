namespace ServiceLayer.Common
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public abstract class EntityService<T, TKeyType> : IEntityService<T, TKeyType>
		where T : Entity<TKeyType>
	{
		protected readonly IGenericRepository<T, TKeyType> Repository;

		protected readonly IUnitOfWork UnitOfWork;

		protected EntityService(IGenericRepository<T, TKeyType> repository, IUnitOfWork unitOfWork)
		{
			this.UnitOfWork = unitOfWork;
			this.Repository = repository;
		}

		public virtual void Create(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			this.Repository.Add(entity);
			this.UnitOfWork.Save();
		}

		public virtual void Update(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			this.Repository.Edit(entity);
			this.UnitOfWork.Save();
		}
		
		public virtual void Delete(TKeyType id)
		{			
			this.Repository.Delete(id);
			this.UnitOfWork.Save();
		}

		public virtual IEnumerable<T> GetAll()
		{
			return this.Repository.GetAll();
		}

		public virtual T GetById(TKeyType id)
		{
			return this.Repository.GetByKey(id);
		}

		public void Dispose()
		{
			this.UnitOfWork.Dispose();
		}
	}
}