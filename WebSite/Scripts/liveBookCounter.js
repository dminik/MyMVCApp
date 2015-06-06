$(function () {

	var dragdropList = new DragdropList();
	var hub = $.connection.liveBookCounterHub;

	// Отсылаем на сервер	
	function addBook(bookId) {
		if (bookId) {
			hub.server.addBook(bookId);			
		} else
			alert("bookId is undefined");
	}

	function addBook1() {
		addBook(1);
	}

	function deleteBook(bookId) {
		if (bookId)
			hub.server.deleteBook(bookId);
		else 
			alert("bookId is undefined");		
	}

	function deleteBook1() {
		deleteBook(1);
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
			dragdropList.recycleImage($book);

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

		$('#btnDeleteBook').click(addBook1);
		$('#btnAddBook').click(deleteBook1);   

		dragdropList.Init(addBook, deleteBook);
		

	});
});