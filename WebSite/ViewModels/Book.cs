namespace WebSite.ViewModels
{
	public class Book : DataLayer.Model.Entities.Book
	{
		public Book(DataLayer.Model.Entities.Book bookEntity)
		{
			Id = bookEntity.Id;
			Name = bookEntity.Name;
			Amount = bookEntity.Amount;
			Price = bookEntity.Price;			
		}
		public int AmountOrdered { get; set; }
	}
}