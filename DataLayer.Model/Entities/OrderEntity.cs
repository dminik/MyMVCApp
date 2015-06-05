namespace DataLayer.Model.Entities
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class OrderEntity : Entity<int>
	{
		[Index(IsUnique = true)]
		[MaxLength(255)]
		[Required]
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