namespace WebSite.ViewModels
{
	public class BookDto : DataLayer.Model.Entities.Book
	{
		public BookDto(DataLayer.Model.Entities.Book bookEntity)
		{
			Id = bookEntity.Id;
			Name = bookEntity.Name;
			Amount = bookEntity.Amount;
			Price = bookEntity.Price;			
		}
		public int RestAmount { get; set; }

		public bool IsOrdered { get; set; }
	}
}