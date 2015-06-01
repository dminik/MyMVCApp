namespace DataLayer.Context.UnitTests.Data.Context
{
	using DataLayer.Context.Interfaces;

	/// <summary>
	/// Empty context
	/// </summary>
	public class EmptyContext
	{
		protected readonly IMainContext Context;

		public Moq.Mock<IMainContext> InMemoryContextMockInstance;

		public IMainContext MainContext
		{
			get { return this.Context; }
		}

		public EmptyContext()
		{
			var inMemoryContext = new InMemoryContextMock();
			InMemoryContextMockInstance = inMemoryContext.Instance;
			this.Context = inMemoryContext.Instance.Object;
		}
	}
}