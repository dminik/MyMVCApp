namespace WebSite.ViewModels
{
	using System.Collections.Generic;

	public class OrderViewModel
	{		 
		public IEnumerable<BookDto> BookList { get; set; }

		public decimal TotalSum { get; set; }
		public decimal MaxTotalSum { get; set; }
	}
}