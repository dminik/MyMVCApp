namespace DataLayer.Model.Entities
{
	using System.Collections.Generic;
	using System.Linq;

	public class Book : Entity<int>
	{		
		public string Name { get; set; }

		public decimal Price { get; set; }

		public int Amount { get; set; }

		public int FilePath { get; set; }
	}
}