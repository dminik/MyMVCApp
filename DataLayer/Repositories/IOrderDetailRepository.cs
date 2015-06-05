namespace DataLayer.Repository.Repositories
{
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public interface IOrderDetailRepository : IGenericRepository<OrderDetailEntity, int>
	{
		OrderDetailEntity GetByBookId(string promoCode, int bookId);
	}
}