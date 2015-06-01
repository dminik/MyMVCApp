namespace DataLayer.Context
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
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

		public IDbSet<BookEntity> Books { get; set; }

		public IDbSet<OrderEntity> Orders { get; set; }

		public IDbSet<OrderDetailEntity> OrderDetails { get; set; }

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

	public class UsersContext : DbContext
	{
		public UsersContext()
			: base("MainContext")
		{
		}

		public DbSet<UserProfile> UserProfiles { get; set; }
	}

	[Table("UserProfile")]
	public class UserProfile
	{
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int UserId { get; set; }
		public string UserName { get; set; }
	}
}