namespace ServiceLayer.Services
{
	using System.Collections.Generic;

	using DataLayer.Model.Entities;

	using ServiceLayer.Common;

	public interface IOrderService : IEntityService<Order, int>
	{
		Order CreateOrder();

		bool AddBook(string promoCode, int bookId, out int restAmount);

		void DeleteBook(string promoCode, int bookId, out int restAmount);

		void ChangeStatus(string promoCode, OrderStatus status);

		IEnumerable<OrderDetail> GetOrderDetailListByPromoCode(string promoCode);

		decimal GetOrderTotalSumByPromoCode(string promoCode);

		int GetRestAmount(int bookId);

		Order GetByPromoCode(string promoCode);
	}
}