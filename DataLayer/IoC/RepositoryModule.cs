namespace DataLayer.Repository.IoC
{
	using System.Reflection;

	using Autofac;

	using DataLayer.Context.IoC;
	using DataLayer.Repository;

	using Module = Autofac.Module;

	public class RepositoryModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule(new EFModule());
			
			builder.RegisterAssemblyTypes(Assembly.Load("DataLayer.Repository"))
				   .Where(t => t.Name.EndsWith("Repository"))
				   .AsImplementedInterfaces()
				  .InstancePerLifetimeScope();

			builder.RegisterType(typeof(DataRepositories)).As(typeof(IDataRepositories)).InstancePerRequest();

		}
	}
}