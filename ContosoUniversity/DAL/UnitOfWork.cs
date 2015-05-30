using System;
using ContosoUniversity.Models;

namespace ContosoUniversity.DAL
{
	public class UnitOfWork : IDisposable
	{
		private readonly SchoolContext context = new SchoolContext();
		private GenericRepository<Department> departmentRepository;
		private CourseRepository courseRepository;
		

		public GenericRepository<Department> DepartmentRepository
		{
			get
			{

				if (this.departmentRepository == null)
				{
					this.departmentRepository = new GenericRepository<Department>(context);
				}
				return departmentRepository;
			}
		}

		public CourseRepository CourseRepository
		{
			get
			{

				if (this.courseRepository == null)
				{
					this.courseRepository = new CourseRepository(context);
				}
				return courseRepository;
			}
		}

		private GenericRepository<Book> bookRepository;
		public GenericRepository<Book> BookRepository
		{
			get
			{
				if (this.bookRepository == null)
				{
					this.bookRepository = new GenericRepository<Book>(context);
				}
				return bookRepository;
			}
		}

		private GenericRepository<Order> orderRepository;
		public GenericRepository<Order> OrderRepository
		{
			get
			{
				if (this.orderRepository == null)
				{
					this.orderRepository = new GenericRepository<Order>(context);
				}
				return orderRepository;
			}
		}

		private OrderDetailRepository orderDetailRepository;
		public OrderDetailRepository OrderDetailRepository
		{
			get
			{
				if (this.orderDetailRepository == null)
				{
					this.orderDetailRepository = new OrderDetailRepository(context);
				}
				return orderDetailRepository;
			}
		}

		public void Save()
		{
			context.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
