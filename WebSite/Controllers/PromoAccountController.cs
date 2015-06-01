namespace WebSite.Controllers
{
	using System;
	using System.Web.Mvc;

	using WebMatrix.WebData;

	using WebSite.Filters;
	using WebSite.Models;

	[Authorize]
	[InitializeSimpleMembership]
	public class PromoAccountController : Controller
	{
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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			WebSecurity.Logout();

			return this.RedirectToAction("Login", "PromoAccount");
		}

		[AllowAnonymous]
		public ActionResult CreatePromoCodeAndEnter()
		{
			var newPromoCode = Guid.NewGuid().ToString();

			WebSecurity.CreateUserAndAccount(newPromoCode, newPromoCode);
			return this.Login(new PromoLoginModel { PromoCode = newPromoCode });
		}
	}
}