namespace WebSite.ViewModels
{
	using System.Collections.Generic;

	using DataLayer.Model.Entities;

	public class OrderViewModel
	{		 
		public IEnumerable<BookDto> BookList { get; set; }

		public decimal TotalSum { get; set; }
		public decimal MaxTotalSum { get; set; }
		public OrderStatus OrderStatus { get; set; }		
	}
}