namespace WebSite.BLL
{
	using System;

	public static class ServiceLayer
	{
		public static string GetNewPromoCode()
		{
			return Guid.NewGuid().ToString();
		}
	}
}