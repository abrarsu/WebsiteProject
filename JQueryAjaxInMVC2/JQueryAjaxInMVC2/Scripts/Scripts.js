$(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});


//Post Club
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

//Refresh club tab
function refreshAddNewTab(resetUrl, showViewTab) {
    $.ajax({
        type: 'GET',
        url: resetUrl,
        success: function (response) {
            $("#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New Club');
            if (showViewTab)
                $('ul.nav.nav-tabs a:eq(0)').tab('show');

        }
    });

}

//Edit Club
function Edit(url) {

    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            $("#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Edit Club');
            $('ul.nav.nav-tabs a:eq(1)').tab('show');

        }
    });

}

//Delete Club
function Delete(url) {
    if (confirm('Are you sure you want to delete this record?') == true) {
        $.ajax({
            type: 'POST',
            url: url,
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html);
                    //success message 
                    $.notify(response.message, "warn");
                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                        activatejQueryTable();
                }
                else {
                    $.notify(response.message, "error");
                }
            }
        });

    }
}

////////////////////////////////////////////////////////

// Post Instructor
function jQueryAjaxPostInstructor(form) {

    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html);
                    refreshAddNewInstructorTab($(form).attr('data-restUrl'), true);
                    //success message 
                    $.notify(response.message, "success");
                    if (typeof activateInstructorjQueryTable !== 'undefined' && $.isFunction(activateInstructorjQueryTable))
                        activateInstructorjQueryTable();
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

//Refresh instructor tab
function refreshAddNewInstructorTab(resetUrl, showViewTab) {
    $.ajax({
        type: 'GET',
        url: resetUrl,
        success: function (response) {
            $("#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New Instructor');
            if (showViewTab)
                $('ul.nav.nav-tabs a:eq(0)').tab('show');

        }
    });

}


//Edit Instructor
function EditInstructor(url) {

    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            $("#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Edit Instructor');
            $('ul.nav.nav-tabs a:eq(1)').tab('show');

        }
    });

}


//Delete Instructor
function DeleteInstructor(url) {
    if (confirm('Are you sure you want to delete this record?') == true) {
        $.ajax({
            type: 'POST',
            url: url,
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html);
                    //success message 
                    $.notify(response.message, "warn");
                    if (typeof activateInstructorjQueryTable !== 'undefined' && $.isFunction(activateInstructorjQueryTable))
                        activateInstructorjQueryTable();
                }
                else {
                    $.notify(response.message, "error");
                }
            }
        });

    }
}

//////////////////////////////////////CLASS CRUD FUNCTIONALITY///////////////////////////////////

//Post Class
function jQueryAjaxPostClass(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html);
                    refreshAddNewClassTab($(form).attr('data-restUrl'), true);
                    //success message 
                    $.notify(response.message, "success");
                    if (typeof activateClassjQueryTable !== 'undefined' && $.isFunction(activateClassjQueryTable))
                        activateClassjQueryTable();
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


//Refresh class tab
function refreshAddNewClassTab(resetUrl, showViewTab) {
    $.ajax({
        type: 'GET',
        url: resetUrl,
        success: function (response) {
            $("#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New Class');
            if (showViewTab)
                $('ul.nav.nav-tabs a:eq(0)').tab('show');

        }
    });

}


//Edit Class
function EditClass(url) {

    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            $("#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Edit Class');
            $('ul.nav.nav-tabs a:eq(1)').tab('show');

        }
    });

}


//Delete Class
function DeleteClass(url) {
    if (confirm('Are you sure you want to delete this record?') == true) {
        $.ajax({
            type: 'POST',
            url: url,
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html);
                    //success message 
                    $.notify(response.message, "warn");
                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                        activatejQueryTable();
                }
                else {
                    $.notify(response.message, "error");
                }
            }
        });

    }
}


//////////////////////////////////////CUSTOMER CRUD FUNCTIONALITY///////////////////////////////////

//Post Customer
function jQueryAjaxPostCustomer(form)
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
                    //$("#firstTab").html(response.html);
                    //refreshAddNewTab($(form).attr('data-restUrl'), true);
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
