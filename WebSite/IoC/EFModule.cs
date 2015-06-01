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
			builder.RegisterType(typeof(MainContext)).As(typeof(IMainContext)).InstancePerLifetimeScope();		
		}

	}
}