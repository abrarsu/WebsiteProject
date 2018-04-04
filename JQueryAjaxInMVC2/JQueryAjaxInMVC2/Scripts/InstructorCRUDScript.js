function jQueryAjaxPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html);
                   // refreshAddNewTab($(form).attr('data-restUrl'), true);
                    //success message 
                    $.notify(response.message, "success");
                    //if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                    //    activatejQueryTable();
                }
                else {
                    //error message 
                    $.notify(response.message, "error");

                }
            }
        }
        if ($(form).attr('enctype') == "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);
    }
    return false;
}