function alertSize() {
    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }
    if (myHeight < 660)
        $("body").attr("style", "overflow-y:scroll;");
    else
        $("body").attr("style", "overflow-y:hidden;");
    //window.alert('Width = ' + myWidth);
    //window.alert('Height = ' + myHeight);
}
/*para muestra de operacion satisfactoria u operacion no satisfactoria*/
function disableButtons() {
    $(".buttonSectionOptions").hide();
}
function updateWindow() {
    $("#divOperacionExitosa").delay(300).fadeOut(500);
    setTimeout("updateWin()", 3000);
}
function updateWin() {
    window.location.reload();
}

function updateSuccess() {
    if ($("#update-message").html() == "True") {
        $("#divOperacionExitosa").fadeIn(300);
        //$(".buttonSectionOptions").show();
        updateWindow();
    } else {
        $(".update-message-box").show();
        $(".buttonSectionOptions").show();
        $("#dialog").animate({ height: "auto" });
        $("#subDialog").animate({ height: "auto" });
        
    }
}
function updateSuccessView() {
    if ($("#update-message").html().search("Error") == "-1") {
        $("#update-message").hide();
        window.location.href = $("#update-message").html();
    }else{
        $(".update-message-box").show();
        $(".buttonSectionOptions").show();
    } 
}
function updateSuccessPopUp() {
    if ($("#update-message-popUp").html() == "True") {
        $("#update-message-popUp").hide();
        $("#divOperacionExitosaPopUp").fadeIn(300);
        //$(".buttonSectionOptions").show();
        updateWindow();
    } else {
        $(".update-message-box-popUp").show();
        $(".buttonSectionOptions").show();
        $("#dialog").animate({ height: "auto" });
        $("#subDialog").animate({ height: "auto" });

    }
}
    /********para el manejo de select tags********/
    // The select element to be replaced:
function convertSelect(id, selectFirstElement) {
/*
selectFirstElement
1: si debe aparecer como seleccionado el primer elemento(ej: Todos como value al iniciar)
undefined: no darle importancia
*/
        var select = $('#' + id);
        var selectBoxContainer = $('<div>', {
            width: select.outerWidth(),
            className: 'tzSelect',
            html: '<div id="selectBox_'+id+'" class="selectBox" rel=""></div>'
        });
        var dropDown = $('<ul>', { className: 'dropDown' });
        var selectBox = selectBoxContainer.find('.selectBox');
        if (select.find('option').size() == undefined) {
            dropDown.css("height", "150px");
        } else {
            if (select.find('option').size() > 5) {
                dropDown.css("height", "150px");
            } else {
                dropDown.css("height", "30px" * select.find('option').size());
            }
        }
        select.find('option').each(function (i) {
            var option = $(this);
            //alert(selectFirstElement + ":" + option.attr("selected")+":"+option.html());
            /*if ((option.attr("selected") == "selected" || option.attr("selected") == true) && !seleccionado) {
                selectBox.html(option.html());
                selectBox.attr("rel", option.val());
            }*/
            if (selectFirstElement != undefined) {
                if (i == 0) {
                    selectBox.html(option.html());
                    selectBox.attr("rel", option.val());
                }
            } else {
                if (option.attr("selected") == "selected" || option.attr("selected") == true) {
                    selectBox.html(option.html());
                    selectBox.attr("rel", option.val());
                }
            }
            if (option.data('skip')) {
                return true;
            }
            var li = $('<li rel="' + option.val() + '">' + option.html() + '</li>');
            li.click(function () {
                selectBox.html(option.html());
                dropDown.trigger('hide');
                selectBox.attr("rel", $(this).attr("rel"));
                select.attr("rel", $(this).attr("rel"));
                select.val(option.html());
                OnChangeDo(select);
            });

            dropDown.append(li);
        });
        selectBoxContainer.append(dropDown.hide());
        select.after(selectBoxContainer);
        dropDown.bind('show', function () {
            if (dropDown.is(':animated')) {
                return false;
            }
            selectBox.addClass('expanded');
            dropDown.slideDown();
        }).bind('hide', function () {
            if (dropDown.is(':animated')) {
                return false;
            }
            selectBox.removeClass('expanded');
            dropDown.slideUp();
        }).bind('toggle', function () {
            if (selectBox.hasClass('expanded')) {
                dropDown.trigger('hide');
            }
            else dropDown.trigger('show');
        });
        selectBox.click(function () {
            dropDown.trigger('toggle');
            return false;
        });
    }
    /*VALIDACIONES EXPRESIONES REGULARES*/
        function validateType(data,type){
            switch(type){
                case "mail": return regIsMail(data);break;
                case "integer":return regIsNumber(data);break;
                case "string":
                default:
                    return regIsString(data);
                break;
            }

        }

    // check is number
        function regIsNumber(fData){
            var reg = new RegExp("/^[0-9]$/");
            return reg.test(fData)
        }

        function regIsString(fData){
            var reg = new RegExp("/^[a-zA-Z]$/");
            return reg.test(fData)
        }
        function regIsMail(fData){
            var reg = new RegExp("/^[0-9a-zA-Z]+@[0-9a-zA-Z]+[\.]{1}[0-9a-zA-Z]+[\.]?[0-9a-zA-Z]+$/");
            return reg.test(fData)
        }