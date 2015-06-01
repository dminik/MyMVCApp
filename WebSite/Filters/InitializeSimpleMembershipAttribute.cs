namespace WebSite.Filters
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	using System.Threading;
	using System.Web.Mvc;

	using WebMatrix.WebData;

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
	{
		private static SimpleMembershipInitializer _initializer;

		private static object _initializerLock = new object();

		private static bool _isInitialized;

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			// Ensure ASP.NET Simple Membership is initialized only once per app start
			LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
		}

		private class UsersContext : DbContext
		{
			public UsersContext()
				: base("MainContext")
			{
			}

			public DbSet<UserProfile> UserProfiles { get; set; }
		}

		[Table("UserProfile")]
		private class UserProfile
		{
			[Key]
			[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
			public int UserId { get; set; }

			public string UserName { get; set; }
		}

		private class SimpleMembershipInitializer
		{
			public SimpleMembershipInitializer()
			{
				Database.SetInitializer<UsersContext>(null);

				try
				{
					using (var context = new UsersContext())
					{
						if (!context.Database.Exists())
						{
							// Create the SimpleMembership database without Entity Framework migration schema
							((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
						}
					}

					WebSecurity.InitializeDatabaseConnection("MainContext", "UserProfile", "UserId", "UserName", true);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(
						"The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588",
						ex);
				}
			}
		}
	}
}