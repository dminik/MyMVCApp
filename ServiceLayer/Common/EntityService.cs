namespace ServiceLayer.Common
{
	using System;
	using System.Collections.Generic;

	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public abstract class EntityService<T> : IEntityService<T>
		where T : BaseEntity
	{
		protected readonly IGenericRepository<T> Repository;

		protected readonly IUnitOfWork UnitOfWork;

		protected EntityService(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
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

		public virtual void Delete(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			this.Repository.Delete(entity);
			this.UnitOfWork.Save();
		}

		public virtual IEnumerable<T> GetAll()
		{
			return this.Repository.GetAll();
		}

		public void Dispose()
		{
			this.UnitOfWork.Dispose();
		}
	}
}