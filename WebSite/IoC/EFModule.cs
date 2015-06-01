namespace WebSite.IoC
{
	using Autofac;

	using DataLayer.Context;
	using DataLayer.Context.Interfaces;
	using DataLayer.Repository;

	public class EFModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule(new RepositoryModule());

			builder.RegisterType(typeof(MainContext)).As(typeof(IMainContext)).InstancePerLifetimeScope();
			builder.RegisterType(typeof(DataRepositories)).As(typeof(IDataRepositories)).InstancePerRequest();         

		}

	}
}