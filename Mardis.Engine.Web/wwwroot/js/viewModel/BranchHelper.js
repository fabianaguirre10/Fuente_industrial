var emptyGuid = "00000000-0000-0000-0000-000000000000";
var vueVM;

function LoadCountries() {
    $.getJSON("/ServicesLocalization/GetAllCountries", {}, function (data) {
        var items = "<option>--Seleccionar--</option>";
        $("#selCountries").empty();
        $.each(data, function (i, country) {
            items += "<option value='" + country.Id + "'>" + country.Name + "</option>";
        });
        $("#selCountries").html(items);
    });
}

$.getJSON("/ServicesCatalog/GetAllCustomersByCurrentAccount", {}, function (data) {
    var items = "<option>--Seleccionar--</option>";
    $("#selCustomer").empty();
    $.each(data, function (i, customer) {
        items += "<option value='" + customer.Id + "'>" + customer.Name + "</option>";
    });
    $("#selCustomer").html(items);
});

function ChangeCountry() {
    $.blockUI({ message: "" });
    var url = "/ServicesLocalization/GetProvincesByCountryId";
    var idCountry = $("#selCountries").val();
    $.getJSON(url, { countryId: idCountry }, function (data) {
        var items = "<option>--Seleccionar--</option>";
        $("#selProvinces").empty();
        $("#selDistricts").html(items);
        $("#selParishes").html(items);
        $("#selSectors").html(items);
        $.each(data, function (i, province) {
            items += "<option value='" + province.Id + "'>" + province.Name + "</option>";
        });
        $("#selProvinces").html(items);
        $.unblockUI();
    });
}

function ChangeProvince() {
    $.blockUI({ message: "" });
    var url = "/ServicesLocalization/GetDistrictsByProvinceId";
    var idProvince = $("#selProvinces").val();
    $.getJSON(url, { idProvince: idProvince }, function (data) {
        var items = "<option>--Seleccionar--</option>";
        $("#selDistricts").empty();
        $("#selParishes").html(items);
        $("#selSectors").html(items);
        $.each(data, function (i, district) {
            items += "<option value='" + district.Id + "'>" + district.Name + "</option>";
        });
        $("#selDistricts").html(items);
        $.unblockUI();
    });
}

function ChangeDistrict() {
    var url = "/ServicesLocalization/GetParishesByDistrictId";
    var idDistrict = $("#selDistricts").val();
    $.getJSON(url, { idDistrict: idDistrict }, function (data) {
        $.blockUI({ message: "" });
        var items = "<option>--Seleccionar--</option>";
        $("#selParishes").empty();
        $.each(data, function (i, parish) {
            items += "<option value='" + parish.Id + "'>" + parish.Name + "</option>";
        });
        $("#selParishes").html(items);
        $.unblockUI();
    });

    url = "/ServicesLocalization/GetSectorsByDistrictId";
    $.getJSON(url, { idDistrict: idDistrict }, function (data) {
        $.blockUI({ message: "" });
        var items = "<option>--Seleccionar--</option>";
        $("#selSectors").empty();
        $.each(data, function (i, sector) {
            items += "<option value='" + sector.Id + "'>" + sector.Name + "</option>";
        });
        $("#selSectors").html(items);
        $.unblockUI();
    });
}

function ChangeOwnerDocument() {
    $.blockUI({ message: "" });
    var url = "/Person/GetPersonByDocument";
    var document = $("#txtOwnerDocument").val();
    $.getJSON(url, { document: document }, function (data) {
        if (data.Id !== emptyGuid) {
            $("#selOwnerTypeDocument").val(data.TypeDocument);
            $("#txtOwnerName").val(data.Name);
            $("#txtOwnerSurname").val(data.SurName);
            $("#txtOwnerPhone").val(data.Phone);
            $("#txtOwnerCellPhone").val(data.Mobile);
        }
        ToogleOwnerInformation(!(data.Name == null));
        $.unblockUI();
    });
}

function ChangeAdministratorDocument() {
    $.blockUI({ message: "" });
    var url = "/Person/GetPersonByDocument";
    var document = $("#txtAdministratorDocument").val();
    $.getJSON(url, { document: document }, function (data) {
        if (data.Id !== emptyGuid) {
            $("#txtAdministratorName").val(data.Name);
            $("#txtAdministratorSurname").val(data.SurName);
            $("#txtAdministratorCellPhone").val(data.Phone);
            $("#txtAdministratorPhone").val(data.Mobile);
        }
        ToogleAdministratorInformation(!(data.Name == null));
        $.unblockUI();
    });
}

function ToogleOwnerInformation(value) {
    $("#selOwnerTypeDocument").prop("disabled", value);
    $("#txtOwnerName").prop("readonly", value);
    $("#txtOwnerSurname").prop("readonly", value);
    $("#txtOwnerPhone").prop("readonly", value);
    $("#txtOwnerCellPhone").prop("readonly", value);
}

