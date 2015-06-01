namespace DataLayer.Repository.Repositories.Base
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	using DataLayer.Model.Entities;

	public interface IGenericRepository<T> : IDisposable
		where T : BaseEntity
	{
		IEnumerable<T> GetAll();

		IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

		T Add(T entity);

		T Delete(T entity);

		void Edit(T entity);

		void Save();
	}
}