var modelView = new MainDashBoardIewModel();

function MainDashBoardIewModel() {
    var self = this;

    self.CampaignList = ko.observableArray();
    self.SelectedCampaignId = ko.observable();
    self.SelectedCampaign = ko.observable();
    self.CampaignDetails = ko.observable();
    self.MerchantList = ko.observableArray([]);
    self.BranchList = ko.observableArray([]);

}

function PushCampaignList(campaignList) {
    campaignList.forEach(function (item) {
        modelView.CampaignList.push(item);
    });
}

function EditCampaignClick(id) {

    $.ajax({
        url: "/Campaign/Register",
        type: "get",
        data: {
            idCampaign: id
        },
        success: function (viewHtml) {
            $("#divMain").html(viewHtml);
        },
        error: function () {
        }
    });

}

function GetCampaignDetails(idCampaign) {

    GetCampaignTaskDetails(idCampaign, function (taskDetails) {
        taskDetails.CountImplementedTasks = Math.round((taskDetails.CountImplementedTasks * 100) / taskDetails.CountTotalTasks) + "%";
        taskDetails.CountStartedTasks = Math.round((taskDetails.CountStartedTasks * 100) / taskDetails.CountTotalTasks) + "%";
        taskDetails.CountPendingTasks = Math.round((taskDetails.CountPendingTasks * 100) / taskDetails.CountTotalTasks) + "%";
        taskDetails.CountNotImplementedTasks = Math.round((taskDetails.CountNotImplementedTasks * 100) / taskDetails.CountTotalTasks) + "%";

        modelView.CampaignDetails = taskDetails;

    });
}

function GetMerchantsByCampaign(idCampaign) {
    $.ajax({
        url: "/User/GetMerchantsByCampaign",
        type: "get",
        async: false,
        data: {
            idCampaign: idCampaign
        },
        success: function (data) {
            var result = $.parseJSON(data);
            PushMerchantList(result);
        },
        error: function () {
        }
    });
}


function PushMerchantList(data) {

    var result = [];
    data.forEach(function (item) {
        GetCampaignTaskDetailsByMerchant(modelView.SelectedCampaign.Id, item.Id,
            function (taskDetails) {

                taskDetails.CountImplementedTasks = Math.round((taskDetails.CountImplementedTasks * 100) / taskDetails.CountTotalTasks) + "%";
                taskDetails.CountStartedTasks = Math.round((taskDetails.CountStartedTasks * 100) / taskDetails.CountTotalTasks) + "%";
                taskDetails.CountPendingTasks = Math.round((taskDetails.CountPendingTasks * 100) / taskDetails.CountTotalTasks) + "%";
                taskDetails.CountNotImplementedTasks = Math.round((taskDetails.CountNotImplementedTasks * 100) / taskDetails.CountTotalTasks) + "%";

                item.TaskDetails = taskDetails;
            });
        result.push(item);
    });

    modelView.MerchantList = result;
};

function GetBranchesByCampaign(idCampaign) {
    $.ajax({
        url: "/Branch/GetBranchesByCampaign",
        type: "get",
        async: false,
        data: {
            idCampaign: idCampaign
        },
        success: function (data) {
            var result = $.parseJSON(data);
            PushBranchesList(result);
        },
        error: function () {
        }
    });
}
function GetDashbord(idCampaign) {
    var url;
    $.ajax({
        url: "/Home/GetDashBord",
        type: "Post",
        async: false,
        data: {
            idCampaign: idCampaign
        },
        success: function (data) {
            url = data;
        
        },
        error: function () {
        }
    });
    return url;
}

function PushBranchesList(data) {

    modelView.BranchList = data;
}

function PopulateResultsInTable(data) {
    modelView.SelectedCampaign = data;
    ko.cleanNode(document.getElementById("divMainDashBoard"));
    ko.applyBindings(modelView, document.getElementById("divMainDashBoard"));

    $.unblockUI();
}

function SelCampaignsChange() {

    $.blockUI({ message: "" });

    var idSelectedCampaign = $("#selCampaigns").val();
    if (idSelectedCampaign) {
        $.ajax({
            url: "/Campaign/GetSimpleCampaignById",
            type: "GET",
            data: {
                idCampaign: idSelectedCampaign
            },
            async: false,
            success: function (data) {
                var register = $.parseJSON(data);
                modelView.SelectedCampaign = register;
                GetCampaignDetails(register.Id);
                GetMerchantsByCampaign(register.Id);
                GetBranchesByCampaign(register.Id);
                PopulateResultsInTable(register);
            },
            error: function (error) {
                alert(error);
            }
        });
    }

}