$(function () {


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


	// Вызывается сервером	
	hub.client.addedBook = function (bookId, restAmount, errorMsg) {		
		if (errorMsg) {
			recycleImage($("#" + bookId));

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

		$('#btnAddBook').click(addBook1);
		$('#btnDeleteBook').click(deleteBook1);   

		dragdropListInit(addBook, deleteBook);
		

	});
});