function ToogleAdministratorInformation(value) {
    $("#txtAdministratorName").prop("readonly", value);
    $("#txtAdministratorSurname").prop("readonly", value);
    $("#txtAdministratorCellPhone").prop("readonly", value);
    $("#txtAdministratorPhone").prop("readonly", value);
}

$("#chkIsAdministratorOwner").change(function () {
    if (this.checked) {
        CopyInformationToOwnerToAdministrator();
        ToogleAdministratorInformation(true);
    } else {
        ToogleAdministratorInformation(false);
    }
});

function CopyInformationToOwnerToAdministrator() {
    $("#txtAdministratorName").val($("#txtOwnerName").val());
    $("#txtAdministratorSurname").val($("#txtOwnerSurname").val());
    $("#txtAdministratorCellPhone").val($("#txtOwnerCellPhone").val());
    $("#txtAdministratorPhone").val($("#txtOwnerPhone").val());
    $("#txtAdministratorDocument").val($("#txtOwnerDocument").val());
}

var selectedItems = [];

function SelectItem(element) {
    var id = element.id;
    if (element.checked) {
        selectedItems.push(id);
    } else {
        selectedItems.forEach(function (item, i) {
            if (item === id) {
                selectedItems.splice(i, 1);
            }
        });
    }
}

function DeleteSelection() {
    $.blockUI({ message: "" });
    
    bootbox.confirm("Esta seguro que desea eliminar?", function (result) {
        if (result) {
            $.ajax({
                url: "/Branch/Delete",
                type: "post",
                data: {
                    input: JSON.stringify(selectedItems)
                },
                success: function (data) {
                    
                    if (data) {

                        window.location.href = "/Branch/Index";

                    } else {
                        bootbox.alert("Existío un error, Vuelva a intentarlo");
                    }
                    $.unblockUI();
                },
                error: function (xhr) {
                    console.log(xhr);
                    $.unblockUI();
                    //Do Something to handle error
                }
            });
        }
    });
}

$("#btnLocalization").on("click", function () {
    
    $.blockUI({ message: "" });
    $.ajax({
        url: "/Branch/Localization",
        type: "get",
        data: {
            input: JSON.stringify(selectedItems)
        },
        success: function (data) {
            
            if (data) {
                window.location.href = "/Branch/Localization";
            } else {
                bootbox.alert("Existío un error, Vuelva a intentarlo");
            }
            $.unblockUI();
        },
        error: function (xhr) {
            console.log(xhr);
            $.unblockUI();
            //Do Something to handle error
        }
    });
});

function changeCustomer() {
    $.blockUI({ message: "" });
    var url = "/Channel/GetAllChannelsByCustomerId";
    var idCustomer = $("#selCustomer").val();
    $.getJSON(url, { idCustomer: idCustomer }, function (data) {
        var items = "<option>--Seleccionar--</option>";
        $("#selChannel").empty();
        $.each(data, function (i, channel) {
            items += "<option value='" + channel.Id + "'>" + channel.Name + "</option>";
        });
        $("#selChannel").html(items);
        $.unblockUI();
    });

    $.blockUI({ message: "" });
    url = "/ServicesCatalog/GetAllTypeBusinessByCustomerId";
    idCustomer = $("#selCustomer").val();
    $.getJSON(url, { idCustomer: idCustomer }, function (data) {
        var items = "<option>--Seleccionar--</option>";
        $("#selTypeBusiness").empty();
        $.each(data, function (i, channel) {
            items += "<option value='" + channel.Id + "'>" + channel.Name + "</option>";
        });
        $("#selTypeBusiness").html(items);
        $.unblockUI();
    });
}

function LoadBranchById(idBranch) {
    $.blockUI({ message: "" });
    $.ajax({
        type: "GET",
        url: "/Branch/Get",
        async: false,
        data: {
            idBranch: idBranch
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            ApplyBindingRegisterBranch(data);
        },
        error: function () {
            alert("error");
        }
    });
}

function ApplyBindingRegisterBranch(data) {

    vueVM = new Vue({
        el: "#divBranch",
        data: {
            branch: data
        },
        methods: {
            moment: function () {
                return moment();
            }
        }
    });

    $.unblockUI();
}

function Save() {
    $.ajax({
        url: "/Branch/SaveRegister",
        type: "post",
        data: {
            branch: JSON.stringify(vueVM.$data.branch)
        },
        success: function (data) {
            if (data) {
                bootbox.alert("Registros Actualizados Satisfactoriamente");
                window.location.href = "/Branch/Index";
            } else {
                bootbox.alert("Error al tratar de Grabar su Local, revise la información de los campos");
            }
        },
        error: function (xhr) {
            bootbox.alert("Error al tratar de Grabar su Local, revise la información de los campos");
        }
    });
}