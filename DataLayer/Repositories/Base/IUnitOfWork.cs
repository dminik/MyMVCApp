namespace DataLayer.Repository.Repositories.Base
{
	using System;

	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Saves all pending changes
		/// </summary>
		/// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
		int Save();
	}
}