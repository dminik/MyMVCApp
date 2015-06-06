namespace ServiceLayer.Services
{
	using System;
	using System.Collections.Generic;

	using DataLayer.Model.Entities;
	using DataLayer.Repository;
	using DataLayer.Repository.Repositories;	
	using ServiceLayer.Common;

	public class OrderService : EntityService<Order, int>, IOrderService		
	{
		private readonly IOrderRepository orderRepository;

		private readonly IDataRepositories dataRepositories;

		public OrderService(IDataRepositories dataRepositories)
			: base(dataRepositories.Orders, dataRepositories)
		{
			this.orderRepository = dataRepositories.Orders;
			this.dataRepositories = dataRepositories;
		}

		public Order CreateOrder()
		{
			var promoCode = GeneratePromoCode();
			var entity = new Order {PromoCode = promoCode, Status = OrderStatus.BuildingByUser, };
			base.Create(entity);
			UnitOfWork.Save();

			return entity;
		}

		public bool AddBook(string promoCode, int bookId, out int restAmount)
		{
			bool isAdded;

			var order = this.orderRepository.GetByPromoCode(promoCode);
			if (order == null)
				throw new Exception("Ошибочный промокод");

			restAmount = GetRestAmount(bookId);
			var book = dataRepositories.Books.GetByKey(bookId);
			if(book == null)
				throw new Exception(string.Format("Ошибочный bookId {0}", bookId));

			if (restAmount > 0)
			{
				dataRepositories.OrderDetails.Add(order.Id, bookId);
				UnitOfWork.Save();
				restAmount--;
				isAdded = true;
			}
			else
			{
				isAdded = false;
			}

			return isAdded;
		}

		public void DeleteBook(string promoCode, int bookId, out int restAmount)
		{			
			var order = this.orderRepository.GetByPromoCode(promoCode);
			if (order == null)
				throw new Exception(string.Format("Ошибочный промокод {0}", promoCode));

			dataRepositories.OrderDetails.Delete(order.Id, bookId);
			UnitOfWork.Save();
			restAmount = GetRestAmount(bookId);								
		}

		public void ChangeStatus(string promoCode, OrderStatus status)
		{
			var order = this.orderRepository.GetByPromoCode(promoCode);
			if (order == null)
				throw new Exception(string.Format("Ошибочный промокод {0}", promoCode));
			order.Status = status;
			UnitOfWork.Save();
		}

		public IEnumerable<OrderDetail> GetByPromoCode(string promoCode)
		{
			return dataRepositories.OrderDetails.GetByPromoCode(promoCode);
		}

		public int GetRestAmount(int bookId)
		{
			var amount = dataRepositories.Books.GetByKey(bookId).Amount;
			var orderedAmount = dataRepositories.OrderDetails.GetRestAmount(bookId);
			var restAmount = amount - orderedAmount;
			return restAmount > 0 ? restAmount : 0;
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