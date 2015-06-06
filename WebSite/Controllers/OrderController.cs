namespace WebSite.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;

	using ServiceLayer;
	using ServiceLayer.Services;

	using WebSite.ViewModels;

	[Authorize]
	public class OrderController : BaseController
	{
		protected IBookService BookService;
		protected IOrderService OrderService;

		public OrderController(IBookService bookService, IOrderService orderService)
		{
			this.BookService = bookService;
			this.OrderService = orderService;
		}

		protected override void Dispose(bool disposing)
		{
			this.OrderService.Dispose();
			this.BookService.Dispose();
			base.Dispose(disposing);
		}

		public ActionResult Index()
		{
			var bookList = this.BookService.GetAll().ToList();

			var modelBookList = new List<Book>();
			foreach (var bookEntity in bookList)
			{
				var bookDTO = new Book(bookEntity);
				bookDTO.RestAmount = OrderService.GetRestAmount(bookDTO.Id);

				modelBookList.Add(bookDTO);
			}

			return this.View(modelBookList);
		}

		[HttpPost]
		public ActionResult AddToOrder(string promoCode, int bookId)
		{
			//var order = unitOfWork.OrderRepository.Get(x => x.PromoCode == promoCode).FirstOrDefault();

			//if (order == null)
			//	throw new Exception("Неправильный промокод " + promoCode);

			//var orderDetail = new OrderDetail();

			//order.OrderDetails.Add(orderDetail);

			//unitOfWork.Save();

			return this.Json(true);
		}

		[HttpPost]
		public ActionResult DeleteFromOrder(string promoCode, int bookId)
		{
			//unitOfWork.OrderDetailRepository.RemoveOrderDetail(promoCode, bookId);
			//unitOfWork.Save();

			return this.Json(true);
		}
	}
}