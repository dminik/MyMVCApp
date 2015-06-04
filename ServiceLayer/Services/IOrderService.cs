namespace ServiceLayer.Services
{
	using DataLayer.Model.Entities;

	public interface IOrderService
	{
		void CreateOrder();

		void AddBook(string promoCode, int bookId);

		void RemoveBook(string promoCode, int bookId);

		void ChangeStatus(string promoCode, OrderStatus status);
	}
}