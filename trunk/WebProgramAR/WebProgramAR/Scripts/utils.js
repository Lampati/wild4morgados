$.ajaxSetup({ cache: false });
var position = 'top';
var initOpenTab = true;

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
//Dialogo login

function initDialogs() {
    $('#dialog').dialog({
        autoOpen: false,
        draggable: true,
        resizable: false,
        position: GetPosition(),
        title: '',
        modal: true,
        overlay: { opacity: 1.5, background: "black" },
        show: "slide",
        hide: "blind"
            , error: function (msg) { alert(msg); }
    });
    $('#dialogLogin').dialog({
        autoOpen: false,
        draggable: true,
        resizable: false,
        position: GetPosition(),
        title: '',
        modal: true,
        overlay: { opacity: 1.5, background: "black" },
        show: "slide",
        beforeClose: function (event, ui) { $("#disablingDiv").fadeOut("slow"); },
        hide: "blind"
            , error: function (msg) { alert(msg); }
    });

    $('#dialogRecoverPassword').dialog({
        autoOpen: false,
        draggable: true,
        resizable: false,
        position: GetPosition(),
        title: '',
        modal: true,
        overlay: { opacity: 1.5, background: "black" },
        show: "slide",
        beforeClose: function (event, ui) { $("#disablingDiv").fadeOut("slow"); },
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
        beforeClose: function (event, ui) { $("#disablingDiv").fadeOut("slow"); },
        hide: "blind"
            , error: function (msg) { alert(msg); }
    });
}

$(document).ready(function () {
    alertSize();
    initDialogs();
    /*PARA EL MANEJO DE MENU/SUBMENU*/
    $(".hasSubMenu").click(function () {
        var divId = $(this).attr("id");
        $(this).addClass("subMenuSelected");
        var left = ($(this).position().left) - 30;
        var top = ($(this).position().top) + 120;
        $("#sub" + divId).css("left", left);
        $("#sub" + divId).css("top", top);
        $("#sub" + divId).stop(true, true).fadeIn("slow");
    });
    $("#menu li").mouseover(function () {
        $(".submenu").each(function () {
            $(this).hide();
        });
        $("#menu li").each(function () {
            $(this).removeClass("subMenuSelected");
        });
    });

    $("ul .submenu").mouseout(function () {
        $(this).stop(true, true).fadeOut("slow");
    });

    /*^FIN MANEJO MENU SUBMENU*/
    $("#btnCancelar").live('click', function () {
        console.log("gil");
        CerrarDialog();
    });
    $("#btnCancelarSubDialog").live('click', function () {
        CerrarSubDialog();
    });


    $(".hoverRel").mousemove(function (event) {
        $("#overDiv").css({ 'top': event.pageY, 'left': event.pageX });
    });
    $(".hoverRel").mouseover(function (event) {
        $("#overDiv").html($(this).attr("alt"));
        activeDiv();
    }).mouseout(function () {
        nActiveDiv();
    });

});

function activeDiv() {
    $("#overDiv").show();
}
function nActiveDiv() {
    $("#overDiv").hide();
}
function OpenDialogLogin(event, id, accion, ancho, alto) {
    if (event != null) {
        event.preventDefault();
    }
    initDialogs();
    

    // obtenemos ancho y alto de la ventana del explorer
    var wscr = $(window).width();
    var hscr = $(window).height();

    if (alto > 400) {
        position = 'top';
    } else {
        position = 'center';
    }

    if (alto > 0) {
        $("#dialogLogin").dialog("option", "height", alto);
    }
    else {
        $("#dialogLogin").dialog("option", "height", 'auto');
    }

    if (ancho > 0) {
        $("#dialogLogin").dialog("option", "width", ancho);
    }
    else {
        $("#dialogLogin").dialog("option", "height", 'auto');
    }

    $("#dialogLogin").unbind("dialogopen");
    $("#dialogLogin").bind("dialogopen", function (event, ui) {
        $(this).empty().html('<img class="loadingGif" src="../../Content/images/ajax-loader.gif" style="position:absolute;left:40%;top:40%;" />');
        $(this).load(accion + id, function () {
            //alert('Load was performed.');

        });
    });
    $("#disablingDiv").fadeIn("slow");
    $("#dialogLogin").dialog({ beforeClose: function (event, ui) { $("#disablingDiv").fadeOut("slow"); } });
    $("#dialogLogin").dialog("open");
}

