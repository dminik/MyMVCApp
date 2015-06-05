namespace DataLayer.Context.Interfaces
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;

	using DataLayer.Model.Entities;

	public interface IMainContext : IDisposable
	{
		IDbSet<Book> Books { get; set; }

		IDbSet<Order> Orders { get; set; }

		IDbSet<OrderDetail> OrderDetails { get; set; }

		int SaveChanges();

		IDbSet<TEntity> Set<TEntity>() where TEntity : class;

		DbEntityEntry Entry(object entity);

		void SetModified(object entity);
	}
}