function GetCampaignTaskDetails(idCampaign, callback) {
    var data;
    $.ajax({
        url: "/Campaign/GetCampaignTaskDetails",
        type: "get",
        async: false,
        data: {
            idCampaign: idCampaign
        },
        success: function (resp) {
            data = $.parseJSON(resp);
            return callback(data);
        },
        error: function (error) {
            //alert(error);
        }
    });
}

function GetCampaignTaskDetailsByMerchant(idCampaign, idMerchant, callback) {
    var data;
    $.ajax({
        url: "/Campaign/GetCampaignTaskDetailsByMerchant",
        type: "get",
        async: false,
        data: {
            idCampaign: idCampaign,
            idMerchant: idMerchant
        },
        success: function (resp) {
            data = $.parseJSON(resp);
            return callback(data);
        },
        error: function (error) {
            //alert(error);
        }
    });
}

function GetCampaignTasks(idCampaign) {
    modelView.CampaignTasks([]);
    $.ajax({
        url: "/Campaign/GetCampaignTasks",
        type: "get",
        async: false,
        data: {
            idCampaign: idCampaign
        },
        success: function (resp) {
            PushCampaignTasks($.parseJSON(resp));
            $("#divTasksCampaign").modal();
        },
        error: function (error) {
            //alert(error);
        }
    });
}

function GetTasksPerCampaign(idCampaign, pageSize, pageIndex, filterValues) {
    $.blockUI({ message: '' });

    if (!pageIndex) {
        pageIndex = 1;
    }

    if (!pageSize) {
        pageSize = 10;
    }

    modelView.CampaignTasks([]);
    $.ajax({
        url: "/Campaign/TasksPerCampaign",
        type: "get",
        async: false,
        data: {
            idCampaign: idCampaign,
            filterValues: ko.toJSON(filterValues),
            pageSize: pageSize,
            pageIndex: pageIndex
        },
        success: function (viewHtml) {
            $("#divMain").html(viewHtml);
            $.unblockUI();
        },
        error: function (error) {
            console.log(error);
            $.unblockUI();
        }
    });
}

function ShowProfile(idTask) {
    $.ajax({
        url: "/Task/Profile",
        type: "get",
        data: {
            idTask: idTask
        },
        success: function (viewHtml) {
            $("#divMain").html(viewHtml);
        },
        error: function () {
        }
    });
}

//function GetAnswerValueByQuestion(idQuestion, idTask, callback) {
//    var data;

//    $.ajax({
//        type: "GET",
//        url: "/Answer/GetAnswerValueByQuestion",
//        async: false,
//        data: {
//            idQuestion: idQuestion,
//            idTask: idTask
//        },
//        success: function (resp) {
//            data = resp;
//            callback(data);
//        },
//        error: function (error) {
//            //alert(error);
//        }
//    });
//}

function UploadBranchImage(jsonData, callback) {
    var data;

    $.ajax({
        type: "POST",
        url: "/Task/UploadBranchImage",
        contentType: false,
        processData: false,
        data: jsonData,
        success: function (resp) {
            data = resp;
            callback(data);
            //PushBranchImages($.parseJSON(data));
        },
        error: function () {
            alert("Hubo un error al cargar la imagen al servidor!");
        }
    });
}

function GetBranchImagesList(idBranch, callback) {
    var data;

    $.ajax({
        type: "GET",
        url: "/Task/GetBranchImagesList",
        data: { branchId: idBranch },
        success: function (resp) {
            data = resp;
            callback(data);
            //PushBranchImages(data);
        },
        error: function (error) {
            debugger;
            //alert(error);
        }
    });
}