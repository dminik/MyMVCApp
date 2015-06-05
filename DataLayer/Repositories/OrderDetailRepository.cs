﻿namespace DataLayer.Repository.Repositories
{
	using System;
	using System.Linq;

	using DataLayer.Context.Interfaces;
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public class OrderDetailRepository : GenericRepository<OrderDetail, int>, IOrderDetailRepository
	{
		public OrderDetailRepository(IMainContext context)
			: base(context)
		{
		}

		public OrderDetail GetByBookId(string promoCode, int bookId)
		{
			return FindBy(x => 
					x.Order.PromoCode == promoCode && 
					x.BookId == bookId)
				.SingleOrDefault();
		}

		public void Delete(int orderId, int bookId)
		{
			var orderDetails = FindBy(y => y.BookId == bookId && y.OrderId == orderId).FirstOrDefault();
			if (orderDetails != null)
				Delete(orderDetails);
		}
		public OrderDetail Add(int orderId, int bookId)
		{
			var orderDetail = new OrderDetail { OrderId = orderId, BookId = bookId, };
			Add(orderDetail);
			return orderDetail;
		}

		public int GetAmountOrdered(int bookId)
		{
			return FindBy(y => y.BookId == bookId).Count();
		}
	}
}