namespace DataLayer.Repository.Repositories.Base
{
	using System;

	using DataLayer.Context.Interfaces;

	/// <summary>
	/// The Entity Framework implementation of IUnitOfWork
	/// </summary>
	public class UnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// The DbContext
		/// </summary>
		private IMainContext mContext;

		/// <summary>
		/// Initializes a new instance of the UnitOfWork class.
		/// </summary>
		/// <param name="context">The object context</param>
		public UnitOfWork(IMainContext context)
		{
			this.mContext = context;
		}

		public IMainContext Context
		{
			get
			{
				return this.mContext;
			}
			set
			{
				this.mContext = value;
			}
		}

		/// <summary>
		/// Saves all pending changes
		/// </summary>
		/// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
		public int Save()
		{
			// Save changes with the default options
			return this.mContext.SaveChanges();
		}

		/// <summary>
		/// Disposes the current object
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Disposes all external resources.
		/// </summary>
		/// <param name="disposing">The dispose indicator.</param>
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.mContext != null)
				{
					this.mContext.Dispose();
					this.mContext = null;
				}
			}
		}
	}
}