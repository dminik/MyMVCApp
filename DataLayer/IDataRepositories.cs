namespace DataLayer.Repository
{
	using DataLayer.Repository.Repositories;
	using DataLayer.Repository.Repositories.Base;

	public interface IDataRepositories : IUnitOfWork
	{
		BookRepository Books { get; }

		OrderRepository Orders { get; }

		OrderDetailRepository OrderDetails { get; }
	}
}