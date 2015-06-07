namespace WebSite
{
	using System.Web.Mvc;

	using Autofac;
	using Autofac.Integration.Mvc;

	using ServiceLayer.IoC;

	using WebSite.BLL;
	
	public class IoCConfig
	{
		public static void Register()
		{
			//Autofac Configuration
			var builder = new ContainerBuilder();

			builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
			
			builder.RegisterModule(new ServiceModule());

			builder.RegisterType(typeof(UserIdentity)).As(typeof(IUserIdentity)).InstancePerHttpRequest();

			var container = builder.Build();

			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}