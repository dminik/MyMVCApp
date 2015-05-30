using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.BLL
{
	static public class ServiceLayer
	{
		static public string GetNewPromoCode()
		{
			return Guid.NewGuid().ToString();
		}

	}
}