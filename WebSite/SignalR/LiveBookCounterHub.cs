namespace WebSite.SignalR
{
	using System;

	using Common;

	using DataLayer.Model.Entities;

	using Microsoft.AspNet.SignalR;

	using ServiceLayer.Services;

	using WebSite.BLL;	

	public class LiveBookCounterHub : Hub, ILiveBookCounterHub
	{
		protected IOrderService OrderService;
		protected IUserIdentity User;

		public LiveBookCounterHub(IOrderService ordeService, IUserIdentity user)
		{
			this.OrderService = ordeService;
			this.User = user;
		}
		
		public void addBook(int bookId)
		{
			string errorMsg = "";
			int restAmount = 0;
			decimal totalSum = 0;
			try
			{
				var promoCode = User.PromoCode;

				if (string.IsNullOrEmpty(promoCode)) 
					throw new ProgramException("PromoCode неопределен у текущего пользователя.");
			
				var isAdded = this.OrderService.AddBook(promoCode, bookId, out restAmount);
				totalSum = this.OrderService.GetOrderTotalSumByPromoCode(promoCode);

				if (!isAdded)				
				{
					throw new ProgramException("Книга не добавилась, так как кончились экземпляры.");
				}			
			}
			catch (Exception ex)
			{
				errorMsg = ex.Message;
			}

			if (string.IsNullOrEmpty(errorMsg))
				this.Clients.Others.OnRefreshBookAmountForAll(bookId, restAmount, "");

			this.Clients.Caller.OnAddBookCompleted(bookId, totalSum, restAmount, errorMsg);
		}

		public void deleteBook(int bookId)
		{
			string errorMsg = "";
			int restAmount = 0;
			decimal totalSum = 0;

			try
			{
				var promoCode = User.PromoCode;

				if (string.IsNullOrEmpty(promoCode))
					throw new ProgramException("PromoCode неопределен у текущего пользователя.");
				
				this.OrderService.DeleteBook(promoCode, bookId, out restAmount);
				totalSum = this.OrderService.GetOrderTotalSumByPromoCode(promoCode);
			}
			catch (Exception ex)
			{
				errorMsg = ex.Message;
			}
						
			if (string.IsNullOrEmpty(errorMsg))
				this.Clients.Others.OnRefreshBookAmountForAll(bookId, restAmount, "");

			this.Clients.Caller.OnDeleteBookCompleted(bookId, totalSum, restAmount, errorMsg);
		}

		
		public void reopenOrder()
		{
			var errorMsg = ChangeOrderStatus(OrderStatus.BuildingByUser);
			this.Clients.Caller.OnReopenOrderCompleted(errorMsg);
		}

		public void commitOrder()
		{
			var errorMsg = ChangeOrderStatus(OrderStatus.BuiltByUser);
			this.Clients.Caller.OnCommitOrderCompleted(errorMsg);
		}

		private string ChangeOrderStatus(OrderStatus orderStatus)
		{
			string errorMsg = "";
			int restAmount = 0;
			decimal totalSum = 0;

			try
			{
				var promoCode = User.PromoCode;

				if (string.IsNullOrEmpty(promoCode))
					throw new ProgramException("PromoCode неопределен у текущего пользователя.");

				OrderService.ChangeStatus(promoCode, orderStatus);
			}
			catch (Exception ex)
			{
				errorMsg = ex.Message;
			}

			return errorMsg;
		}
	}
}