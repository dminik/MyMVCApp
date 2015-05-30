using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
	using System.Collections.Generic;

	public class Order
	{
		[Key]
		public int Id { get; set; }

		public string PromoCode { get; set; }

		public OrderStatus Status { get; set; }		

		public virtual ICollection<OrderDetail> OrderDetails { get; set; }		
	}

	public enum OrderStatus
	{
		BuildingByUser,
		BuiltByUser,
	}
}