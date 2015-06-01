namespace DataLayer.Repository.Repositories.Base
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	using DataLayer.Model.Entities;

	public interface IGenericRepository<T, TKeyType> : IDisposable
		where T : Entity<TKeyType>
	{
		IEnumerable<T> GetAll();

		IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

		T GetByKey(TKeyType key);

		T Add(T entity);

		T Delete(T entity);

		void Delete(TKeyType id);

		void Edit(T entity);

		void Save();
	}
}