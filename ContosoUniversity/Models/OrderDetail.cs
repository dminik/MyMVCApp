using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
	public class OrderDetail
	{
		[Key]
		public int Id { get; set; }

		public int OrderId { get; set; }

		public int BookId { get; set; }

		public virtual Order Order { get; set; }
	}
}