﻿namespace DataLayer.Repository.Repositories
{
	using DataLayer.Model.Entities;
	using DataLayer.Repository.Repositories.Base;

	public interface IOrderRepository : IGenericRepository<OrderEntity, int>
	{
		OrderEntity GetByPromoCode(string promoCode);			
	}
}