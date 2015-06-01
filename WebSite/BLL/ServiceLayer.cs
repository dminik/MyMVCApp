namespace WebSite.BLL
{
	using System;

	static public class ServiceLayer
	{
		static public string GetNewPromoCode()
		{
			return Guid.NewGuid().ToString();
		}

	}
}