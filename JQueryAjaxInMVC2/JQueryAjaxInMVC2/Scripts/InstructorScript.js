//var InstructorInfo = {
//    FirstName: $("#FirstName").val(),
//    LastName: $("LastName").val(),
//    Username: $("Username").val(),
//    Password: $("Password").val(),
//    ClubName: $("ClubName").val(),
//    Email: $("Email").val(),
//    PhoneNumber: $("PhoneNumber").val(),
//    AddressLine1: $("AddressLine1").val(),
//    AddressLine2: $("AddressLine2").val(),
//    PostCode: $("Username").val()

//};

//function jQueryAjaxPostInstructor(InstructorInfo)
//{
//    $("#btnSave").click(function () {

//        $.validator.unobtrusive.parse(InstructorInfo);
//        if ($(InstructorInfo).valid()) {

//            var ajaxConfig = {
//                type: 'POST',
//                url: "Instructor/SaveInstructor",
//                data: InstructorInfo,
//                success: function (response) {
//                    if (response.success) {
//                        $("/Instructor#firstTab").html(response.html);
//                        //refreshAddNewInstructorTab($(form).attr('data-restUrl'), true);
//                        //success message 
//                        $.notify(response.message, "success");
//                        if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
//                            activatejQueryTable();
//                    }
//                    else {
//                        //error message 
//                        $.notify(response.message, "error");

//                    }
//                }
//            }
//            if ($(form).attr('enctype') == "multipart/form-data") {
//                ajaxConfig["contentType"] = false;
//                ajaxConfig["processData"] = false;
//            }
//            $.ajax(ajaxConfig);
//        }
       


//    });
//        return false;
   
//}


function jQueryAjaxPostInstructor(form)
{
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {

        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {
                    $("/Instructor#firstTab").html(response.html);
                    refreshAddNewInstructorTab($(form).attr('data-restUrl'), true);
                    //success message 
                    $.notify(response.message, "success");
                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                        activatejQueryTable();
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

function refreshAddNewInstructorTab(resetUrl, showViewTab) {
    $.ajax({
        type: 'GET',
        url: resetUrl,
        success: function (response) {
            $("/Instructor#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New Instructor');
            if (showViewTab)
                $('ul.nav.nav-tabs a:eq(0)').tab('show');

        }
    });

}
