namespace DataLayer.Context.UnitTests.Data.Context
{
	using System.Linq;

	public class OrderContext : EmptyContext
	{		
		public OrderContext() : base()
		{
			var orders = TestDataProvider.GetOrders().ToList();
			orders.ForEach(s => Context.Orders.Add(s));		
		}
	}
}