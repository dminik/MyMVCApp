namespace ServiceLayer.Services
{
	using DataLayer.Model.Entities;

	using ServiceLayer.Common;

	public interface IOrderService : IEntityService<Order, int>
	{
		Order CreateOrder();

		bool AddBook(string promoCode, int bookId, out int restAmount);

		void DeleteBook(string promoCode, int bookId, out int restAmount);

		void ChangeStatus(string promoCode, OrderStatus status);

		int GetRestAmount(int bookId);
	}
}