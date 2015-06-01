namespace WebSite.Controllers
{
	using System.Web.Mvc;

	using ServiceLayer;

	public abstract class BaseController : Controller
	{
		protected IBookService BookService;

		protected override void Dispose(bool disposing)
		{
			this.BookService.Dispose();
			base.Dispose(disposing);
		}
	}
}