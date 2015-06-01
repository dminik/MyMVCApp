namespace DataLayer.Repository.Repositories
{
	using DataLayer.Context.Interfaces;
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public class OrderRepository : GenericRepository<OrderEntity>, IOrderRepository
	{
		public OrderRepository(IMainContext context)
			: base(context)
		{
		}
	}
}