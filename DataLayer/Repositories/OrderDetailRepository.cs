namespace DataLayer.Repository.Repositories
{
	using DataLayer.Context.Interfaces;
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public class OrderDetailRepository : GenericRepository<OrderDetailEntity>, IOrderDetailRepository
	{
		public OrderDetailRepository(IMainContext context)
			: base(context)
		{
		}
	}
}