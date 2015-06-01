namespace ServiceLayer.Common
{
	using System;
	using System.Collections.Generic;

	using DataLayer.Model.Entities;

	public interface IEntityService<T> : IService, IDisposable
		where T : BaseEntity
	{
		void Create(T entity);

		void Delete(T entity);

		IEnumerable<T> GetAll();

		void Update(T entity);
	}
}