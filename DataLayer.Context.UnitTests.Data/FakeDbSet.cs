namespace DataLayer.Context.UnitTests.Data
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;

	public class FakeDbSet<T> : IDbSet<T> where T : class
	{
		readonly HashSet<T> mData;
		readonly IQueryable mQuery;
		int mIdentity = 1;
		List<PropertyInfo> mKeyProperties;

		void GetKeyProperties()
		{
			this.mKeyProperties = new List<PropertyInfo>();
			var properties = typeof(T).GetProperties();
			foreach (var property in properties)
				foreach (Attribute attribute in property.GetCustomAttributes(true))
					if (attribute is KeyAttribute || attribute is DatabaseGeneratedAttribute)
						this.mKeyProperties.Add(property);
		}

		void GenerateId(T entity)
		{
			// If non-composite integer key
			if (this.mKeyProperties.Count == 1 &&
				(this.mKeyProperties[0].PropertyType == typeof(Int32) || this.mKeyProperties[0].PropertyType == typeof(Int64)))
				this.mKeyProperties[0].SetValue(entity, this.mIdentity++, null);
		}

		public FakeDbSet(IEnumerable<T> startData = null)
		{
			this.GetKeyProperties();
			this.mData = (startData != null ? new HashSet<T>(startData) : new HashSet<T>());
			this.mQuery = this.mData.AsQueryable();
		}

		public virtual T Find(params object[] keyValues)
		{
			if (keyValues.Length != this.mKeyProperties.Count)
				throw new ArgumentException("Incorrect number of keys passed to find method");

			var keyQuery = this.AsQueryable();
			for (var i = 0; i < keyValues.Length; i++)
			{
				var x = i; // nested linq
				keyQuery = keyQuery.Where(entity => this.mKeyProperties[x].GetValue(entity, null).Equals(keyValues[x]));
			}

			return keyQuery.SingleOrDefault();
		}

		public T Add(T item)
		{
			this.GenerateId(item);
			this.mData.Add(item);
			return item;
		}

		public T Remove(T item)
		{
			this.mData.Remove(item);
			return item;
		}

		public T Attach(T item)
		{
			this.mData.Add(item);
			return item;
		}

		public void Detach(T item)
		{
			this.mData.Remove(item);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this.mData.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		Type IQueryable.ElementType
		{
			get { return this.mQuery.ElementType; }
		}
		Expression IQueryable.Expression
		{
			get { return this.mQuery.Expression; }
		}
		IQueryProvider IQueryable.Provider
		{
			get { return this.mQuery.Provider; }
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.mData.GetEnumerator();
		}

		public T Create()
		{
			return Activator.CreateInstance<T>();
		}

		public ObservableCollection<T> Local
		{
			get { return new ObservableCollection<T>(this.mData); }
		}

		public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
		{
			return Activator.CreateInstance<TDerivedEntity>();
		}
	}
}