namespace DataLayer.Repository.Repositories.Base
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;

	using DataLayer.Context.Interfaces;
	using DataLayer.Model.Entities;

	public abstract class GenericRepository<T, TKeyType> : IGenericRepository<T, TKeyType>
		where T : Entity<TKeyType>
	{
		protected readonly IDbSet<T> DbSet;

		protected IMainContext Context;

		protected GenericRepository(IMainContext context)
		{
			this.Context = context;
			this.DbSet = context.Set<T>();
		}

		public virtual IEnumerable<T> GetAll()
		{
			return this.DbSet.AsEnumerable();
		}

		protected IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
		{
			var query = this.DbSet.Where(predicate).AsEnumerable();
			return query;
		}

		public T GetByKey(TKeyType key)
		{
			return this.DbSet.Find(key);			
		}

		public virtual T Add(T entity)
		{
			return this.DbSet.Add(entity);
		}

		public virtual T Delete(T entity)
		{			
			return this.DbSet.Remove(entity);
		}

		public virtual void Delete(TKeyType id)
		{
			var entityToDelete = this.DbSet.Find(id);
			Delete(entityToDelete);
		}

		public virtual void Edit(T entity)
		{
			this.Context.Entry(entity).State = EntityState.Modified;
		}

		public virtual void Save()
		{
			this.Context.SaveChanges();
		}

		public void Dispose()
		{
			this.Context.Dispose();
		}
	}
}