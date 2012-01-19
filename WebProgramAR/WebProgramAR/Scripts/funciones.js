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
});

    /********para el manejo de select tags********/
    // The select element to be replaced:
function convertSelect(id) {
  
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
            if ((i + 1) == select.attr('selectedIndex')) {
                selectBox.html(option.text());
            }
            if (option.data('skip')) {
                return true;
            }
            var li = $('<li rel="' + option.val() + '">' + option.html() + '</li>');
            li.click(function () {
                selectBox.html(option.html());
                dropDown.trigger('hide');
                selectBox.attr("rel", $(this).val());
                select.val($(this).val());
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
    