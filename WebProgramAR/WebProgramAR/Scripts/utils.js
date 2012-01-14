$.ajaxSetup({ cache: false });
var position = 'center';

function SetFiltroOpenClose() {
    $(document).ready(function () {
        $('.filtro .header').click(function () {
            $('.filtro .content').toggle('slow');
            if ($('.filtro .header').text() == '(+)')
                $('.filtro .header').text('(-)');
            else
                $('.filtro .header').text('(+)');

            return false;
        });
    });
}

SetFiltroOpenClose();

function GetPosition() {
    return position;
}

$(document).ready(function () {

    $('#dialog').dialog({
        autoOpen: false,
        draggable: true,
        resizable: false,
        position: GetPosition(),
        title: '',
        modal: true,
        overlay: { opacity: 1.5, background: "black" },
        show: "blind",
        hide: "blind"
            , error: function (msg) { alert(msg); }
    });

    $('#subDialog').dialog({
        autoOpen: false,
        draggable: true,
        resizable: false,
        position: 'top',
        title: '',
        modal: true,
        overlay: { opacity: 1.5, background: "black" },
        show: "blind",
        hide: "blind"
            , error: function (msg) { alert(msg); }
    });
});

function OpenDialog(event, id, accion, ancho, alto) {
    if (event != null) {
        event.preventDefault();
    }

    // obtenemos ancho y alto de la ventana del explorer
    var wscr = $(window).width();
    var hscr = $(window).height();

    if (alto > 400) {
        position = 'top';
    } else {
        position = 'center';
    }

    if (alto > 0) {
        $("#dialog").dialog("option", "height", alto);
    }
    else {
        $("#dialog").dialog("option", "height", 'auto');
    }

    if (ancho > 0) {
        $("#dialog").dialog("option", "width", ancho);
    }
    else {
        $("#dialog").dialog("option", "height", 'auto');
    }

    $("#dialog").unbind("dialogopen");
    $("#dialog").bind("dialogopen", function (event, ui) {
        $(this).empty().html('<img src="../../Content/images/ajax-loader.gif" style="float:center" />');
        $(this).load(accion + id, function () {
            //alert('Load was performed.');

        });
    });
    $( "#dialog" ).dialog({
       beforeClose: function(event, ui) { $("#disablingDiv").fadeOut("slow"); }
    });
    $("#disablingDiv").fadeIn("slow");
    
    $('#dialog').dialog('open');
}

function OpenDialogConParametrosOpcionales(event, id, accion, ancho, alto) {

    if (event != null) {
        event.preventDefault();
    }

    // obtenemos ancho y alto de la ventana del explorer
    var wscr = $(window).width();
    var hscr = $(window).height();

    if (alto > 400) {
        position = 'top';
    } else {
        position = 'center';
    }

    if (alto > 0) {
        $("#dialog").dialog("option", "height", alto);
    }
    else {
        $("#dialog").dialog("option", "height", 'auto');
    }

    if (ancho > 0) {
        $("#dialog").dialog("option", "width", ancho);
    }
    else {
        $("#dialog").dialog("option", "height", 'auto');
    }
    var urlBienFormada = accion.replace(/&amp;/g, "&");
    //urlBienFormada = urlBienFormada.replace("?", "/" + id + "?");
    urlBienFormada = urlBienFormada.replace("XXX", id );
    urlBienFormada = urlBienFormada.substring(0, urlBienFormada.length - 1)

    $("#dialog").unbind("dialogopen");
    $("#dialog").bind("dialogopen", function (event, ui) {
        $(this).empty().html('<img src="~/Content/images/ajax-loader.gif" style="float:center" />');
        $(this).load(urlBienFormada, function () {
            //alert('Load was performed.');

        });
    });
    $('#dialog').dialog('open');
}

function CerrarDialog() {
    $('#dialog').dialog("close");
}


$(document).ready(function () {
    $('#subDialog').dialog({
        autoOpen: false,
        draggable: true,
        resizable: false,
        position: 'top',
        title: '',
        modal: true,
        overlay: { opacity: 1.5, background: "black" },
        show: "blind",
        hide: "blind"
            , error: function (msg) { alert(msg); }
    });
});

