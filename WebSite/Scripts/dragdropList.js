function DragdropList() {

	var self = this;

	this.Init = function (sendAddBookToServer, sendDeleteBookToServer) {

		// there's the gallery and the trash
		var $gallery = $("#gallery"),
		    $trash = $("#trash"),
			maxTotalSum = parseFloat($("#maxTotalSum").val()),
		    $totalSum = $("#totalSum");

		var trashIcon = "<a href='link/to/trash/script/when/we/have/js/off' title='Delete this image' class='ui-icon ui-icon-trash'>Delete image</a>"; // image preview function, demonstrating the ui.dialog used as a modal window
		var recycleIcon = "<a href='link/to/recycle/script/when/we/have/js/off' title='Recycle this image' class='ui-icon ui-icon-refresh'>Recycle image</a>"; // image recycle function



		this.moveBookVisualElementToTrash = function ($item) {
			$item.fadeOut(function () {
				var $list = $("ul", $("#trash")).length ?
					$("ul", $("#trash")) :
					$("<ul class='gallery ui-helper-reset'/>").appendTo($("#trash"));

				$item.find("a.ui-icon-trash").remove();
				$item.append(recycleIcon).appendTo($list).fadeIn(function () {
					$item
						//.animate({ width: "48px" })
						.find("img");
					//.animate({ height: "36px" });
				});
			});
		}

		this.moveBookVisualElementToGallery = function ($item) {
			$item.fadeOut(function () {
				$item
					.find("a.ui-icon-refresh")
					.remove()
					.end()
					.css("width", "96px")
					.append(trashIcon)
					.find("img")
					.css("height", "72px")
					.end()
					.appendTo($("#gallery"))
					.fadeIn();
			});
		}

		function viewLargerImage($link) {
			var src = $link.attr("href"),
			    title = $link.siblings("img").attr("alt"),
			    $modal = $("img[src$='" + src + "']");

			if ($modal.length) {
				$modal.dialog("open");
			} else {
				var img = $("<img alt='" + title + "' width='384' height='288' style='display: none; padding: 8px;' />")
					.attr("src", src).appendTo("body");
				setTimeout(function () {
					img.dialog({
						title: title,
						width: 400,
						modal: true
					});
				}, 1);
			}
		}

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
			drop: function (event, ui) {
				var $book = ui.draggable;
				// Проверяем, что сумма перетаскиваемой книги итоговая сумма не больше максимальной
				var price = parseFloat($book.find(".book-price-amount").text());				
				var currentTotal = parseFloat($totalSum.text());			
				var newTotal = currentTotal + price;

				alert("Сумма заказа " + newTotal);
				if (newTotal > maxTotalSum) {
					alert("Сумма заказа " + newTotal + " превышает допустимую " + maxTotalSum);
					return;
				}

				var bookId = $book.attr('id');
				self.moveBookVisualElementToTrash($book);
				sendAddBookToServer(bookId);
			}
		});

		$gallery.droppable({
			accept: "#trash li",
			activeClass: "custom-state-active",
			drop: function (event, ui) {
				var bookId = ui.draggable.attr('id');
				self.moveBookVisualElementToGallery(ui.draggable);
				sendDeleteBookToServer(bookId);
			}
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
				self.moveBookVisualElementToTrash($item);
			} else if ($target.is("a.ui-icon-zoomin")) {
				self.viewLargerImage($target);
			} else if ($target.is("a.ui-icon-refresh")) {
				self.moveBookVisualElementToGallery($item);
			}

			return false;
		});

		this.addBookCompleted = function (bookId, restAmount, errorMsg) {
			var $book = $("#" + bookId);
			if (errorMsg) {
				self.moveBookVisualElementToGallery($book);

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

		this.refreshBookAmountForAll = function (bookId, restAmount, errorMsg) {
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
	}
}