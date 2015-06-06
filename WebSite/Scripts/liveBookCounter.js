$(function () {


	var hub = $.connection.liveBookCounterHub;

	// Отсылаем на сервер	
	function addBook(bookId) {
		hub.server.addBook(bookId);
	}

	function addBook1() {
		addBook(1);
	}

	function deleteBook(bookId) {
		hub.server.deleteBook(bookId);
	}

	function deleteBook1() {
		deleteBook(1);
	}


	// Вызывается сервером	
	hub.client.addedBook = function (bookId, countOfReservedBooks, errorMsg) {
		if (errorMsg) {
			if (errorMsg === "Ошибочный промокод") {
				document.getElementById('logoutForm').submit();
			}
			else
				alert(errorMsg);
		} else
			$('#live-counter').val(countOfReservedBooks);
	};

	hub.client.deletedBook = function (bookId, countOfReservedBooks, errorMsg) {
		if (errorMsg) {
			if (errorMsg === "Ошибочный промокод") {
				document.getElementById('logoutForm').submit();
			}
			else
				alert(errorMsg);
		} else
			$('#live-counter').val(countOfReservedBooks);
	};


	// Привязка событий контролов	
	$.connection.hub.start().done(function () {

		$('#btnAddBook').click(addBook1);
		$('#btnDeleteBook').click(deleteBook1); 

		dragdropListInit(addBook, deleteBook);
		

	});
});

