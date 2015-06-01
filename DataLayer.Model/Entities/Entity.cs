namespace DataLayer.Model.Entities
{
	using System.ComponentModel.DataAnnotations;

	
	public abstract class Entity<T> : IEntity<T>
	{
		[Key]
		public virtual T Id { get; set; }
	}
}