namespace DataLayer.Model.Entities
{
	using System.ComponentModel.DataAnnotations.Schema;

	public class OrderDetail : Entity<int>
	{		
		public int OrderId { get; set; }
		
		public int BookId { get; set; }

		public virtual Book Book { get; set; }

		public virtual Order Order { get; set; }
	}
}