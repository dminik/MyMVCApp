namespace DataLayer.Context
{
	using System.Collections.Generic;

	using DataLayer.Model.Entities;

	public static class TestDataProvider
	{
		public static List<OrderEntity> GetOrders()
		{
			var books = new List<OrderEntity>
			{
				new OrderEntity
				{
					PromoCode = "q1",
					Status = OrderStatus.BuildingByUser,
					OrderDetails = new List<OrderDetailEntity> { new OrderDetailEntity { BookId = 0 } }
				},
				new OrderEntity { PromoCode = "q2", Status = OrderStatus.BuildingByUser }
			};

			return books;
		}

		public static List<BookEntity> GetBooks()
		{
			var books = new List<BookEntity>
			{
				new BookEntity { Name = "Буратино", Amount = 5, Price = 1000, Id = 0 },
				new BookEntity { Name = "Война и мир", Amount = 2, Price = 500, Id = 1 }
			};

			return books;
		}
	}
}