var modelView = new MainBranchViewModel();

guidEmpty = "00000000-0000-0000-0000-000000000000";

//#region ************Main View Model de Locales************
function MainBranchViewModel() {
    var self = this;
    //Esto es para que tenga filtros las pantallas de index
    self.FilterViewModel = new FilterViewModel();
    //Esto es para mantenimiento de los locales
    self.RegisterBranch = ko.observable();

    //Esto es para Cascading de Paises
    self.Countries = ko.observableArray([]);
    //Esto es para Cascading de Provincias
    self.Provinces = ko.observableArray([]);
    self.ProvincesTemp = ko.observableArray([]);
    self.IdProvince = ko.observable();
    //Esto es para Cascading de Cantones
    self.Districts = ko.observableArray([]);
    self.DistrictsTemp = ko.observableArray([]);
    self.IdDistrict = ko.observable();
    //Esto es para Cascading de Parroquias
    self.Parishes = ko.observableArray([]);
    self.ParishesTemp = ko.observableArray([]);
    self.IdParish = ko.observable();
    //Esto es para Cascading de Zonas
    self.Sectors = ko.observableArray([]);
    self.SectorsTemp = ko.observableArray([]);
    self.IdSector = ko.observable();

    //Esto es para Cascading de Clientes
    self.Customers = ko.observableArray([]);
    //Esto es para Cascading de Canales
    self.Channels = ko.observable();
    //Esto es para el Cascading de Tipos de Negocio
    self.TypeBusiness = ko.observable();

    //Cuenta auxiliar para creación
    self.Account = ko.observable();
    //Esto es para las Vuetas de Local
    self.BusinessCustomers = ko.observableArray([]);
    //Resultado de la búsqueda
    self.Results = ko.observableArray();

    self.ResultBranchesGeo = ko.observableArray([]);

    //lista de borrados
    self.itemDelete = ko.observableArray();
    self.itemBCDelete = ko.observableArray();

    self.TaskServices = ko.observableArray();
    self.BranchImages = ko.observableArray();

    self.EditBranchClick = function (id) {

        $.ajax({
            url: "/Branch/New",
            type: "get",
            data: {
                idBranch: id
            },
            success: function (viewHtml) {
                $("#divMain").html(viewHtml);
            },
            error: function () {
            }
        });

    };

    self.OpenProfileBranchClick = function (id) {
        $.ajax({
            url: "/Branch/Profile",
            type: "get",
            data: {
                idBranch: id
            },
            success: function (viewHtml) {
                $("#divMain").html(viewHtml);
            },
            error: function () {
            }
        });
    };

    self.DeleteImageBranchClick = function (id) {
        $.blockUI({ message: '' });
        $.ajax({
            url: "/Branch/DeleteImageBranch",
            type: "get",
            data: {
                idImageBranch: id
            },
            success: function (data) {
                if (null != data && "" !== data) {
                    var desData = $.parseJSON(data);
                    ApplyBindingRegisterBranch(desData);
                    bootbox.alert("Registros Actualizados Satisfactoriamente");
                } else {
                    bootbox.alert("Existío un error, Vuelva a intentarlo");
                }

                $.unblockUI();
            },
            error: function () {
                $.unblockUI();
            }
        });
    };

}
//#endregion

//#region *******************CRUD Locales*******************

function NewRecordClickEvent() {
    window.LoadPage("/Branch/New");
}

