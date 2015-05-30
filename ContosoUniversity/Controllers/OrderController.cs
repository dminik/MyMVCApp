using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.Models;
using ContosoUniversity.DAL;

namespace ContosoUniversity.Controllers
{
	using ContosoUniversity.BLL;

	[Authorize]
	public class OrderController : BaseController
	{		
		public ActionResult Index()
		{
			IEnumerable<Book> modelBookList = unitOfWork.BookRepository.Get().ToList();				
						
			return View(modelBookList);
		}
		
		[HttpPost]
		public ActionResult AddToOrder(string promoCode, int bookId)
		{			
			var order = unitOfWork.OrderRepository.Get(x => x.PromoCode == promoCode).FirstOrDefault();

			if (order == null)
				throw new Exception("Неправильный промокод " + promoCode);

			var orderDetail = new OrderDetail();

			order.OrderDetails.Add(orderDetail);

			unitOfWork.Save();

			return Json(true);
		}

		[HttpPost]
		public ActionResult DeleteFromOrder(string promoCode, int bookId)
		{
			unitOfWork.OrderDetailRepository.RemoveOrderDetail(promoCode, bookId);
			unitOfWork.Save();

			return Json(true);
		}
	}
}
