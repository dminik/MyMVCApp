namespace WebSite
{
	using System.Web.Mvc;

	using Autofac;
	using Autofac.Integration.Mvc;

	using WebSite.IoC;

	public class IoCConfig
	{
		public static void Register()
		{
			//Autofac Configuration
			var builder = new Autofac.ContainerBuilder();

			builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

			builder.RegisterModule(new RepositoryModule());
			builder.RegisterModule(new ServiceModule());
			builder.RegisterModule(new EFModule());

			var container = builder.Build();

			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}