function SaveBranch() {

    $.blockUI({ message: '' });

    var isValid = ValidateBranch();

    ko.utils.arrayForEach(modelView.BusinessCustomers(), function (bc) {//businessCustomerTemp
        var dato = {
            "IdBranch": "00000000-0000-0000-0000-000000000000",
            "Id": "00000000-0000-0000-0000-000000000000",
            "IdCustomer": bc.IdCustomer,
            "IdTypeBusiness": bc.IdTypeBusiness,
            "IdChannel": bc.IdChannel,
            "Branch": null,
            "Customer": null,
            "Channel": null,
            "TypeBusiness": null
        };

        modelView.RegisterBranch.BranchCustomers.push(dato);
    });

    showError("divError", isValid);
    if (isValid) {
        if (modelView.RegisterBranch.IsAdministratorOwner) {
            modelView.RegisterBranch.IsAdministratorOwner = "SI";
        }
        else {
            modelView.RegisterBranch.IsAdministratorOwner = "NO";
        }

        $.ajax({
            url: "/Branch/SaveBranch",
            type: "post",
            data: {
                input: ko.toJSON(modelView.RegisterBranch)
            },
            success: function (data) {
                if (null != data && "" !== data) {
                    var desData = $.parseJSON(data);
                    ApplyBindingRegisterBranch(desData);
                    bootbox.alert("Registros Actualizados Satisfactoriamente");
                } else {
                    bootbox.alert("Existío un error, Vuelva a intentarlo");
                }

                //LoadPage("/Branch/Index");
                $.unblockUI();
            },
            error: function () {
                $.unblockUI();
            }
        });
    }
}

function LoadBranchById(idBranch) {
    $.blockUI({ message: "" });
    $.ajax({
        type: "GET",
        url: "/Branch/GetBranchById",
        data: {
            idBranch: idBranch
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            PushBranchImages(data.BranchImages);
            ApplyBindingRegisterBranch(data);
            LoadLocalizationsBindings();
            LoadBranchCustomers();
            $.unblockUI();
        },
        error: function () {
            alert("error");
            $.unblockUI();
        }
    });
}

function LoadBranchProfile(idBranch) {
    $.blockUI({ message: "" });
    $.ajax({
        type: "GET",
        url: "/Branch/GetBranchProfile",
        data: {
            IdBranch: idBranch
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            ApplyBindingRegisterBranch(data);
            $.unblockUI();
        },
        error: function () {
            alert("error");
            $.unblockUI();
        }
    });
}

function LoadBranchCustomers() {


    for (var i = 0; i < modelView.RegisterBranch.BranchCustomers.length; i++) {
        modelView.BusinessCustomers.push({
            Id: i,
            IdChannel: modelView.RegisterBranch.BranchCustomers[i].IdChannel,
            IdCustomer: modelView.RegisterBranch.BranchCustomers[i].IdCustomer,
            IdTypeBusiness: modelView.RegisterBranch.BranchCustomers[i].IdTypeBusiness,
            NameChannel: modelView.RegisterBranch.BranchCustomers[i].Channel.Name,
            NameCustomer: modelView.RegisterBranch.BranchCustomers[i].Customer.Name,
            NameTypeBusiness: modelView.RegisterBranch.BranchCustomers[i].TypeBusiness.Name
        });
        window.countCustomer++;
    }
    modelView.RegisterBranch.BranchCustomers = ko.observableArray([]);
}

function DeleteBranch() {
    bootbox.confirm("Esta seguro que desea eliminar?", function (result) {
        if (result) {
            $.ajax({
                url: "/Branch/Delete",
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
                    alert(xhr);
                    //Do Something to handle error
                }
            });
        }
    });
}

function DeleteCustomer() {
    modelView.itemBCDelete().forEach(function (idPanel) {
        modelView.BusinessCustomers().forEach(function (value) {
            if (String(value.Id) === idPanel) {
                modelView.BusinessCustomers.remove(value);
            }
        });

    });
}

//#endregion

//#region *****************Clientes de Local****************
function GetChannelsByIdCustomer() {
    var customerId = $("#selCustomer").val();
    if (null == customerId || "" === customerId) {
        return;
    }
    GetAllChannelsByCustomer(customerId, function (d) {
        modelView.Channels(d);
    });
}

function GetTypeBusinessByIdCustomer() {
    var customerId = $("#selCustomer").val();
    if (null == customerId || "" === customerId) {
        return;
    }
    GetAllTypeBusinessByCustomer(customerId, function (d) {
        modelView.TypeBusiness(d);
    });
}

