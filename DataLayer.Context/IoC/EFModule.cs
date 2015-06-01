namespace DataLayer.Context.IoC
{
	using Autofac;

	using DataLayer.Context;
	using DataLayer.Context.Interfaces;

	public class EFModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{			
			builder.RegisterType(typeof(MainContext)).As(typeof(IMainContext)).InstancePerLifetimeScope();		
		}
	}
}