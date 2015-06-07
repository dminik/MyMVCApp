namespace WebSite.SignalR
{
	using System;

	using Microsoft.AspNet.SignalR;

	using ServiceLayer.Services;

	using WebSite.BLL;	

	public class LiveBookCounterHub : Hub, ILiveBookCounterHub
	{
		protected IOrderService OrdeService;
		protected IUserIdentity User;

		public LiveBookCounterHub(IOrderService ordeService, IUserIdentity user)
		{
			this.OrdeService = ordeService;
			this.User = user;
		}

		
		public void addBook(int bookId)
		{
			string errorMsg = "";
			int restAmount = 0;

			try
			{
				var promoCode = User.PromoCode;

				if (string.IsNullOrEmpty(promoCode)) 
					throw new Exception("PromoCode неопределен у текущего пользователя.");

			
				var isAdded = this.OrdeService.AddBook(promoCode, bookId, out restAmount);

				if (!isAdded)				
				{
					throw new Exception("Книга не добавилась, так как кончились экземпляры.");
				}			
			}
			catch (Exception ex)
			{
				errorMsg = ex.Message;
			}

			if (string.IsNullOrEmpty(errorMsg))							
				this.Clients.Others.refreshBookAmountForAll(bookId, restAmount, "");			
			
			this.Clients.Caller.addedBook(bookId, restAmount, errorMsg);
		}

		public void deleteBook(int bookId)
		{
			string errorMsg = "";
			int restAmount = 0;

			try
			{
				var promoCode = User.PromoCode;

				if (string.IsNullOrEmpty(promoCode))
					throw new Exception("PromoCode неопределен у текущего пользователя.");
				
				this.OrdeService.DeleteBook(promoCode, bookId, out restAmount);
			}
			catch (Exception ex)
			{
				errorMsg = ex.Message;
			}
						
			if (string.IsNullOrEmpty(errorMsg))							
				this.Clients.Others.refreshBookAmountForAll(bookId, restAmount, "");

			this.Clients.Caller.deletedBook(bookId, restAmount, errorMsg);
		}	
	}
}