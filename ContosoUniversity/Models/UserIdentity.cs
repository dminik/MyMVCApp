namespace ContosoUniversity.Models
{
	using System.Web.WebPages;

	public class UserIdentity
	{		
		public string PromoCode { get; set; }

		public bool IsAuthenticated { get { return !this.PromoCode.IsEmpty(); }}
	}
}