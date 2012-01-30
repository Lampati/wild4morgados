
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
        $(".buttonSectionOptions").show();
        updateWindow();

    } else {
        $("#update-message").show();
    }
}

    /********para el manejo de select tags********/
    // The select element to be replaced:
function convertSelect(id,selectFirstElement) {
        var select = $('#' + id);
        var selectBoxContainer = $('<div>', {
            width: select.outerWidth(),
            className: 'tzSelect',
            html: '<div id="selectBox_'+id+'" class="selectBox" rel=""></div>'
        });
        var dropDown = $('<ul>', { className: 'dropDown' });
        var selectBox = selectBoxContainer.find('.selectBox');
        if (select.find('option').size() > 5) {
            dropDown.css("height", "150px");
        } else {
            dropDown.css("height", "30px" * select.find('option').size());
        }

        select.find('option').each(function (i) {
            var option = $(this);
            if ((i) == select.attr('selectedIndex')) {
                if (i == 0 && selectFirstElement != undefined)
                { selectBox.html(option.text()); }
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
        select.hide().after(selectBoxContainer);
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
    