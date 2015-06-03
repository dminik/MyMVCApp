﻿namespace xxx.Controllers
{
	using System.Collections.Generic;
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
		public IEnumerable<BookEntity> Get()
		{
			var bookList = this.BookService.GetAll();
			return bookList;
		}

		// GET api/book/5
		public BookEntity Get(int id)
		{
			var book = this.BookService.GetById(id);
			return book;
		}

		// POST api/book
		public void Post(BookEntity item)
		{
			this.BookService.Update(item);			
		}

		// PUT api/book/5
		public void Put(BookEntity item)
		{
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