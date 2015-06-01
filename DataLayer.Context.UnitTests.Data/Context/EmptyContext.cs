namespace DataLayer.Context.UnitTests.Data.Context
{
	using DataLayer.Context.Interfaces;

	/// <summary>
	/// Empty context
	/// </summary>
	public class EmptyContext
	{
		protected readonly IMainContext Context;
		public IMainContext MainContext
		{
			get { return this.Context; }
		}

		public EmptyContext()
		{
			var inMemoryContext = new InMemoryContextMock();
			this.Context = inMemoryContext.Instance.Object;
		}
	}
}