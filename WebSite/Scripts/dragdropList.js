function DragdropList() {

	var self = this;

	this.Init = function (addBook, deleteBook) {

		// there's the gallery and the trash
		var $gallery = $("#gallery"),
		    $trash = $("#trash");

		var trashIcon = "<a href='link/to/trash/script/when/we/have/js/off' title='Delete this image' class='ui-icon ui-icon-trash'>Delete image</a>"; // image preview function, demonstrating the ui.dialog used as a modal window
		var recycleIcon = "<a href='link/to/recycle/script/when/we/have/js/off' title='Recycle this image' class='ui-icon ui-icon-refresh'>Recycle image</a>"; // image recycle function



		this.deleteImage = function ($item) {
			$item.fadeOut(function () {
				var $list = $("ul", $("#trash")).length ?
					$("ul", $("#trash")) :
					$("<ul class='gallery ui-helper-reset'/>").appendTo($("#trash"));

				$item.find("a.ui-icon-trash").remove();
				$item.append(recycleIcon).appendTo($list).fadeIn(function () {
					$item
						//.animate({ width: "48px" })
						.find("img")
					//.animate({ height: "36px" });
				});
			});
		}

		this.recycleImage = function ($item) {
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

		$trash.droppable({
			accept: "#gallery > li",
			activeClass: "ui-state-highlight",
			drop: function (event, ui) {
				var bookId = ui.draggable.attr('id');
				self.deleteImage(ui.draggable);
				addBook(bookId);
			}
		});

		$gallery.droppable({
			accept: "#trash li",
			activeClass: "custom-state-active",
			drop: function (event, ui) {
				var bookId = ui.draggable.attr('id');
				self.recycleImage(ui.draggable);
				deleteBook(bookId);
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
				self.deleteImage($item);
			} else if ($target.is("a.ui-icon-zoomin")) {
				self.viewLargerImage($target);
			} else if ($target.is("a.ui-icon-refresh")) {
				self.recycleImage($item);
			}

			return false;
		});
	}
}