function CerrarDialogLogin() {
    $('#dialogLogin').dialog("close");
    $('#dialogLogin').dialog("destroy");
    initDialogs();
}


function OpenDialogRecoverPassword(event, id, accion, ancho, alto) {
    if (event != null) {
        event.preventDefault();
    }
    initDialogs();
    
    // obtenemos ancho y alto de la ventana del explorer
    var wscr = $(window).width();
    var hscr = $(window).height();

    if (alto > 400) {
        position = 'top';
    } else {
        position = 'center';
    }

    if (alto > 0) {
        $("#dialogRecoverPassword").dialog("option", "height", alto);
    }
    else {
        $("#dialogRecoverPassword").dialog("option", "height", 'auto');
    }

    if (ancho > 0) {
        $("#dialogRecoverPassword").dialog("option", "width", ancho);
    }
    else {
        $("#dialogRecoverPassword").dialog("option", "height", 'auto');
    }

    $("#dialogRecoverPassword").unbind("dialogopen");
    $("#dialogRecoverPassword").bind("dialogopen", function (event, ui) {
        $(this).empty().html('<img class="loadingGif" src="../../Content/images/ajax-loader.gif" style="position:absolute;left:40%;top:40%;" />');
        $(this).load(accion + id, function () {
            //alert('Load was performed.');

        });
    });
    $("#disablingDiv").fadeIn("slow");
    $("#dialogRecoverPassword").dialog({ beforeClose: function (event, ui) { $("#disablingDiv").fadeOut("slow"); } });
    $("#dialogRecoverPassword").dialog("open");
}

function CerrarDialogRecoverPassword() {
    $('#dialogRecoverPassword').dialog("close");
    $('#dialogRecoverPassword').dialog("destroy");
    initDialogs();
}

function OpenTab(event, id, accion,idTab) {
    if (event != null) {
        event.preventDefault();
    }
    if (initOpenTab) {
        //$("#"+idTab).unbind("tabOpen");
        //$("#" + idTab).bind("dialogopen", function (event, ui) {
        $("#" + idTab).empty().html('<img class="loadingGif" src="../../Content/images/ajax-loader.gif" style="position:absolute;left:40%;top:40%;" />').delay(300).html();
        initOpenTab = false;
        $("#" + idTab).stop(true, true).load(accion + id, function () {
            initOpenTab = true;
            $("#dialog").unbind("dialogopen");
            //alert('Load was performed.');
        });
        //});
    }
}
function OpenDialog(event, id, accion, ancho, alto) {
    if (event != null) {
        event.preventDefault();
    }
    initDialogs();
    

    // obtenemos ancho y alto de la ventana del explorer
    var wscr = $(window).width();
    var hscr = $(window).height();

    position = 'top';
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
        $("#dialog").dialog("option", "width", "auto");
    }
    $("#dialog").unbind("dialogopen");
    $("#dialog").bind("dialogopen", function (event, ui) {
        $(this).empty().html('<img class="loadingGif" src="../../Content/images/ajax-loader.gif" style="position:absolute;left:40%;top:40%;" />');
        $(this).load(accion + id, function () {
            //alert('Load was performed.');

        });
    });
    $("#disablingDiv").fadeIn("slow");
    $("#dialog").dialog({
        beforeClose: function (event, ui) {
            $("#disablingDiv").fadeOut("slow");
            $("#dialog").dialog("destroy");
        }
    });
    $("#dialog").dialog("open");

}

function OpenDialogConParametrosOpcionales(event, id, accion, ancho, alto) {

    if (event != null) {
        event.preventDefault();
    }

    // obtenemos ancho y alto de la ventana del explorer
    var wscr = $(window).width();
    var hscr = $(window).height();
    initDialogs();
    position = 'top';
    
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
    
    $("#dialog").dialog({ beforeClose: function (event, ui) { $("#disablingDiv").fadeOut("slow"); } });
    $("#dialog").dialog("open");
}

function CerrarDialog() {
    $('#dialog').dialog("close");
    $('#dialog').dialog("destroy");
    initDialogs();
}

function OpenSubDialog(event, id, accion, ancho, alto) {
    event.preventDefault();
    initDialogs();
    
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
    $("#subDialog").dialog("open");
    $("#subDialog").dialog({ beforeClose: function (event, ui) { $("#disablingDiv").fadeOut("slow"); } });
    
}

function CerrarSubDialog() {
    $('#subDialog').dialog("close");
    $('#subDialog').dialog("destroy");
    initDialogs();
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
