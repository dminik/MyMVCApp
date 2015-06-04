namespace DataLayer.Model.Entities
{
	using System.ComponentModel.DataAnnotations;

	
	public abstract class Entity<TKey> : IEntity<TKey>
	{
		[Key]
		public virtual TKey Id { get; set; }
	}
}