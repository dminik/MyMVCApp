namespace WebSite.Controllers
{
	using System.Collections.Generic;
	using System.Configuration;
	using System.Linq;
	using System.Web.Mvc;

	using DataLayer.Model.Entities;

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
			var order = OrderService.GetByPromoCode(promoCode);
			
			var orderViewModel = new OrderViewModel()
			{
				BookList = modelBookList,
				TotalSum = totalSum,
				MaxTotalSum = maxTotalSum,
				OrderStatus = order.Status,
			};

			return this.View(orderViewModel);
		}

		//public ActionResult CommitOrder()
		//{
		//	var promoCode = UserIdentity.PromoCode;
		//	OrderService.ChangeStatus(promoCode, OrderStatus.BuiltByUser);	
			
		//	return new EmptyResult();
		//}

		//public ActionResult ReopenOrder()
		//{
		//	var promoCode = UserIdentity.PromoCode;
		//	OrderService.ChangeStatus(promoCode, OrderStatus.BuildingByUser);

		//	return new EmptyResult();
		//}	
	}
}