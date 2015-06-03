namespace ServiceLayer.Common
{
	using System;
	using System.Collections.Generic;

	using DataLayer.Model.Entities;

	public interface IEntityService<T, TKeyType> : IService, IDisposable
		where T : Entity<TKeyType>
	{
		void Create(T entity);		

		void Delete(TKeyType id);

		IEnumerable<T> GetAll();

		void Update(T entity);

		T GetById(TKeyType id);
	}
}