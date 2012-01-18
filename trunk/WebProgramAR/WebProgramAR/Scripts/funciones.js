$(document).ready(function () {
    $('#logonButton').live('click', function (event) {

        var validator = $("#frmLogIn").validate({rules: {
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