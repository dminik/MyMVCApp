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

		public async Task<HttpResponseMessage> Post()
		{
			var item = new Book();


			// Check if the request contains multipart/form-data.
			if (!Request.Content.IsMimeMultipartContent())
			{
				throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
			}

			string root = HttpContext.Current.Server.MapPath("~/Images/Books");
			var provider = new MultipartFormDataStreamProvider(root);

			try
			{								
				await Request.Content.ReadAsMultipartAsync(provider);

				item.Name = provider.FormData["Name"];
				item.Price = decimal.Parse(provider.FormData["Price"]);
				item.Amount = int.Parse(provider.FormData["Amount"]);

				if (provider.FileData.Any())
				{
					var fileData = provider.FileData.First();
					var finfo = new FileInfo(fileData.LocalFileName);

					string guid = Guid.NewGuid().ToString();

					item.FilePath = guid + "_" + fileData.Headers.ContentDisposition.FileName.Replace("\"", "");
					var absolutFilePath = Path.Combine(root, item.FilePath);
					File.Move(finfo.FullName, absolutFilePath);
				}

				this.BookService.Create(item);

				return new HttpResponseMessage()
				{
					Content = new StringContent("Сохранен файл обложки: " + item.FilePath)
				};
			}
			catch (System.Exception e)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
			}
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