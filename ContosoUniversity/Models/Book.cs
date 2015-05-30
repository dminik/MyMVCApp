using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
   public class Book
   {
	  [Key]      
	  public int Id  { get; set; }
	 
	  public string Name { get; set; }
	  	
	  public decimal Price { get; set; }
	 
	  public int Amount { get; set; }
   }
}