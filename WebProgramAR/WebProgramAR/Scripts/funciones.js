$(document).ready(function () {
    $('#logonButton').live('click', function (event) {

        var validator = $("#frmLogIn").validate({ rules: {
            UserName: { required: true, maxlength: 64 },
            Password: { required: true, maxlength: 64 }
        }
        });
        if (validator.form() == true) {

            var ai = {
                UserName: $("#UserName").val(),
                Password: $("#Password").val(),
                RememberMe: $("#RememberMe").val()
            };


            $.ajax({
                url: '/Account/LogOn',
                type: "POST",
                dataType: "json",
                data: JSON.stringify(ai),
                contentType: 'application/json; charset=utf-8',
                success: function (data, textStatus) {
                    if (data.success == "true") {
                        $("#logonerror").hide();
                        getUserInformation();
                        $("#dialog").hide();
                        $("#disablingDiv").fadeOut("slow");
                    } else {
                        $("#logonerror").fadeIn("slow");
                    }
                },
                error: function (xhr, status, error) {
                    debugger;
                    var verr = xhr.status + "\r\n" + status + "\r\n" + error;
                    alert(verr);
                }

            });
        }
    });


    /********para el manejo de select tags********/
    // The select element to be replaced:
    var select = $('select');

    var selectBoxContainer = $('<div>', {
        width: select.outerWidth(),
        className: 'tzSelect',
        html: '<div class="selectBox"></div>'
    });

    var dropDown = $('<ul>', { className: 'dropDown' });
    var selectBox = selectBoxContainer.find('.selectBox');

    // Looping though the options of the original select element

    select.find('option').each(function (i) {
        var option = $(this);
        if ((i + 1) == select.attr('selectedIndex')) {
            selectBox.html(option.text());
        }

        // As of jQuery 1.4.3 we can access HTML5 
        // data attributes with the data() method.

        if (option.data('skip')) {
            return true;
        }

        // Creating a dropdown item according to the
        // data-icon and data-html-text HTML5 attributes:

        var li = $('<li rel="' + option.val() + '">' + option.html() + '</li>');

        li.click(function () {

            selectBox.html(option.html());
            dropDown.trigger('hide');

            // When a click occurs, we are also reflecting
            // the change on the original select element:
            select.val(option.val());
            return false;
        });

        dropDown.append(li);
    });

    selectBoxContainer.append(dropDown.hide());
    select.hide().after(selectBoxContainer);

    // Binding custom show and hide events on the dropDown:

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

});