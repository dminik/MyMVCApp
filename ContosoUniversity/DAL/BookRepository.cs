using System;
using ContosoUniversity.Models;

namespace ContosoUniversity.DAL
{
    public class BookRepository : GenericRepository<Book>
    {
		public BookRepository(SchoolContext context)
            : base(context)
        {
        }

    }
}
