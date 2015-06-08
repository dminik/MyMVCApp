namespace WebSite.Controllers
{
	using System;
	using System.Web.Mvc;

	using ServiceLayer.Services;

	using WebMatrix.WebData;

	using WebSite.Filters;
	using WebSite.Models;

	[Authorize]
	[InitializeSimpleMembership]
	public class PromoAccountController : Controller
	{
		private IOrderService OrderService { get; set; }

		public PromoAccountController(IOrderService orderService)
		{
			this.OrderService = orderService;
		}

		protected override void Dispose(bool disposing)
		{
			this.OrderService.Dispose();
			base.Dispose(disposing);
		}

		[AllowAnonymous]
		public ActionResult Login()
		{
			return this.View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(PromoLoginModel model)
		{
			if (this.ModelState.IsValid && WebSecurity.Login(model.PromoCode, model.PromoCode, true))
			{
				return this.RedirectToAction("Index", "Order");
			}
			this.ModelState.AddModelError("", "Неправильный промокод.");
			return this.View(model);
		}

		
		public ActionResult LogOff()
		{
			WebSecurity.Logout();

			return this.RedirectToAction("Login", "PromoAccount");
		}

		[AllowAnonymous]
		public ActionResult CreatePromoCodeAndEnter()
		{
			var newOrder = OrderService.CreateOrder();
			var newPromoCode = newOrder.PromoCode;
			
			WebSecurity.CreateUserAndAccount(newPromoCode, newPromoCode);

			return this.Login(new PromoLoginModel { PromoCode = newPromoCode });
		}
		
	}
}