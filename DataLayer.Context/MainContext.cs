namespace DataLayer.Context
{
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;

	using DataLayer.Context.Interfaces;
	using DataLayer.Model.Entities;

	public class MainContext : DbContext, IMainContext
	{
		public MainContext()
			: base(string.Format("name={0}", "MainContext"))
		{
		}

		public IDbSet<Book> Books { get; set; }

		public IDbSet<Order> Orders { get; set; }

		public IDbSet<OrderDetail> OrderDetails { get; set; }

		IDbSet<TEntity> IMainContext.Set<TEntity>()
		{
			return this.Set<TEntity>();
		}

		public void SetModified(object entity)
		{
			this.Entry(entity).State = EntityState.Modified;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			base.OnModelCreating(modelBuilder);
		}
	}
}