namespace ServiceLayer.Services
{
	using System;
	using System.Linq;

	using DataLayer.Model.Entities;
	using DataLayer.Repository;
	using DataLayer.Repository.Repositories;

	using ServiceLayer.Cache;
	using ServiceLayer.Common;

	public class OrderService : EntityService<OrderEntity, int>, IOrderService		
	{
		private readonly IOrderRepository orderRepository;

		private IDataRepositories dataRepositories;

		public OrderService(IDataRepositories dataRepositories)
			: base(dataRepositories.Orders, dataRepositories)
		{
			this.orderRepository = dataRepositories.Orders;
			this.dataRepositories = dataRepositories;
		}
		
		public void CreateOrder()
		{
			var promoCode = GeneratePromoCode();
			var entity = new OrderEntity {PromoCode = promoCode, Status = OrderStatus.BuildingByUser, };
			base.Create(entity);
		}

		public void AddBook(string promoCode, int bookId)
		{
			var order = this.orderRepository.FindBy(x => x.PromoCode == promoCode).SingleOrDefault();
			if (order == null)
				throw new Exception(string.Format("Ошибочный промокод {0}", promoCode));

			order.OrderDetails.Add(new OrderDetailEntity{BookId = bookId, });
		}

		public void RemoveBook(string promoCode, int bookId)
		{
			var order = this.orderRepository.FindBy(x => x.PromoCode == promoCode).SingleOrDefault();
			if (order != null)
			{
				var orderDetails = order.OrderDetails.SingleOrDefault(x => x.BookId == bookId);
				if (orderDetails != null)
					order.OrderDetails.Remove(orderDetails);
			}
		}

		public void ChangeStatus(string promoCode, OrderStatus status)
		{
			var order = this.orderRepository.FindBy(x => x.PromoCode == promoCode).SingleOrDefault() ?? new OrderEntity();
			order.Status = status;
		}

		private string GeneratePromoCode()
		{
			string code;
			do
			{
				code = Guid.NewGuid().ToString();
			}
			while (orderRepository.FindBy(x => x.PromoCode == code).FirstOrDefault() == null);

			return code;
		}
	}
}