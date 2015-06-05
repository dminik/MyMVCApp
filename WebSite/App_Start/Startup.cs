using Microsoft.Owin;

using WebSite;

[assembly: OwinStartup(typeof(Startup))]
namespace WebSite
{
	using System.Reflection;

	using Autofac;
	using Autofac.Integration.SignalR;

	using Microsoft.AspNet.SignalR;


	using Owin;

	using ServiceLayer.IoC;

	using WebSite.BLL;

	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var builder = new ContainerBuilder();

			// STANDARD SIGNALR SETUP:

			// Get your HubConfiguration. In OWIN, you'll create one rather than using GlobalHost.
			var config = new HubConfiguration();


			builder.RegisterModule(new ServiceModule());

			builder.RegisterType(typeof(UserIdentity)).As(typeof(IUserIdentity)).ExternallyOwned();

			builder.RegisterHubs(Assembly.GetExecutingAssembly());

			

			
			var container = builder.Build();
			config.Resolver = new AutofacDependencyResolver(container);

			// OWIN SIGNALR SETUP:

			// Register the Autofac middleware FIRST, then the standard SignalR middleware.
			app.UseAutofacMiddleware(container);
			app.MapSignalR("/signalr", config);

		}
	}
}