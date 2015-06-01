namespace DataLayer.Model.Entities
{
	public class BookEntity : Entity<int>
	{
		public string Name { get; set; }

		public decimal Price { get; set; }

		public int Amount { get; set; }
	}
}