namespace DataLayer.Repository
{
	using DataLayer.Context.Interfaces;
	using DataLayer.Repository.Repositories;
	using DataLayer.Repository.Repositories.Base;

	public class DataRepositories : UnitOfWork, IDataRepositories
	{
		private BookRepository mBookRepository;

		private OrderDetailRepository mOrderDetailRepository;

		private OrderRepository mOrderRepository;

		public DataRepositories(IMainContext context)
			: base(context)
		{
			this.Context = context;
		}

		public BookRepository Books
		{
			get
			{
				return this.mBookRepository ?? (this.mBookRepository = new BookRepository(this.Context));
			}
		}

		public OrderRepository Orders
		{
			get
			{
				return this.mOrderRepository ?? (this.mOrderRepository = new OrderRepository(this.Context));
			}
		}

		public OrderDetailRepository OrderDetails
		{
			get
			{
				return this.mOrderDetailRepository ?? (this.mOrderDetailRepository = new OrderDetailRepository(this.Context));
			}
		}
	}
}