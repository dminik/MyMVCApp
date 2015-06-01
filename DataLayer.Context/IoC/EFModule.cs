namespace DataLayer.Context.IoC
{
	using System.Data.Entity;

	using Autofac;

	using DataLayer.Context.Interfaces;

	using SchoolContextSeedInitializer = DataLayer.Context.Migrations.MainContextSeedInitializer;

	public class EFModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			Database.SetInitializer(new SchoolContextSeedInitializer());
			builder.RegisterType(typeof(MainContext)).As(typeof(IMainContext)).InstancePerLifetimeScope();
		}
	}
}