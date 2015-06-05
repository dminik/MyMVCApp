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
				new Order
				{
					PromoCode = "q1",
					Status = OrderStatus.BuildingByUser,
					OrderDetails = new List<OrderDetail> { new OrderDetail { BookId = 1 } }
				},
				new Order
				{
					PromoCode = "q2",
					Status = OrderStatus.BuildingByUser,
					OrderDetails = new List<OrderDetail> { new OrderDetail { BookId = 1 } }
				},
			};

			return books;
		}

		public static List<Book> GetBooks()
		{
			var books = new List<Book>
			{
				new Book { Name = "Буратино", Amount = 5, Price = 1000, Id = 1 },
				new Book { Name = "Война и мир", Amount = 2, Price = 500, Id = 2 }
			};

			return books;
		}
	}
}