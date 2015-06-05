namespace DataLayer.Repository.Repositories
{
	using System;
	using System.Linq;

	using DataLayer.Context.Interfaces;
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
	{
		public OrderRepository(IMainContext context)
			: base(context)
		{
		}

		public Order GetByPromoCode(string promoCode)
		{
			return FindBy(x => x.PromoCode == promoCode).SingleOrDefault();
		}		
	}
}