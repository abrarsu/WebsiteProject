function jQueryAjaxPost(form)
{
    $.validator.unobtrusive.parse(form);
    if($(form).valid())
    {
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html);
                    refreshAddNewTab($(form).attr('data-restUrl'), true);
                    //success message 
                }
                else {
                    //error message 

                }
            }
        }
        if ($(form).attr('enctype') == "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig)
    }
    return false;
}

function refreshAddNewTab(resetUrl, showViewTab)
{
    $.ajax({
        type: 'GET',
        url: resetUrl,
        success: function (response) {
            $("#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New Club');
            if(showViewTab)        
                $('ul.nav.nav-tabs a:eq(0)').tab('show');
            
        }
    })

}