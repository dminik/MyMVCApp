namespace DataLayer.Context.UnitTests.Data
{
	using System;
	using System.Data.Entity;

	using DataLayer.Context.Interfaces;
	using DataLayer.Model.Entities;

	using Moq;

	public class InMemoryContextMock
	{
		public Mock<IMainContext> Instance { get; set; }

		public InMemoryContextMock()
		{
			var startupAction = new Action<Mock<IMainContext>>(f =>
				{
					f.Setup(m => m.Books).Returns(this.Books);
					f.Setup(m => m.Orders).Returns(this.Orders);

					f.Setup(m => m.Set<BookEntity>()).Returns(this.Books);
					f.Setup(m => m.Set<OrderEntity>()).Returns(this.Orders);
										
				});

			this.Instance = new Mock<IMainContext>();

			startupAction(this.Instance);
		}

				
		IDbSet<BookEntity> mBookEntity;
		public IDbSet<BookEntity> Books
		{
			get
			{				
				return this.mBookEntity ?? (this.mBookEntity = new FakeDbSet<BookEntity>());
			}
		}

		IDbSet<OrderEntity> mOrderEntity;
		public IDbSet<OrderEntity> Orders
		{
			get
			{
				return this.mOrderEntity ?? (this.mOrderEntity = new FakeDbSet<OrderEntity>());
			}
		}
	}
}