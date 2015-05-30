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
	abstract public class BaseController : Controller
	{
		protected readonly UnitOfWork unitOfWork = new UnitOfWork();

		private UserIdentity userIdentity;
		protected UserIdentity UserIdentity
		{
			get
			{
				if (this.userIdentity == null)
				{
					this.userIdentity = new UserIdentity();

					var userInCookie = Request.Cookies["user"];

					if (userInCookie != null)
					{
						this.userIdentity.PromoCode = userInCookie.Value;
					}
				}

				return this.userIdentity;
			}
		}

		
		protected override void Dispose(bool disposing)
		{
			unitOfWork.Dispose();
			base.Dispose(disposing);
		}
	}
}
