namespace DataLayer.Repository.Repositories
{
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public interface IOrderDetailRepository : IGenericRepository<OrderDetailEntity, int>
	{
		OrderDetailEntity GetByBookId(string promoCode, int bookId);
		OrderDetailEntity Add(int orderId, int bookId);	
		void Delete(int orderId, int bookId);
	}
}