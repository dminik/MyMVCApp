namespace WebSite.Controllers
{
	using System.Linq;
	using System.Web.Mvc;

	using ServiceLayer;

	[Authorize]
	public class OrderController : BaseController
	{
		public OrderController(IBookService bookService)
		{
			this.BookService = bookService;
		}

		public ActionResult Index()
		{
			var modelBookList = this.BookService.GetAll().ToList();				
						
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