function AddAccountToBranch() {
    modelView.Account.NameChannel = $("#selChannel>option:selected").html();//con jquery hay manera mas efecvtiva que hacer esto
    modelView.Account.NameCustomer = $("#selCustomer>option:selected").html();
    modelView.Account.NameTypeBusiness = $("#selTypeBusiness>option:selected").html();
    modelView.BusinessCustomers.push({
        Id: window.countCustomer++,
        IdChannel: $("#selChannel").val(),
        IdCustomer: $("#selCustomer").val(),
        IdTypeBusiness: $("#selTypeBusiness").val(),
        NameChannel: modelView.Account.NameChannel,
        NameCustomer: modelView.Account.NameCustomer,
        NameTypeBusiness: modelView.Account.NameTypeBusiness
    });
}
//#endregion

//#region **********************Persons*********************

function GetOwnerByDocument(doc) {
    //null    && "" 
    if (doc === "S") {
        doc = modelView.RegisterBranch.PersonOwner.Document;

        if (doc == null) {
            return;
        }
    }
    else {
        return;
    }
    $.blockUI({ message: "" });
    GetPersonByDocument(doc, function (d) {
        modelView.RegisterBranch.PersonOwner = d;
        modelView.RegisterBranch.IdPersonOwner = d.Id;
        refreshData(modelView, "divRegisterBranch");

        DisableOwnerInformation(!(null == modelView.RegisterBranch.PersonOwner.Name));
    });
    $.unblockUI();
}

function GetAdministratorByDocument(doc) {
    if (doc === "S") {
        doc = modelView.RegisterBranch.PersonAdministration.Document;
        if (doc == null) {
            return;
        }
    }
    else {
        return;
    }
    var checkedValue = $('.messageCheckbox:checked').val();
    if (!checkedValue) {
        $.blockUI({ message: '' });
        GetPersonByDocument(doc, function (d) {
            modelView.RegisterBranch.PersonAdministration = d;
            modelView.RegisterBranch.IdPersonAdministrator = d.Id;
            refreshData(modelView, "divRegisterBranch");
            DisableAdministratorInformation(!(null == modelView.RegisterBranch.PersonAdministration.Name));
            $.unblockUI();
        });
    }
}
//#endregion

//#region **********************Events**********************

function InitializeDataNewBranchView() {
    GetAllCountries(function (d) {
        modelView.Countries(d);
    });
    GetAllCurrentCustomers(function (d) {
        modelView.Customers(d);
    });
    modelView.Account = {
        IdChannel: guidEmpty,
        IdCustomer: guidEmpty,
        IdTypeBusiness: guidEmpty,
        NameChannel: "",
        NameCustomer: "",
        NameTypeBusiness: ""
    };
    DisableOwnerInformation(true);
    DisableAdministratorInformation(true);
}

function EventHanderCountries(data) {

    ko.mapping.fromJS(data, {}, modelView.Countries);
}

function refreshData(modelView, divName) {

    ko.cleanNode(document.getElementById(divName));
    ko.applyBindings(modelView, document.getElementById(divName));
}

function ApplyBindingRegisterBranch(data) {
    modelView.RegisterBranch = data;
    modelView.IdProvince = modelView.RegisterBranch.IdProvince;
    modelView.IdParish = modelView.RegisterBranch.IdParish;
    modelView.IdSector = modelView.RegisterBranch.IdSector;
    modelView.IdDistrict = modelView.RegisterBranch.IdDistrict;

    ko.cleanNode(document.getElementById("divRegisterBranch"));
    ko.applyBindings(modelView, document.getElementById("divRegisterBranch"));
}

function AdministratorIsOwner() {

    if (document.getElementById('chkOwner').checked) {
        if ("" === $("#txtIdentification").val()) {
            alert("Por favor Ingrese Información del Propietario");
            document.getElementById('chkOwner').checked = false;
            return;
        }
        CopyInformationToOwnerToAdministrator();
        DisableAdministratorInformation(true);
    } else {
        CleanAdministratorInformation();
        DisableAdministratorInformation(false);
    }
}

