namespace WebSite.Hubs
{
	public interface ILiveBookCounterHub
	{
		void addBook(int bookId);

		void deleteBook(int bookId);
	}
}