namespace DataLayer.Context.Migrations
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	using DataLayer.Model.Entities;

	public sealed class SchoolContextSeedInitializer : DropCreateDatabaseAlways<MainContext>
	{		
		void SeedBooks(MainContext context)
		{
			var books = new List<BookEntity>
			{
				new BookEntity { Name = "Буратино", Amount = 5, Price = 1000, Id = 0, },
				new BookEntity { Name = "Война и мир", Amount = 2, Price = 500, Id = 1, },				
			};


			books.ForEach(s => context.Books.AddOrUpdate(p => p.Id, s));

			context.SaveChanges();
		}

		void SeedOrders(MainContext context)
		{
			var books = new List<OrderEntity>
			{
				new OrderEntity { PromoCode = "q1", Status = OrderStatus.BuildingByUser, 
					OrderDetails = new List<OrderDetailEntity>(){new OrderDetailEntity(){BookId = 0, }}},
				new OrderEntity { PromoCode = "q2", Status = OrderStatus.BuildingByUser, },				
			};

			books.ForEach(s => context.Orders.AddOrUpdate(p => p.Id, s));

			context.SaveChanges();
		}

		protected override void Seed(MainContext context)
		{
			this.SeedBooks(context);
			this.SeedOrders(context);			
		}

	}
}
