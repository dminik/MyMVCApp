namespace WebSite.Controllers
{
	using System.Collections.Generic;
	using System.Configuration;
	using System.IO;
	using System.Linq;
	using System.Net.Http;
	using System.Web.Mvc;

	using Common;

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
			var promoCode = UserIdentity.PromoCode;

			var order = OrderService.GetByPromoCode(promoCode);

			if (order == null)
				return this.RedirectToAction("LogOff", "PromoAccount"); 

			decimal maxTotalSum = decimal.Parse(ConfigurationManager.AppSettings["promo:MaxTotalSum"]);
			var bookList = this.BookService.GetAll().ToList();
			
			var ownOrderDetails = OrderService.GetOrderDetailListByPromoCode(promoCode).ToList();
			string rootForImages = "http://localhost:82/Images/Books"; // todo перенести в web.config

			var modelBookList = new List<BookDto>();
			foreach (var bookEntity in bookList)
			{
				var dtoBook = new BookDto(bookEntity);
				dtoBook.RestAmount = OrderService.GetRestAmount(dtoBook.Id);
				dtoBook.IsOrdered = ownOrderDetails.Any(x => x.BookId == dtoBook.Id);
				if (!string.IsNullOrEmpty(dtoBook.FilePath))
					dtoBook.FilePath = Path.Combine(rootForImages, dtoBook.FilePath);
				modelBookList.Add(dtoBook);
			}
			
			var totalSum = OrderService.GetOrderTotalSumByPromoCode(promoCode);
			
			var orderViewModel = new OrderViewModel()
			{
				BookList = modelBookList,
				TotalSum = totalSum,
				MaxTotalSum = maxTotalSum,
				OrderStatus = order.Status,				
			};

			return this.View(orderViewModel);
		}		
	}
}