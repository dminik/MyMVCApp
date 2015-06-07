namespace WebSite.Controllers
{
	using System.Collections.Generic;
	using System.Configuration;
	using System.Linq;
	using System.Web.Mvc;

	using ServiceLayer;
	using ServiceLayer.Services;

	using WebSite.BLL;
	using WebSite.ViewModels;

	[Authorize]
	public class OrderController : BaseController
	{
		protected IBookService BookService;
		protected IOrderService OrderService;
		protected IUserIdentity UserIdentity;

		public OrderController(IBookService bookService, IOrderService orderService, IUserIdentity user)
		{
			this.BookService = bookService;
			this.OrderService = orderService;
			this.UserIdentity = user;
		}

		protected override void Dispose(bool disposing)
		{
			this.OrderService.Dispose();
			this.BookService.Dispose();
			base.Dispose(disposing);
		}

		public ActionResult Index()
		{
			decimal maxTotalSum = decimal.Parse(ConfigurationManager.AppSettings["promo:MaxTotalSum"]);
			var bookList = this.BookService.GetAll().ToList();

			var promoCode = UserIdentity.PromoCode;
			var ownOrderDetails = OrderService.GetOrderDetailListByPromoCode(promoCode).ToList();

			var modelBookList = new List<BookDto>();
			foreach (var bookEntity in bookList)
			{
				var dtoBook = new BookDto(bookEntity);
				dtoBook.RestAmount = OrderService.GetRestAmount(dtoBook.Id);
				dtoBook.IsOrdered = ownOrderDetails.Any(x => x.BookId == dtoBook.Id);
				modelBookList.Add(dtoBook);
			}
			
			var totalSum = OrderService.GetOrderTotalSumByPromoCode(promoCode);
			
			var orderViewModel = new OrderViewModel()
			{
				BookList = modelBookList,
				TotalSum = totalSum,
				MaxTotalSum = maxTotalSum,
			};

			return this.View(orderViewModel);
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