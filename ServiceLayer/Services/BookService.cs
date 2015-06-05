namespace ServiceLayer.Services
{
	using DataLayer.Model.Entities;
	using DataLayer.Repository;
	using DataLayer.Repository.Repositories;

	using ServiceLayer.Cache;
	using ServiceLayer.Common;

	public class BookService : CachedEntityService<Book, int>, IBookService
	{
		private readonly IBookRepository bookRepository;

		private IDataRepositories dataRepositories;

		public BookService(ICacheService cacheService, IDataRepositories dataRepositories)
			: base(cacheService, dataRepositories.Books, dataRepositories)
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