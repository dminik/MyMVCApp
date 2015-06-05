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

		public OrderEntity CreateOrder()
		{
			var promoCode = GeneratePromoCode();
			var entity = new OrderEntity {PromoCode = promoCode, Status = OrderStatus.BuildingByUser, };
			base.Create(entity);
			UnitOfWork.Save();

			return entity;
		}

		public void AddBook(string promoCode, int bookId)
		{
			this.orderRepository.AddOrderDetail(promoCode, bookId);			
			UnitOfWork.Save();
		}

		public void RemoveBook(string promoCode, int bookId)
		{
			this.orderRepository.DeleteOrderDetail(promoCode, bookId);
			UnitOfWork.Save();				
		}

		public void ChangeStatus(string promoCode, OrderStatus status)
		{
			var order = this.orderRepository.GetByPromoCode(promoCode);
			if (order == null)
				throw new Exception(string.Format("Ошибочный промокод {0}", promoCode));
			order.Status = status;
			UnitOfWork.Save();
		}

		private string GeneratePromoCode()
		{
			string promoCode;
			do
			{
				promoCode = Guid.NewGuid().ToString();				
			}
			while (orderRepository.GetByPromoCode(promoCode) != null);

			return promoCode;
		}
	}
}