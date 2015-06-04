using Microsoft.Owin;
using WebSite;

[assembly: OwinStartup(typeof(Startup))]
namespace WebSite
{
	using Owin;

	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
		}
	}
}