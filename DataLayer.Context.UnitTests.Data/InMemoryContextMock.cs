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

					f.Setup(m => m.Set<Book>()).Returns(this.Books);
					f.Setup(m => m.Set<Order>()).Returns(this.Orders);
										
				});

			this.Instance = new Mock<IMainContext>();

			startupAction(this.Instance);
		}

				
		IDbSet<Book> mBookEntity;
		public IDbSet<Book> Books
		{
			get
			{				
				return this.mBookEntity ?? (this.mBookEntity = new FakeDbSet<Book>());
			}
		}

		IDbSet<Order> mOrderEntity;
		public IDbSet<Order> Orders
		{
			get
			{
				return this.mOrderEntity ?? (this.mOrderEntity = new FakeDbSet<Order>());
			}
		}
	}
}