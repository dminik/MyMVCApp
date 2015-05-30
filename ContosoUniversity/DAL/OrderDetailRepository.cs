using System;
using ContosoUniversity.Models;

namespace ContosoUniversity.DAL
{
	using System.Linq;

	public class OrderDetailRepository : GenericRepository<OrderDetail>
	{
		public OrderDetailRepository(SchoolContext context)
			: base(context)
		{
		}

		public void AddOrderDetail(OrderDetail orderDetail)
		{
			context.OrderDetails.Add(orderDetail);
		}

		public void RemoveOrderDetail(string promoCode, int bookId)
		{
			var ordrDetails = context.OrderDetails.FirstOrDefault(x => x.BookId == bookId && x.Order.PromoCode == promoCode);

			if (ordrDetails != null)			
				context.OrderDetails.Remove(ordrDetails);
		}
	}
}
