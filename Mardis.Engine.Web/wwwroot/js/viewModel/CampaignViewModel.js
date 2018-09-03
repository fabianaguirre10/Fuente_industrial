var modelView = new MainCampaignViewModel();

guidEmpty = "00000000-0000-0000-0000-000000000000";

function MainCampaignViewModel() {
    var self = this;
    //Esto es para que tenga filtros las pantallas de index
    self.FilterViewModel = new FilterViewModel();
    //Esto es para el mantenimiento de las campañas
    self.RegisterCampaign = ko.observable();
    //Clientes
    self.Customers = ko.observableArray([]);
    //Estados de Campaña
    self.StatusCampaign = ko.observableArray([]);
    //Esto es para Cascading de Servicios
    self.Services = ko.observableArray([]);

    //Esto es para Cascading de Canales
    self.Channels = ko.observableArray([]);
    //Esto es para Cascading de Supervisores
    self.Supervisors = ko.observableArray([]);

    //lista de Servicios agregados
    self.itemServices = ko.observableArray([]);
    //lista de borrados
    self.campaignsDeleted = ko.observableArray();

    //Esto es para recuperar canales
    self.IdChannel = ko.observable();

    //lista de borrados
    self.itemDelete = ko.observableArray();

    self.CampaignTasks = ko.observableArray();

    self.EditCampaignClick = function (id) {

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

    };
}

function NewCampaignClickEvent() {
    window.LoadPage("/Campaign/Register");
}

function LoadCampaignById(idCampaign) {
    $.blockUI({ message: '' });
    $.ajax({
        type: "GET",
        url: "/Campaign/GetCampaignById",
        async: false,
        data: {
            idCampaign: idCampaign
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            ApplyBindingRegisterCampaign(data);
            LoadServicesBinding();
        },
        error: function () {
            alert("error");
        }
    });
}

function LoadServicesBinding() {
    if (modelView.RegisterCampaign.IdCustomer !== guidEmpty && modelView.RegisterCampaign.IdCustomer) {
        GetAllChannelsByCustomer(modelView.RegisterCampaign.IdCustomer, function (d) {
            modelView.Channels(d);
            modelView.RegisterCampaign.IdChannel = modelView.IdChannel;
            $("#ddlChannel").val(modelView.IdChannel);

            GetServicesByChannelId(modelView.RegisterCampaign.IdChannel, function (d) {

                modelView.Services(d);
            });
        });
    }
}

function ApplyBindingRegisterCampaign(data) {
    modelView.RegisterCampaign = data;
    modelView.IdChannel = modelView.RegisterCampaign.IdChannel;
    if (data.CampaignServices) {
        modelView.itemServices = ko.observableArray();
        $("#lstServices").html('');

        for (var i = 0; i < data.CampaignServices.length; i++) {
            var tempService = data.CampaignServices[i];
            var idButton = "srv" + window.countServices++;
            $("#lstServices").append(getButtonService(tempService.Service.Name, tempService.Service.Id));
            var element = new ListServicesTemporary(idButton, tempService.Service.Name, "BDD", tempService.Service.Id);
            modelView.itemServices.push(element);
        }
    }

    ko.cleanNode(document.getElementById("divRegisterCampaign"));
    ko.applyBindings(modelView, document.getElementById("divRegisterCampaign"));
    $.unblockUI();
}

function CloseCampaign() {
    LoadPage("/Campaign");
}

function AddService() {

    var valService = $("#ddlService>option:selected").html();
    var idService = $("#ddlService").val();
    var idButton = "srv" + window.countServices++;

    if (null != valService && "" !== valService.trim()) {
        var element = new ListServicesTemporary(idButton, valService, "NEW", idService);

        var exists = false;
        modelView.itemServices().forEach(function (item, index) {

            if (item.IdService() === idService) {
                exists = true;
            }
        });
        if (exists) {
            bootbox.alert("Ya se ha agregado ese servicio antes...");
            return;
        }

        modelView.itemServices.push(element);
        $("#lstServices").append(getButtonService(valService, idButton));
        $("#ddlService").val("");
    }
}

function ListServicesTemporary(id, name, action, idService) {
    self = this;
    self.Id = ko.observable(id);
    self.Name = ko.observable(name);
    self.Action = ko.observable(action);
    self.IdService = ko.observable(idService);
}

function getButtonService(label, idButton) {

    var returnHtmlFilter = "";

    returnHtmlFilter += "<div class='row'><div class='col-sm-8'>";
    returnHtmlFilter += "<a class='btn btn-default' href='#' ";
    returnHtmlFilter += " id='" + idButton + "'";
    returnHtmlFilter += " onclick=DeleteService('" + idButton + "') ";
    returnHtmlFilter += ">";
    returnHtmlFilter += label;
    returnHtmlFilter += "<i class='clip-close-2'></i>";
    returnHtmlFilter += "</a>";
    returnHtmlFilter += "</div></div>";

    return returnHtmlFilter;
}

function DeleteService(idButton) {
    var button = $("#" + idButton);
    if (!button.is(':empty')) {
        ko.utils.arrayForEach(modelView.itemServices(), function (oneService) {
            modelView.itemServices.remove(oneService);
            button.remove();
        });
    }
}

function InitializeDataNewCampaign() {
    GetAllCurrentCustomers(function (d) {
        modelView.Customers(d);
    });
    GetStatusCampaigns(function (d) {
        modelView.StatusCampaign(d);
    });
    GetSupervisorList(function (d) {
        modelView.Supervisors(d);
    });
}

