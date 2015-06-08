namespace DataLayer.Context
{
	using System.Collections.Generic;

	using DataLayer.Model.Entities;

	public static class TestDataProvider
	{
		public static List<Order> GetOrders()
		{
			var books = new List<Order>
			{
				//new Order
				//{
				//	PromoCode = "q1",
				//	Status = OrderStatus.BuildingByUser,
				//	OrderDetails = new List<OrderDetail> { new OrderDetail { BookId = 1 } }
				//},
				//new Order
				//{
				//	PromoCode = "q2",
				//	Status = OrderStatus.BuildingByUser,
				//	OrderDetails = new List<OrderDetail> { new OrderDetail { BookId = 1 } }
				//},
			};

			return books;
		}

		public static List<Book> GetBooks()
		{
			var books = new List<Book>
			{
				new Book { Name = "Буратино", Amount = 3, Price = 1000, Id = 1, FilePath = "b15144e5-8b7d-4e2e-8476-0aa333f9d76b_book1.png", },
				new Book { Name = "Война и мир", Amount = 1, Price = 500, Id = 2, FilePath = "b15144e5-8b7d-4e2e-8476-0aa333f9d76b_book2.png", },
				new Book { Name = "Я Бетмен", Amount = 2, Price = 700, Id = 3, FilePath = "b15144e5-8b7d-4e2e-8476-0aa333f9d76b_book3.png", },
			};

			return books;
		}
	}
}