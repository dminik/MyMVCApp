namespace WebAPI.Controllers
{
	using System.Collections.Generic;
	using System.IO;
	using System.Net.Http;
	using System.Web.Http;

	using DataLayer.Model.Entities;

	using ServiceLayer;

	public class BookController : ApiController
	{
		protected IBookService BookService;

		public BookController(IBookService bookService)
		{
			this.BookService = bookService;
		}

		// GET api/book
		public IEnumerable<Book> Get()
		{
			var bookList = this.BookService.GetAll();
			return bookList;
		}

		// GET api/book/5
		public Book Get(int id)
		{
			var book = this.BookService.GetById(id);
			return book;
		}

		// POST api/book
		public void Post(Book item)
		{
			this.BookService.Update(item);			
		}

		// PUT api/book/
		public void Put(Book item)
		{
			//var request = System.Web.HttpContext.Current.Request.F
			//if()
			//{
			//	if (!Infile(Request.Files[Save])) continue;
			//	string fileType = Request.Files[Save].ContentType;
			//	Stream file_Strm = Request.Files[Save].InputStream;
			//	string file_Name = Path.GetFileName(Request.Files[Save].FileName);
			//	int fileSize = Request.Files[Save].ContentLength;
			//	byte[] fileRcrd = new byte[fileSize];


			this.BookService.Create(item);
		}

		// DELETE api/book/5
		public void Delete(int id)
		{
			this.BookService.Delete(id);
		}

		protected override void Dispose(bool disposing)
		{
			this.BookService.Dispose();
			base.Dispose(disposing);
		}
	}
}