namespace WebAPI
{
	using System.Reflection;
	using System.Web.Http;

	using Autofac;
	using Autofac.Integration.WebApi;

	using ServiceLayer.IoC;

	public class IoCConfig
	{
		public static void Register()
		{			
			var builder = new ContainerBuilder();

			builder.RegisterModule(new ServiceModule());
			
			builder.RegisterInstance(new SomeRepo()).As<ISomeRepo>();

			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			var container = builder.Build();			
			GlobalConfiguration.Configuration.DependencyResolver = 
				new AutofacWebApiDependencyResolver(container);
		}
	}
}