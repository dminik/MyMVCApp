namespace DataLayer.Repository.Repositories
{
	using System;
	using System.Linq;

	using DataLayer.Context.Interfaces;
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public class OrderDetailRepository : GenericRepository<OrderDetailEntity, int>, IOrderDetailRepository
	{
		public OrderDetailRepository(IMainContext context)
			: base(context)
		{
		}

		public OrderDetailEntity GetByBookId(string promoCode, int bookId)
		{
			return FindBy(x => 
					x.Order.PromoCode == promoCode && 
					x.BookId == bookId)
				.SingleOrDefault();
		}

		public void Delete(int orderId, int bookId)
		{
			var orderDetails = FindBy(y => y.BookId == bookId && y.OrderId == orderId).FirstOrDefault();
			Delete(orderDetails);
		}
		public OrderDetailEntity Add(int orderId, int bookId)
		{
			var orderDetail = new OrderDetailEntity { OrderId = orderId, BookId = bookId, };
			Add(orderDetail);
			return orderDetail;
		}
	}
}