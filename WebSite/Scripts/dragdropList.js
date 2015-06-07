function DragdropList() {

	var self = this;
	var $gallery = $("#gallery"),
		$trash = $("#trash"),
		$totalSum = $("#totalSum"),
		$alertmsg = $("#alert-msg");

	var maxTotalSum = parseFloat($("#maxTotalSum").val());
	
	var sendAddBookToServer;
	var sendDeleteBookToServer;

	var moveBookVisualElementToTrash = function ($item) {
		$item.fadeOut(function () {
			var $list = $("ul", $("#trash")).length ?
				$("ul", $("#trash")) :
				$("<ul class='gallery ui-helper-reset'/>").appendTo($("#trash"));

			$item.find("a.ui-icon-trash").remove();
			$item.appendTo($list).fadeIn(function () {
				$item
					//.animate({ width: "48px" })
					.find("img");
				//.animate({ height: "36px" });
			});
		});
	}

	var moveBookVisualElementToGallery = function ($item) {
		$item.fadeOut(function () {
			$item
				.find("a.ui-icon-refresh")
				.remove()
				.end()
				.css("width", "96px")
				.find("img")
				.css("height", "72px")
				.end()
				.appendTo($("#gallery"))
				.fadeIn();
		});
	}
	
	var onGalleryDropping = function (event, ui) {
		var bookId = ui.draggable.attr('id');
		moveBookVisualElementToGallery(ui.draggable);
		sendDeleteBookToServer(bookId);
	}

	var onTrashDropping = function (event, ui) {
		var $book = ui.draggable;
		// Проверяем, что сумма перетаскиваемой книги итоговая сумма не больше максимальной
		var price = parseFloat($book.find(".book-price-amount").text());
		var currentTotal = parseFloat($totalSum.text());
		var newTotal = currentTotal + price;

		if (newTotal > maxTotalSum) {
			self.showAlert("Сумма заказа " + newTotal + " превышает допустимую " + maxTotalSum);
			return;
		}

		var bookId = $book.attr('id');
		moveBookVisualElementToTrash($book);
		sendAddBookToServer(bookId);
	}

	this.showAlert = function (msg) {
		$alertmsg.text(msg).fadeIn("slow").fadeOut(10000);
	}

	this.addBookCompleted = function (bookId, totalSum, restAmount, errorMsg) {
		var $book = $("#" + bookId);
		if (errorMsg) {
			moveBookVisualElementToGallery($book);

			if (errorMsg === "Ошибочный промокод")
				document.getElementById('logoutForm').submit();
			else
				self.showAlert(errorMsg);
		} else {
			if (bookId == 1)
				$('#live-counter').val(restAmount);

			//обновляем инфу на книжке
			var $bookAmountOrderedElement = $("#" + bookId + " .amount-ordered");
			$bookAmountOrderedElement.text(restAmount);

			//обновляем сумму заказа
			$totalSum.text(totalSum);
		}
	};

	this.refreshBookAmountForAll = function (bookId, restAmount, errorMsg) {
		var $book = $("#" + bookId);
		if (errorMsg) {
			self.showAlert(errorMsg);
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

	this.Init = function (pSendAddBookToServer, pSendDeleteBookToServer) {

		sendAddBookToServer = pSendAddBookToServer;
		sendDeleteBookToServer = pSendDeleteBookToServer;

		// let the gallery items be draggable
		$("li", $gallery).draggable({
			cancel: "a.ui-icon", // clicking an icon won't initiate dragging
			revert: "invalid", // when not dropped, the item will revert back to its initial position
			containment: "document",
			helper: "clone",
			cursor: "move"
		});

		$("li", $trash).draggable({
			cancel: "a.ui-icon", // clicking an icon won't initiate dragging
			revert: "invalid", // when not dropped, the item will revert back to its initial position
			containment: "document",
			helper: "clone",
			cursor: "move"
		});

		

		$trash.droppable({
			accept: "#gallery > li",
			activeClass: "ui-state-highlight",
			drop: onTrashDropping
		});

		

		$gallery.droppable({
			accept: "#trash li",
			activeClass: "custom-state-active",
			drop: onGalleryDropping
		});

		// Блокируем все книжки при первой отрисовке которые нельзя заказать 
		$("#gallery li").each(function (index) {
			if ($(this).find(".amount-ordered").text() == 0) {
				$(this).draggable("disable");
			}
		});


		// resolve the icons behavior with event delegation
		$("ul.gallery > li").click(function (event) {
			var $item = $(this),
			    $target = $(event.target);

			if ($target.is("a.ui-icon-trash")) {
				moveBookVisualElementToTrash($item);			
			} else if ($target.is("a.ui-icon-refresh")) {
				moveBookVisualElementToGallery($item);
			}

			return false;
		});		
	}
}