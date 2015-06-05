namespace ServiceLayer.Services
{
	using System;
	using System.Linq;

	using DataLayer.Model.Entities;
	using DataLayer.Repository;
	using DataLayer.Repository.Repositories;

	using ServiceLayer.Cache;
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

		public bool AddBook(string promoCode, int bookId, out int countOfReservedBooks)
		{
			bool isAdded;

			var order = this.orderRepository.GetByPromoCode(promoCode);
			if (order == null)
				throw new Exception("Ошибочный промокод");

			countOfReservedBooks = GetAmountOrdered(bookId);
			var book = dataRepositories.Books.GetByKey(bookId);
			if(book == null)
				throw new Exception(string.Format("Ошибочный bookId {0}", bookId));

			if (book.Amount > countOfReservedBooks)
			{
				dataRepositories.OrderDetails.Add(order.Id, bookId);
				UnitOfWork.Save();
				countOfReservedBooks++;
				isAdded = true;
			}
			else
			{
				isAdded = false;
			}

			return isAdded;
		}

		public void DeleteBook(string promoCode, int bookId, out int countOfReservedBooks)
		{			
			var order = this.orderRepository.GetByPromoCode(promoCode);
			if (order == null)
				throw new Exception(string.Format("Ошибочный промокод {0}", promoCode));

			dataRepositories.OrderDetails.Delete(order.Id, bookId);
			UnitOfWork.Save();
			countOfReservedBooks = order.OrderDetails.Count;								
		}

		public void ChangeStatus(string promoCode, OrderStatus status)
		{
			var order = this.orderRepository.GetByPromoCode(promoCode);
			if (order == null)
				throw new Exception(string.Format("Ошибочный промокод {0}", promoCode));
			order.Status = status;
			UnitOfWork.Save();
		}

		public int GetAmountOrdered(int bookId)
		{
			return dataRepositories.OrderDetails.GetAmountOrdered(bookId);
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