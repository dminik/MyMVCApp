namespace DataLayer.Repository.Repositories
{
	using System;
	using System.Linq;

	using DataLayer.Context.Interfaces;
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public class BookRepository : GenericRepository<BookEntity>, IBookRepository
	{
		public BookRepository(IMainContext context)
			: base(context)
		{
		}

		public BookEntity GetBookByName(string name)
		{
			return this.Context.Books.SingleOrDefault(a => a.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		}
	}
}