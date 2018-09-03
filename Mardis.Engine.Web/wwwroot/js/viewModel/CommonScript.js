var emptyGuid = "00000000-0000-0000-0000-000000000000";

function validationFormGroup(div, value) {
    var controlDiv = $("#" + div);
    var isValid = false;


    if (null != value && "" != value) {
        controlDiv.removeClass("form-group has-error");
        controlDiv.addClass("form-group");
        isValid = true;
    } else {
        controlDiv.remove("form-group");
        controlDiv.addClass("form-group has-error");
        isValid = false;
    }


    return isValid;
}

function showError(div, isValidForm) {

    var controlDiv = $("#" + div);


    if (isValidForm) {
        controlDiv.removeClass("errorHandler alert alert-danger");
        controlDiv.addClass("errorHandler alert alert-danger no-display");
    } else {
        controlDiv.removeClass("errorHandler alert alert-danger no-display");
        controlDiv.addClass("errorHandler alert alert-danger");
    }

}

//
function isValidField(div, value) {

    var controlDiv = $("#" + div);
    var returnValue = true;

    if (null != value && "" != value) {
        controlDiv.removeClass("has-error");
        returnValue = true;
    } else {
        controlDiv.addClass("has-error");
        returnValue = false;
    }

    return returnValue;
}


function getFilter(label, idFilterExecutionDetail) {

    var returnHtmlFilter = "";

    returnHtmlFilter += "<a class='btn btn-bricky' href='#' ";
    returnHtmlFilter += " id='" + idFilterExecutionDetail + "'";
    returnHtmlFilter += " onclick=DeleteFilter('" + idFilterExecutionDetail + "') ";
    returnHtmlFilter += ">";
    returnHtmlFilter += label;
    returnHtmlFilter += "<i class='clip-close-2'></i>";
    returnHtmlFilter += "</a>";

    return returnHtmlFilter;
}

//Obtener campañas mediante nombre de campaña
function GetCampaignByName(nameCampaign, callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/Campaign/GetCampaignByName",
        data: {
            nameCampaign: nameCampaign
        },
        success: function (resp) {
            data = resp;
            if (resp) {
                data = $.parseJSON(resp);
            }
            callback(data);
        },
        error: function (error) {
            alert("error");
        }
    });
}

//Obtener Tareas Pendientes
function GetPendingTasks(callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/Task/GetPendingTaskList",
        data: {
        },
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert(error);
        }
    });
}

//Obtener Tareas Iniciadas
function GetStartedTasks(callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/Task/GetStartedTaskList",
        data: {
        },
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert(error);
        }
    });
}

//Obtener Tareas Implementadas
function GetImplementedTasks(callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/Task/GetImplementedTaskList",
        data: {
        },
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert(error);
        }
    });
}

//Obtener Tareas NoImplementadas
function GetNotImplementedTasks(callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/Task/GetNotImplementedTaskList",
        data: {
        },
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert(error);
        }
    });
}

//Obtener Estados de Tareas
function GetAllStatusTask(callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/Task/GetAllStatusTask",
        data: {
        },
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert(error);
        }
    });
}

//Obtener Motivos de Tareas no implementadas
function GetAllTaskReasons(callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/Task/GetAllTaskNotImplementedReason",
        data: {
        },
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert(error);
        }
    });
}

//Obtener los productos filtrados por cliente
function GetProductListByCustomer(idCustomer, callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "Product/GetCustomerProductList",
        data: { idCustomer: idCustomer },
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert(error);
        }
    });
}

//Obtiene Fecha en formato año/mes/dia
function GetFormattedDate(date) {
    var todayTime = new Date(date);
    var month = todayTime.getMonth() + 1;
    var day = todayTime.getDate();
    var year = todayTime.getFullYear();
    return year + "/" + month + "/" + day;
}