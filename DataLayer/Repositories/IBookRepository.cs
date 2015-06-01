namespace DataLayer.Repository.Repositories
{
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public interface IBookRepository : IGenericRepository<BookEntity, int>
	{
	}
}