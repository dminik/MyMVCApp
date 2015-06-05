namespace ServiceLayer.Services
{
	using DataLayer.Model.Entities;

	public interface IOrderService
	{
		OrderEntity CreateOrder();

		bool AddBook(string promoCode, int bookId, out int countOfReservedBooks);

		void DeleteBook(string promoCode, int bookId, out int countOfReservedBooks);

		void ChangeStatus(string promoCode, OrderStatus status);
	}
}