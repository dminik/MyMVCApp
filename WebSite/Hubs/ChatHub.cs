namespace WebSite.Hubs
{
	using System.Collections.Generic;
	using System.Linq;

	using Microsoft.AspNet.SignalR;

	using WebSite.Models;

	public class ChatHub : Hub
	{
		static readonly List<User> Users = new List<User>();

		// Отправка сообщений
		public void Send(string name, string message)
		{
			this.Clients.All.addMessage(name, message);
		}

		// Подключение нового пользователя
		public void Connect(string userName)
		{
			var id = this.Context.ConnectionId;


			if (Users.All(x => x.ConnectionId != id))
			{
				Users.Add(new User { ConnectionId = id, Name = userName });

				// Посылаем сообщение текущему пользователю
				this.Clients.Caller.onConnected(id, userName, Users);

				// Посылаем сообщение всем пользователям, кроме текущего
				this.Clients.AllExcept(id).onNewUserConnected(id, userName);
			}
		}

		// Отключение пользователя
		public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
		{
			var item = Users.FirstOrDefault(x => x.ConnectionId == this.Context.ConnectionId);
			if (item != null)
			{
				Users.Remove(item);
				var id = this.Context.ConnectionId;
				this.Clients.All.onUserDisconnected(id, item.Name);
			}

			return base.OnDisconnected(stopCalled);
		}
	}
}