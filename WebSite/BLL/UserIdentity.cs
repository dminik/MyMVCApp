using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.BLL
{
	public class UserIdentity : IUserIdentity
	{
		public string PromoCode
		{
			get
			{
				var promoCode = "";

				if (HttpContext.Current != null)
					if (HttpContext.Current.User != null)
						if (HttpContext.Current.User.Identity != null)
							return HttpContext.Current.User.Identity.Name;

				return promoCode;
			}

			internal set { }
		}
	}
}