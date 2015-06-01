namespace DataLayer.Repository
{
	using DataLayer.Repository.Repositories;
	using DataLayer.Repository.Repositories.Base;

	public interface IDataRepositories : IUnitOfWork
	{
		IBookRepository Books { get; set; }

		OrderRepository Orders { get; }

		OrderDetailRepository OrderDetails { get; }
	}
}