function CopyInformationToOwnerToAdministrator() {
    $("#txtOwnerIdentification").val($("#txtIdentification").val());
    $("#txtOwnerFirstName").val($("#txtFirstName").val());
    $("#txtCodeAdmin").val($("#txtCodeOwner").val());
    $("#txtOwnerSurname").val($("#txtSurname").val());
    $("#txtOwnerPhone").val($("#txtPhone").val());
    $("#txtOwnerCellPhone").val($("#txtCellPhone").val());
    modelView.RegisterBranch.IdPersonAdministrator = modelView.RegisterBranch.PersonOwner.Id;
    modelView.RegisterBranch.PersonAdministration.Id = modelView.RegisterBranch.PersonOwner.Id;
    modelView.RegisterBranch.PersonAdministration.Document = $("#txtOwnerIdentification").val();
    modelView.RegisterBranch.PersonAdministration.Name = $("#txtOwnerFirstName").val();
    modelView.RegisterBranch.PersonAdministration.SurName = $("#txtOwnerSurname").val();
    modelView.RegisterBranch.PersonAdministration.Code = $("#txtCodeAdmin").val();
    modelView.RegisterBranch.PersonAdministration.Phone = $("#txtOwnerPhone").val();
    modelView.RegisterBranch.PersonAdministration.Mobile = $("#txtOwnerCellPhone").val();
}

function DisableAdministratorInformation(value) {
    if ("" !== $("#txtIdentification").val()) {
        $("#txtOwnerIdentification").prop('readonly', value);
    }
    $("#txtOwnerFirstName").prop('readonly', value);
    $("#selAdministratorTypePerson").prop('readonly', value);
    $("#txtCodeAdmin").prop('readonly', value);
    $("#txtOwnerSurname").prop('readonly', value);
    $("#txtOwnerPhone").prop('readonly', value);
    $("#txtOwnerCellPhone").prop('readonly', value);
    $("#selAdministratorTypePerson").prop('disabled', value);
}

function DisableOwnerInformation(value) {
    $("#dllTypePerson").prop('disabled', value);
    $("#txtFirstName").prop('readonly', value);
    $("#txtCodeOwner").prop('readonly', value);
    $("#txtSurname").prop('readonly', value);
    $("#txtPhone").prop('readonly', value);
    $("#txtCellPhone").prop('readonly', value);
}

function CleanAdministratorInformation() {
    $("#txtOwnerIdentification").val('');
    $("#txtOwnerFirstName").val('');
    $("#txtCodeAdmin").val('');
    $("#txtOwnerSurname").val('');
    $("#txtOwnerPhone").val('');
    $("#txtOwnerCellPhone").val('');
}

function CloseBranch() {
    window.LoadPage("/Branch");
}

//#endregion

//#region **************Localization Cascading**************

function LoadProvinces() {
    if (modelView.RegisterBranch.Id !== guidEmpty) {
        return;
    }
    var countryCode = $("#ddlCountry").val();
    if (!countryCode) {
        return;
    }
    GetProvincesByIdCountry(countryCode, function (d) {
        modelView.Provinces(d);
    });
}

function LoadDistricts() {
    if (modelView.RegisterBranch.Id !== guidEmpty) {
        return;
    }
    var provinceCode = $("#ddlProvince").val();
    if (!provinceCode) {
        return;
    }
    GetDistrictsByIdProvince(provinceCode, function (d) {
        modelView.Districts(d);
    });
}

function LoadParishes() {
    if (modelView.RegisterBranch.Id !== guidEmpty) {
        return;
    }
    var districtId = $("#ddlDistrict").val();
    if (!districtId) {
        return;
    }
    GetParishesByIdDistrict(districtId, function (d) {
        modelView.Parishes(d);
    });
}

