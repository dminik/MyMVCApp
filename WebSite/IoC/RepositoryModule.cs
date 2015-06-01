namespace WebSite.IoC
{
	using System.Reflection;

	using Autofac;

	using Module = Autofac.Module;

	public class RepositoryModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.Load("DataLayer.Repository"))
				   .Where(t => t.Name.EndsWith("Repository"))
				   .AsImplementedInterfaces()
				  .InstancePerLifetimeScope();
		}
	}
}