namespace DataLayer.Context.Migrations
{
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;

	using DataLayer.Model.Entities;

	public sealed class MainContextSeedInitializer : DropCreateDatabaseIfModelChanges<MainContext>
	{
		private void SeedBooks(MainContext context)
		{
			var books = TestDataProvider.GetBooks();
			books.ForEach(s => context.Books.AddOrUpdate(p => p.Id, s));
			context.SaveChanges();
		}

		private void SeedOrders(MainContext context)
		{
			var orders = TestDataProvider.GetOrders();
			orders.ForEach(s => context.Orders.AddOrUpdate(p => p.Id, s));
			context.SaveChanges();
		}

		protected override void Seed(MainContext context)
		{
			this.SeedBooks(context);
			this.SeedOrders(context);
		}
	}
}