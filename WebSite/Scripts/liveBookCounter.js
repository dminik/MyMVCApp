$(function () {


	var hub = $.connection.liveBookCounterHub;

	// Отсылаем на сервер
	function addBook() {
		hub.server.addBook($('#bookId').val());
	}

	function deleteBook() {
		hub.server.deleteBook($('#bookId').val());
	}


	// Вызывается сервером	
	hub.client.addedBook = function (bookId, countOfReservedBooks) {
		$('#live-counter').val(countOfReservedBooks);
	};

	hub.client.deletedBook = function (bookId, countOfReservedBooks) {
		$('#live-counter').val(countOfReservedBooks);
	};


	// Привязка событий контролов	
	$.connection.hub.start().done(function () {

		$('#btnAddBook').click(addBook);
		$('#btnDeleteBook').click(deleteBook);

	});
});

