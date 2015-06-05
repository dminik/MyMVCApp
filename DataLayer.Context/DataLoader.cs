namespace DataLayer.Context
{
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;

	using DataLayer.Model.Entities;

	internal sealed class SchoolContextSeedInitializer : DropCreateDatabaseAlways<MainContext>
	{
		private void SeedBooks(MainContext context)
		{
			var books = TestDataProvider.GetBooks();

			books.ForEach(s => context.Books.AddOrUpdate(p => p.Id, s));

			context.SaveChanges();
		}

		private void SeedOrders(MainContext context)
		{
			var books = new List<Order>
			{
				new Order
				{
					PromoCode = "q1",
					Status = OrderStatus.BuildingByUser,
					OrderDetails = new List<OrderDetail> { new OrderDetail { BookId = 0 } }
				},
				new Order { PromoCode = "q2", Status = OrderStatus.BuildingByUser }
			};

			books.ForEach(s => context.Orders.AddOrUpdate(p => p.Id, s));

			context.SaveChanges();
		}

		protected override void Seed(MainContext context)
		{
			this.SeedBooks(context);
			this.SeedOrders(context);

			context.SaveChanges();
		}
	}
}