var countServices = 0;

function AddService() {

    var valService = $("#ddlService>option:selected").html();
    var idService = $("#ddlService").val();

    var services = $("#txtNewService").val();

    if (null != valService && "" !== valService.trim()) {

        if (services.indexOf(idService) >= 0) {
            bootbox.alert("Ya se ha agregado ese servicio antes...");
            return;
        }

        $("#txtNewService").val(services + ";" + idService);
        $("#lstServices").append(getButtonService(valService, idService));
        $("#ddlService").val("");
    }
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
    var services = $("#txtDeletedService").val();
    if (!button.is(":empty")) {
        services += ";" + idButton;
        button.remove();
        $("#txtDeletedService").val(services);
    }
}

$("#ddlCustomer").change(function () {
    $.blockUI({ message: "" });
    var url = "/Channel/GetAllChannelsByCustomerId";
    var idCustomer = $("#ddlCustomer").val();
    $.getJSON(url, { idCustomer: idCustomer }, function (data) {
        var items = "<option>--Seleccionar--</option>";
        $("#ddlChannel").empty();
        $("#ddlService").html(items);
        $.each(data, function (i, channel) {
            items += "<option value='" + channel.Id + "'>" + channel.Name + "</option>";
        });
        $("#ddlChannel").html(items);
        $.unblockUI();
    });
});

$("#ddlChannel").change(function () {
    $.blockUI({ message: "" });
    var url = "/Service/GetServicesListByChannelId";
    var idChannel = $("#ddlChannel").val();
    $.getJSON(url, { idChannel: idChannel }, function (data) {
        var items = "<option>--Seleccionar--</option>";
        $("#ddlService").empty();
        $.each(data, function (i, service) {
            items += "<option value='" + service.Id + "'>" + service.Name + "</option>";
        });
        $("#ddlService").html(items);
        $.unblockUI();
    });
});

var itemsSelected = [];

function SelectItem(idCampaign) {
    itemsSelected.forEach(function (item, i) {
        if (item === idCampaign) {
            itemsSelected.splice(i, 1);
        }
    });
    itemsSelected.push(idCampaign);
}

function ShowGeoposition() {

    $.blockUI({ message: "" });
    if (itemsSelected.length !== 1) {
        bootbox.alert("Debe seleccionar únicamente una campaña...");
        $.unblockUI();
        return;
    }

    window.location.href = "/Campaign/Geoposition?campaign=" + itemsSelected[0];

    //debugger;
    //$.ajax({
    //    url: "/Campaign/Geoposition",
    //    type: "GET",
    //    async: false,
    //    data: {
    //        idCampaign: itemsSelected[0]
    //    },
    //    success: function (viewHtml) {
    //        debugger;
    //        $("#divMain").html(viewHtml);
    //    }
    //});
}

function DeleteCampaign() {
    bootbox.confirm("Esta seguro que desea eliminar?", function (result) {
        if (result) {
            $.ajax({
                url: "/Campaign/Delete",
                type: "post",
                data: {
                    input: JSON.stringify(itemsSelected)
                },
                success: function (data) {
                    if (data) {
                        window.location.href = "/Campaign/Index";
                    } else {
                        bootbox.alert("Existío un error, Vuelva a intentarlo");
                    }
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    });
}