namespace WebSite.SignalR
{
	public interface ILiveBookCounterHub
	{
		void addBook(int bookId);

		void deleteBook(int bookId);
	}
}