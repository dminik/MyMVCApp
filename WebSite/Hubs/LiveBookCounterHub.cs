namespace WebSite.Hubs
{
	using System;

	using DataLayer.Repository;

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
			var promoCode = User.PromoCode;

			if (string.IsNullOrEmpty(promoCode)) 
				throw new Exception("PromoCode неопределен у текущего пользователя.");

			int countOfReservedBooks;
			var isAdded = this.OrdeService.AddBook(promoCode, bookId, out countOfReservedBooks);

			if (isAdded)
				this.Clients.All.addedBook(bookId, countOfReservedBooks);
			else
			{
				throw new Exception("Книга не добавилась, так как кончились экземпляры.");
			}
		}

		public void deleteBook(int bookId)
		{
			var promoCode = User.PromoCode;

			if (string.IsNullOrEmpty(promoCode))
				throw new Exception("PromoCode неопределен у текущего пользователя.");

			int countOfReservedBooks;
			this.OrdeService.DeleteBook(promoCode, bookId, out countOfReservedBooks);

			this.Clients.All.deletedBook(bookId, countOfReservedBooks);			
		}	
	}
}