function LoadSectors() {
    if (modelView.RegisterBranch.Id !== guidEmpty) {
        return;
    }
    var districtId = $("#ddlDistrict").val();
    if (!districtId) {
        return;
    }
    GetSectorsByIdDistrict(districtId, function (d) {
        modelView.Sectors(d);
    });
}

function LoadLocalizationsBindings() {
    if (modelView.RegisterBranch.IdCountry !== guidEmpty && modelView.RegisterBranch.IdCountry) {
        GetProvincesByIdCountry(modelView.RegisterBranch.IdCountry, function (d) {
            modelView.Provinces(d);
            modelView.RegisterBranch.IdProvince = modelView.IdProvince;
            $("#ddlProvince").val(modelView.IdProvince);
        });
    }
    if (modelView.IdProvince !== guidEmpty && modelView.IdProvince) {
        GetDistrictsByIdProvince(modelView.IdProvince, function (d) {
            modelView.Districts(d);
            modelView.RegisterBranch.IdDistrict = modelView.IdDistrict;
            $("#ddlDistrict").val(modelView.IdDistrict);
        });
    }
    if (modelView.IdDistrict !== guidEmpty && modelView.IdDistrict) {
        GetParishesByIdDistrict(modelView.IdDistrict, function (d) {
            modelView.Parishes(d);
            modelView.RegisterBranch.IdParish = modelView.IdParish;
            $("#ddlParish").val(modelView.IdParish);
        });
    }
    if (modelView.IdDistrict !== guidEmpty && modelView.IdDistrict) {
        GetSectorsByIdDistrict(modelView.IdDistrict, function (d) {
            modelView.Sectors(d);
            modelView.RegisterBranch.IdSector = modelView.IdSector;
            $("#ddlSector").val(modelView.IdSector);
        });
    }
}

//#endregion

//#region *******************Validations********************

function ValidateBranch() {
    var jsonRegister = ko.toJS(ko.mapping.toJS(modelView.RegisterBranch));
    var isValid = true;

    if (!isValidField("divGBranchName", jsonRegister.Name)) {
        isValid = false;
    }

    if (!isValidField("divGBranchCountry", jsonRegister.IdCountry)) {
        isValid = false;
    }

    if (!isValidField("divGBranchProvince", jsonRegister.IdProvince)) {
        isValid = false;
    }

    if (!isValidField("divGBranchDistrict", jsonRegister.IdDistrict)) {
        isValid = false;
    }

    if (!isValidField("divGBranchParish", jsonRegister.IdParish)) {
        isValid = false;
    }

    if (!isValidField("divGBranchSector", jsonRegister.IdSector)) {
        isValid = false;
    }

    if (!isValidField("divGBranchZone", jsonRegister.Zone)) {
        isValid = false;
    }

    if (!isValidField("divGBranchNeighborhood", jsonRegister.Neighborhood)) {
        isValid = false;
    }

    if (!isValidField("divGBranchLatitude", jsonRegister.LatitudeBranch)) {
        isValid = false;
    }

    if (!isValidField("divGBranchLongitude", jsonRegister.LenghtBranch)) {
        isValid = false;
    }

    if (!isValidField("divGBranchMainStreet", jsonRegister.MainStreet)) {
        isValid = false;
    }

    if (!isValidField("divGBranchSecondaryStreet", jsonRegister.SecundaryStreet)) {
        isValid = false;
    }

    if (!isValidField("divGBranchNumber", jsonRegister.NumberBranch)) {
        isValid = false;
    }

    if (!isValidField("divGBranchReference", jsonRegister.Reference)) {
        isValid = false;
    }

    if (!isValidField("divGBranchOwnerIdentification", jsonRegister.PersonOwner.Document)) {
        isValid = false;
    }

    if (!isValidField("divGBranchOwnerTypePerson", jsonRegister.PersonOwner.TypeDocument)) {
        isValid = false;
    }

    if (!isValidField("divGBranchOwnerName", jsonRegister.PersonOwner.Name)) {
        isValid = false;
    }

    if (!isValidField("divGBranchOwnerSurname", jsonRegister.PersonOwner.SurName)) {
        isValid = false;
    }

    if (!isValidField("divGBranchOwnerPhone", jsonRegister.PersonOwner.Phone)) {
        isValid = false;
    }

    if (!isValidField("divGBranchOwnerMobile", jsonRegister.PersonOwner.Mobile)) {
        isValid = false;
    }

    if (!isValidField("divGBranchAdministratorIdentification", jsonRegister.PersonAdministration.Document)) {
        isValid = false;
    }

    if (!isValidField("divGBtranchAdministratorName", jsonRegister.PersonAdministration.Name)) {
        isValid = false;
    }

    if (!isValidField("divBranchAdministratorSurname", jsonRegister.PersonAdministration.SurName)) {
        isValid = false;
    }

    if (!isValidField("divBranchAdministratorPhone", jsonRegister.PersonAdministration.Phone)) {
        isValid = false;
    }

    if (!isValidField("divBranchAdministratorMobile", jsonRegister.PersonAdministration.Mobile)) {
        isValid = false;
    }
    return isValid;
}

