﻿namespace WebSite.Hubs
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
			string errorMsg = "";
			int countOfReservedBooks = 0;

			try
			{
				var promoCode = User.PromoCode;

				if (string.IsNullOrEmpty(promoCode)) 
					throw new Exception("PromoCode неопределен у текущего пользователя.");

			
				var isAdded = this.OrdeService.AddBook(promoCode, bookId, out countOfReservedBooks);

				if (!isAdded)				
				{
					throw new Exception("Книга не добавилась, так как кончились экземпляры.");
				}			
			}
			catch (Exception ex)
			{
				errorMsg = ex.Message;
			}

			this.Clients.All.addedBook(bookId, countOfReservedBooks, errorMsg);
		}

		public void deleteBook(int bookId)
		{
			string errorMsg = "";
			int countOfReservedBooks = 0;

			try
			{
				var promoCode = User.PromoCode;

				if (string.IsNullOrEmpty(promoCode))
					throw new Exception("PromoCode неопределен у текущего пользователя.");
				
				this.OrdeService.DeleteBook(promoCode, bookId, out countOfReservedBooks);

			}
			catch (Exception ex)
			{
				errorMsg = ex.Message;
			}

			this.Clients.All.deletedBook(bookId, countOfReservedBooks, errorMsg);			
		}	
	}
}