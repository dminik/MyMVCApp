$(function () {

	var dragdropList = new DragdropList();
	var hub = $.connection.liveBookCounterHub;

	// Отсылаем на сервер	
	function sendAddBookToServer(bookId) {
		if (bookId) {
			hub.server.addBook(bookId);			
		} else
			alert("bookId is undefined");
	}

	function sendAddBookToServer1() {
		sendAddBookToServer(1);
	}

	function sendDeleteBookToServer(bookId) {
		if (bookId)
			hub.server.deleteBook(bookId);
		else 
			alert("bookId is undefined");		
	}

	function sendDeleteBookToServer1() {
		sendDeleteBookToServer(1);
	}

	// Вызывается сервером уже после добавления книги	
	hub.client.OnRefreshBookAmountForAll = dragdropList.refreshBookAmountForAll;
	hub.client.OnAddBookCompleted = dragdropList.addBookCompleted;
	hub.client.OnDeleteBookCompleted = dragdropList.addBookCompleted;


	// Привязка событий контролов	
	$.connection.hub.start().done(function () {

		$('#btnDeleteBook').click(sendAddBookToServer1);
		$('#btnAddBook').click(sendDeleteBookToServer1);   

		dragdropList.Init(sendAddBookToServer, sendDeleteBookToServer);
		

	});
});