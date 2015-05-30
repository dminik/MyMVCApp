using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using ContosoUniversity.Filters;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
	[Authorize]
	[InitializeSimpleMembership]
	public class PromoAccountController : Controller
	{
		[AllowAnonymous]
		public ActionResult Login()
		{			
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(PromoLoginModel model)
		{
			if (ModelState.IsValid && WebSecurity.Login(model.PromoCode, model.PromoCode, persistCookie: true))
			{
				return RedirectToAction("Index", "Order");				
			}
			else
			{
				ModelState.AddModelError("", "Неправильный промокод.");
				return View(model);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			WebSecurity.Logout();

			return RedirectToAction("Login", "PromoAccount");
		}

		[AllowAnonymous]
		public ActionResult CreatePromoCodeAndEnter()
		{
			var newPromoCode = Guid.NewGuid().ToString();

			WebSecurity.CreateUserAndAccount(newPromoCode, newPromoCode);
			return Login(new PromoLoginModel() { PromoCode = newPromoCode, });					
		}
	}
}
