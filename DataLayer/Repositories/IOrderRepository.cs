namespace DataLayer.Repository.Repositories
{
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public interface IOrderRepository : IGenericRepository<OrderEntity, int>
	{
		OrderEntity GetByPromoCode(string promoCode);

		OrderDetailEntity AddOrderDetail(string promoCode, int bookId);

		void DeleteOrderDetail(string promoCode, int bookId);

	}
}