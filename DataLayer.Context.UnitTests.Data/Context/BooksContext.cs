namespace DataLayer.Context.UnitTests.Data.Context
{
	using System.Collections.Generic;
	using System.Linq;

	using DataLayer.Model.Entities;

	/// <summary>
	/// WarCraft game
	/// </summary>
	public class BooksContext : EmptyContext
	{
		public readonly List<Book> Books;

		public BooksContext()
		{
			this.Books = TestDataProvider.GetBooks().ToList();
			this.Books.ForEach(g => this.Context.Books.Add(g));
		}
	}
}