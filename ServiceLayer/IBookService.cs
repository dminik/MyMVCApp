namespace ServiceLayer
{
	using DataLayer.Model.Entities;

	using ServiceLayer.Common;

	public interface IBookService : IEntityService<BookEntity>
	{
		BookEntity GetById(int id);
	}
}