function LoadChannels() {

    if (modelView.RegisterCampaign.Id !== guidEmpty) {
        return;
    }
    var customerCode = $("#ddlCustomer").val();
    if (!customerCode) {
        return;
    }

    GetAllChannelsByCustomer(customerCode, function (d) {
        modelView.Channels(d);
    });
}

function LoadChannelsByCustomerId(idCustomer) {
    GetAllChannelsByCustomer(idCustomer, function (d) {
        modelView.Channels(d);
    });
}

function LoadServices() {
    if (modelView.RegisterCampaign.Id !== guidEmpty) {
        return;
    }
    var channelId = $("#ddlChannel").val();
    if (!channelId) {
        return;
    }
    GetServicesByChannelId(channelId, function (d) {
        modelView.Services(d);
    });
}

function SaveCampaign() {

    var isValid = ValidateCampaign();

    showError("divError", isValid);

    if (isValid) {
        $.blockUI({ message: '' });

        var startDate = GetFormattedDate($("#txtStartDate").val());
        var rangetDate = GetFormattedDate($("#txtRangeDate").val());
        var endDate = GetFormattedDate($("#txtEndDate").val());

        modelView.RegisterCampaign.StartDate = startDate;
        modelView.RegisterCampaign.EndDate = endDate;
        modelView.RegisterCampaign.RangeDate = rangetDate;

        $.ajax({
            url: "/Campaign/SaveCampaign",
            type: "post",
            data: {
                campaign: modelView.RegisterCampaign,
                inputServices: ko.toJSON(modelView.itemServices)
            },
            success: function (data) {

                if (null != data && "" !== data) {
                    var desData = $.parseJSON(data);
                    ApplyBindingRegisterCampaign(desData);
                    bootbox.alert("Registros Actualizados Satisfactoriamente");
                } else {
                    bootbox.alert("Existío un error, Vuelva a intentarlo");
                }

            },
            complete: function (data) {
                $.unblockUI();
            },
            error: function (xhr) {
                //Do Something to handle error
            }
        });
    }


}

function DeleteCampaign() {
    bootbox.confirm("Esta seguro que desea eliminar?", function (result) {
        if (result) {
            $.ajax({
                url: "/Campaign/Delete",
                type: "post",
                data: {
                    input: ko.toJSON(modelView.itemDelete)
                },
                success: function (data) {
                    if (data) {

                        ko.utils.arrayForEach(modelView.itemDelete(), function (itemDeleteTemp) {

                            var rowId = "row" + itemDeleteTemp;
                            $("#" + rowId).remove();
                        });

                        modelView.itemDelete = ko.observableArray();
                    } else {
                        bootbox.alert("Existío un error, Vuelva a intentarlo");
                    }
                },
                error: function (xhr) {
                    //Do Something to handle error
                }
            });
        }
    });
}

function Geoposition() {
    $.blockUI({ message: '' });
    debugger;
    if (modelView.itemDelete().length !== 1) {
        bootbox.alert("¡Debe seleccionar únicamente una campaña!");
        $.unblockUI();
        return;
    }
    $.ajax({
        url: "/Campaign/Geoposition",
        type: "GET",
        async: false,
        data: {
            idCampaign : modelView.itemDelete()[0]
        },
        success:function(viewHtml) {
            $("#divMain").html(viewHtml);
            $.unblockUI();
        }
    });
}

//#region Validacion de Datos

function ValidateCampaign() {
    var jsonRegister = ko.toJS(ko.mapping.toJS(modelView.RegisterCampaign));
    var isValid = true;

    if (!isValidField("divGStatusCampaign", jsonRegister.IdStatusCampaign)) {
        isValid = false;
    }

    if (!isValidField("divGClient", jsonRegister.IdCustomer)) {
        isValid = false;
    }

    if (!isValidField("divGChannel", jsonRegister.IdChannel)) {
        isValid = false;
    }

    if (!isValidField("divGStartDate", jsonRegister.StartDate)) {
        isValid = false;
    }

    if (!isValidField("divGEndDate", jsonRegister.EndDate)) {
        isValid = false;
    }

    if (!isValidField("divGRangeDate", jsonRegister.RangeDate)) {
        isValid = false;
    }

    if (!isValidField("divGSupervisor", jsonRegister.IdSupervisor)) {
        isValid = false;
    }

    if (!isValidField("divGComment", jsonRegister.Comment)) {
        isValid = false;
    }

    return isValid;
}

//#endregion

function GetCampaignDetails(data) {
    
    var resultData = [];

    data.forEach(function (item) {
        GetCampaignTaskDetails(item.Id, function (taskDetails) {
            
            taskDetails.CountImplementedTasks = Math.round((taskDetails.CountImplementedTasks * 100) / taskDetails.CountTotalTasks) + "%";
            taskDetails.CountStartedTasks = Math.round((taskDetails.CountStartedTasks * 100) / taskDetails.CountTotalTasks) + "%";
            taskDetails.CountPendingTasks = Math.round((taskDetails.CountPendingTasks * 100) / taskDetails.CountTotalTasks) + "%";
            taskDetails.CountNotImplementedTasks = Math.round((taskDetails.CountNotImplementedTasks * 100) / taskDetails.CountTotalTasks) + "%";

            item.TaskDetails = taskDetails;
        });
        resultData.push(item);
    });

    return resultData;
}

function PushCampaignTasks(data) {
    data.forEach(function (item) {
        modelView.CampaignTasks.push(item);
    });
}