//#endregion

function GeoLocationLoad() {
    $.ajax({
        url: "/Branch/Localization",
        type: "get",
        data: {
            input: ko.toJSON(modelView.itemDelete)
        },
        success: function (viewHtml) {
            $("#divMain").html(viewHtml);
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

function SetLocation() {
    modelView.RegisterBranch.LatitudeBranch = $("#Latitude").val();
    modelView.RegisterBranch.LenghtBranch = $("#Longitude").val();
    $("#txtLongitude").val($("#Longitude").val());
    $("#txtLatitude").val($("#Latitude").val());
}

function ShowPartialBranchSearch() {

    $("#divBranchLocalization").modal();
    var point;
    if (modelView.RegisterBranch.LatitudeBranch && modelView.RegisterBranch.LenghtBranch) {
        point =
            {
                latitud: modelView.RegisterBranch.LatitudeBranch,
                longitud: modelView.RegisterBranch.LenghtBranch,
                Name: modelView.RegisterBranch.Name
            }
    }
    NewMap(point);
}

function UploadBranchImage() {
    $.blockUI({ message: '' });

    modelView.BranchImages([]);

    var fileUpload = $("#file-1").get(0);
    var files = fileUpload.files;
    var data = new FormData();
    for (var i = 0; i < files.length ; i++) {
        data.append(files[i].name, files[i]);
    }
    data.append("branch", modelView.RegisterBranch.Id);
    $.ajax({
        type: "POST",
        url: "/Branch/UploadBranchImage",
        contentType: false,
        processData: false,
        data: data,
        success: function (data) {
            PushBranchImages($.parseJSON(data));
            $.unblockUI();
        },
        error: function () {
            alert("There was error uploading files!");
            $.unblockUI();
        }
    });
}

function ShowTasksByService(element) {

    var idService = element.parentNode.id;

    modelView.TaskServices([]);

    $.blockUI({ message: '' });
    $.ajax({
        url: "/Task/GetTaskListByServiceAndBranch",
        type: "get",
        data: {
            idBranch: modelView.RegisterBranch.Id,
            idService: idService
        },
        success: function (data) {
            if (null != data && "" !== data) {
                PushTaskByService($.parseJSON(data));

                $("#divTasksBranch").modal();
            } else {
                bootbox.alert("Existío un error, Vuelva a intentarlo");
            }

            $.unblockUI();
        },
        error: function () {
            $.unblockUI();
        }
    });
}

function PushTaskByService(data) {
    data.forEach(function (item) {
        modelView.TaskServices.push(item);
    });
}

function PushBranchImages(data) {
    data.forEach(function (item) {
        modelView.BranchImages.push(item);
    });
}