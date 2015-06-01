namespace ServiceLayer
{
	using System.Linq;

	using DataLayer.Model.Entities;
	using DataLayer.Repository;
	using DataLayer.Repository.Repositories;

	using ServiceLayer.Common;

	public class BookService : EntityService<BookEntity, int>, IBookService
	{
		private readonly IBookRepository bookRepository;

		private IDataRepositories dataRepositories;

		public BookService(IDataRepositories dataRepositories)
			: base(dataRepositories.Books, dataRepositories)
		{
			this.bookRepository = dataRepositories.Books;
			this.dataRepositories = dataRepositories;
		}

		//public BookEntity GetById(int id)
		//{
		//	return this._bookRepository.FindBy(x => x.Id == id).FirstOrDefault();
		//}
	}
}