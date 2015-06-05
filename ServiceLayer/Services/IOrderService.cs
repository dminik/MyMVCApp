namespace ServiceLayer.Services
{
	using DataLayer.Model.Entities;

	public interface IOrderService
	{
		OrderEntity CreateOrder();

		void AddBook(string promoCode, int bookId);

		void RemoveBook(string promoCode, int bookId);

		void ChangeStatus(string promoCode, OrderStatus status);
	}
}