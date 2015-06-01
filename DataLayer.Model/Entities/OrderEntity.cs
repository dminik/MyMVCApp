namespace DataLayer.Model.Entities
{
	using System.Collections.Generic;

	public class OrderEntity : Entity<int>
	{
		public string PromoCode { get; set; }

		public OrderStatus Status { get; set; }

		public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }
	}

	public enum OrderStatus
	{
		BuildingByUser,

		BuiltByUser
	}
}