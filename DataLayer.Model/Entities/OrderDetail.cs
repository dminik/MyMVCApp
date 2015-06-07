namespace DataLayer.Model.Entities
{
	using System.ComponentModel.DataAnnotations.Schema;

	public class OrderDetail : Entity<int>
	{
		[Index("IX_Unique_OrderId_FirstAndSecond", 1, IsUnique = true)]
		public int OrderId { get; set; }

		[Index("IX_Unique_OrderId_FirstAndSecond", 2, IsUnique = true)]
		public int BookId { get; set; }

		public virtual Book Book { get; set; }

		public virtual Order Order { get; set; }
	}
}