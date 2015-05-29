using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
   public class Book
   {
      [Key]      
      public int Id  { get; set; }

      [StringLength(50)]
      [Display(Name = "Имя")]
      public string Name { get; set; }
	  
	  [Display(Name = "Цена")]
	  public decimal Price { get; set; }

	  [Display(Name = "Количество")]
	  public int Amount { get; set; }

   }
}