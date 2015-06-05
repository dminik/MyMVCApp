namespace ServiceLayer.Services
{
	using DataLayer.Model.Entities;

	using ServiceLayer.Common;

	public interface IOrderService : IEntityService<Order, int>
	{
		Order CreateOrder();

		bool AddBook(string promoCode, int bookId, out int countOfReservedBooks);

		void DeleteBook(string promoCode, int bookId, out int countOfReservedBooks);

		void ChangeStatus(string promoCode, OrderStatus status);

		int GetAmountOrdered(int bookId);
	}
}