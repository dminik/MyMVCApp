namespace WebSite.IoC
{
	using System.Reflection;

	using Autofac;

	using Module = Autofac.Module;

	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule(new RepositoryModule());

			builder.RegisterAssemblyTypes(Assembly.Load("ServiceLayer"))
					  .Where(t => t.Name.EndsWith("Service"))
					  .AsImplementedInterfaces()
					  .InstancePerLifetimeScope();
			
			
		}

	}
}