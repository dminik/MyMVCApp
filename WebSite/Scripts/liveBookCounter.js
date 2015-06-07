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
	hub.client.refreshBookAmountForAll = function (bookId, restAmount, errorMsg) {
		var $book = $("#" + bookId);
		if (errorMsg) {			
				alert(errorMsg);
		} else {
			if (bookId == 1)
				$('#live-counter').val(restAmount);

			//обновляем инфу на книжке
			var $bookAmountOrderedElement = $("#" + bookId + " .amount-ordered");
			$bookAmountOrderedElement.text(restAmount);

			if (restAmount == 0)
				$book.draggable("disable");
			else
				$book.draggable("enable");
		}
	};

	
	hub.client.addedBook = function (bookId, restAmount, errorMsg) {
		var $book = $("#" + bookId);
		if (errorMsg) {
			dragdropList.moveBookVisualElementToGallery($book);

			if (errorMsg === "Ошибочный промокод")
				document.getElementById('logoutForm').submit();
			else
				alert(errorMsg);
		} else {
			if (bookId == 1)
				$('#live-counter').val(restAmount);

			//обновляем инфу на книжке
			var $bookAmountOrderedElement = $("#" + bookId + " .amount-ordered");
			$bookAmountOrderedElement.text(restAmount);
		}
	};

	hub.client.deletedBook = hub.client.addedBook;


	// Привязка событий контролов	
	$.connection.hub.start().done(function () {

		$('#btnDeleteBook').click(sendAddBookToServer1);
		$('#btnAddBook').click(sendDeleteBookToServer1);   

		dragdropList.Init(sendAddBookToServer, sendDeleteBookToServer);
		

	});
});