function OpenSubDialog(event, id, accion, ancho, alto) {

    event.preventDefault();

    if (alto > 0) {
        $("#subDialog").dialog("option", "height", alto);
    }
    else {
        $("#subDialog").dialog("option", "height", 'auto');
    }

    if (ancho > 0) {
        $("#subDialog").dialog("option", "width", ancho);
    }
    else {
        $("#subDialog").dialog("option", "height", 'auto');
    }

    $("#subDialog").unbind("dialogopen");
    $("#subDialog").bind("dialogopen", function (event, ui) {
        $(this).empty().html('<img src="~/Content/images/ajax-loader.gif" style="float:center" />');
        if (id == null) {
            var url = accion;            
        }
        else {
            var url = accion + id;
        }

        $(this).load(url, function () {
            //alert('Load was performed.');

        });
    });
    $('#subDialog').dialog('open');
}

function CerrarSubDialog() {
    $('#subDialog').dialog("close");
}

function FuncionesAutorizadas(funciones) {
    var funcion = funciones.split(',');

    $('#btnNuevo').attr("style", "visibility:hidden;");
    $('.botonEditar').attr("style", "visibility:hidden;");
    $('.botonEliminar').attr("style", "visibility:hidden;");
    $('.botonChangePassword').attr("style", "visibility:hidden;");
    $('.botonVerObjetivos').attr("style", "visibility:hidden;");


    for (i = 0; i < funcion.length; i++) {
        switch (funcion[i]) {
            case "1":
                $('#btnNuevo').attr("style", "visibility:visible;");
                break;
            case "2":
                $('.botonEditar').attr("style", "visibility:visible;");
                break;
            case "3":
                $('.botonEliminar').attr("style", "visibility:visible;");
                break;
            case "4":
                $('.botonChangePassword').attr("style", "visibility:visible;");
            case "5":
                $('.botonVerObjetivos').attr("style", "visibility:visible;");
        }
    }
}




(function ($) {
    $.widget("ui.combobox", {
        _create: function () {
            var self = this,
					select = this.element.hide(),
					selected = select.children(":selected"),
					value = selected.val() ? selected.text() : "";
            var input = this.input = $("<input>")
					.insertAfter(select)
					.val(value)
					.autocomplete({
					    delay: 0,
					    minLength: 0,
					    source: function (request, response) {
					        var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
					        response(select.children("option").map(function () {
					            var text = $(this).text();
					            if (this.value && (!request.term || matcher.test(text)))
					                return {
					                    label: text.replace(
											new RegExp(
												"(?![^&;]+;)(?!<[^<>]*)(" +
												$.ui.autocomplete.escapeRegex(request.term) +
												")(?![^<>]*>)(?![^&;]+;)", "gi"
											), "<strong>$1</strong>"),
					                    value: text,
					                    option: this
					                };
					        }));
					    },
					    select: function (event, ui) {
					        ui.item.option.selected = true;
					        self._trigger("selected", event, {
					            item: ui.item.option
					        });
					    },
					    change: function (event, ui) {
					        if (!ui.item) {
					            var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex($(this).val()) + "$", "i"),
									valid = false;
					            select.children("option").each(function () {
					                if ($(this).text().match(matcher)) {
					                    this.selected = valid = true;
                                        alert('aaa');
					                    return false;
					                }
					            });
					            if (!valid) {
					                // remove invalid value, as it didn't match anything
					                $(this).val("");
					                select.val("");
					                input.data("autocomplete").term = "";
					                //return false;
					            }
					        }
					    }
					})
					.addClass("ui-widget ui-widget-content ui-corner-left");

            input.data("autocomplete")._renderItem = function (ul, item) {
                return $("<li></li>")
						.data("item.autocomplete", item)
						.append("<a>" + item.label + "</a>")
						.appendTo(ul);
            };

            this.button = $("<button type='button'>&nbsp;</button>")
					.attr("tabIndex", -1)
					.attr("title", "Mostrar Todos los Items")
					.insertAfter(input)
					.button({
					    icons: {
					        primary: "ui-icon-triangle-1-s"
					    },
					    text: false
					})
					.removeClass("ui-corner-all")
					//.addClass("ui-corner-right ui-button-icon")
                    .css("height", "21px")
                    .css("width", "20px")
                    .css("valign", "baseline")
					.click(function () {
					    // close if already visible
					    if (input.autocomplete("widget").is(":visible")) {
					        input.autocomplete("X");
					        return;
					    }

					    // work around a bug (likely same cause as #5265)
					    $(this).blur();

					    // pass empty string as value to search for, displaying all results
					    input.autocomplete("search", "");
					    input.focus();
					});
        },

        destroy: function () {
            this.input.remove();
            this.button.remove();
            this.element.show();
            $.Widget.prototype.destroy.call(this);
            $("#disablingDiv").fadeOut("slow");
        }
    });
})(jQuery);
