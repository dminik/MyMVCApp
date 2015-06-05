namespace DataLayer.Repository.Repositories
{
	using System;
	using System.Linq;

	using DataLayer.Context.Interfaces;
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public class OrderRepository : GenericRepository<OrderEntity, int>, IOrderRepository
	{
		public OrderRepository(IMainContext context)
			: base(context)
		{
		}

		public OrderEntity GetByPromoCode(string promoCode)
		{
			return FindBy(x => x.PromoCode == promoCode).SingleOrDefault();
		}

		public OrderDetailEntity AddOrderDetail(string promoCode, int bookId)
		{
			var order = this.GetByPromoCode(promoCode);
			if (order == null)
				throw new Exception(string.Format("Ошибочный промокод {0}", promoCode));

			var orderDetail = new OrderDetailEntity { BookId = bookId, };
			order.OrderDetails.Add(orderDetail);
			return orderDetail;
		}

		public void DeleteOrderDetail(string promoCode, int bookId)
		{			
			var order = GetByPromoCode(promoCode);

			if (order == null)
				throw new Exception(string.Format("Ошибочный промокод {0}", promoCode));

			var orderDetails = order.OrderDetails.SingleOrDefault(y => y.BookId == bookId);
			order.OrderDetails.Remove(orderDetails);			
		}

	}
}