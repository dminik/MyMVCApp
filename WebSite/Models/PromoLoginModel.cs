namespace WebSite.Models
{
	using System.ComponentModel.DataAnnotations;

	public class PromoLoginModel
	{
		[Required]
		[Display(Name = "Промокод")]
		public string PromoCode { get; set; }
	}
}