namespace DataLayer.Repository.Repositories
{
	using System;
	using System.Linq;

	using DataLayer.Context.Interfaces;
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public class BookRepository : GenericRepository<Book, int>, IBookRepository
	{
		public BookRepository(IMainContext context)
			: base(context)
		{
		}

		public Book GetBookByName(string name)
		{
			return this.Context.Books.SingleOrDefault(a => a.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		}
	}
}