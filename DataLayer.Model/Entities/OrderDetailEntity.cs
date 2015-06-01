namespace DataLayer.Model.Entities
{
	public class OrderDetailEntity : Entity<int>
	{
		public int OrderId { get; set; }

		public int BookId { get; set; }

		public virtual OrderEntity Order { get; set; }
	}
}