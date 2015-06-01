namespace ServiceLayer
{
	using System.Linq;

	using DataLayer.Model.Entities;
	using DataLayer.Repository;
	using DataLayer.Repository.Repositories;

	using ServiceLayer.Common;

	public class BookService : EntityService<BookEntity>, IBookService
	{
		private readonly BookRepository _bookRepository;

		private IDataRepositories _dataRepositories;

		public BookService(IDataRepositories dataRepositories)
			: base(dataRepositories.Books, dataRepositories)
		{
			this._bookRepository = dataRepositories.Books;
			this._dataRepositories = dataRepositories;
		}

		public BookEntity GetById(int id)
		{
			return this._bookRepository.FindBy(x => x.Id == id).FirstOrDefault();
		}
	}
}