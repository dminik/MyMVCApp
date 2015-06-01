namespace WebSite
{
	using System.Web.Mvc;
	using Autofac;
	using Autofac.Integration.Mvc;
	using ServiceLayer.IoC;


	public class IoCConfig
	{
		public static void Register()
		{
			//Autofac Configuration
			var builder = new Autofac.ContainerBuilder();

			builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
			
			builder.RegisterModule(new ServiceModule());		

			var container = builder.Build();

			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}