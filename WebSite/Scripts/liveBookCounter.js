$(function () {

	var dragdropList = new DragdropList();
	var hub = $.connection.liveBookCounterHub;

	// Отсылаем на сервер	
	function sendAddBookToServer(bookId) {
		if (bookId) {
			hub.server.addBook(bookId);			
		} else
			dragdropList.showAlert("bookId is undefined");
	}
	
	function sendDeleteBookToServer(bookId) {
		if (bookId)
			hub.server.deleteBook(bookId);
		else 
			dragdropList.showAlert("bookId is undefined");
	}

	function sendCommitOrderToServer() {		
			hub.server.commitOrder();		
	}

	function sendReopenOrderToServer() {
		hub.server.reopenOrder();
	}

	// Вызывается сервером уже после добавления книги	
	hub.client.OnRefreshBookAmountForAll = dragdropList.refreshBookAmountForAll;
	hub.client.OnAddBookCompleted = dragdropList.addBookCompleted;
	hub.client.OnDeleteBookCompleted = dragdropList.deleteBookCompleted;

	hub.client.OnCommitOrderCompleted = dragdropList.commitOrderCompleted;
	hub.client.OnReopenOrderCompleted = dragdropList.reopenOrderCompleted;


	// Привязка событий контролов	
	$.connection.hub.start().done(function () {
		dragdropList.Init(sendAddBookToServer, sendDeleteBookToServer, sendCommitOrderToServer, sendReopenOrderToServer);
	});
});