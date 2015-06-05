namespace DataLayer.Repository.Repositories
{
	using System.Linq;

	using DataLayer.Context.Interfaces;
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public class OrderDetailRepository : GenericRepository<OrderDetailEntity, int>, IOrderDetailRepository
	{
		public OrderDetailRepository(IMainContext context)
			: base(context)
		{
		}

		public OrderDetailEntity GetByBookId(string promoCode, int bookId)
		{
			return FindBy(x => 
					x.Order.PromoCode == promoCode && 
					x.BookId == bookId)
				.SingleOrDefault();
		}		
	}
}