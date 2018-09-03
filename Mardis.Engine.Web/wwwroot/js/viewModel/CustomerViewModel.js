function MainCustomerViewModel() {

    var self = this;
    //Esto es para que tenga filtros las pantallas de index
    self.FilterViewModel = new FilterViewModel();
    //Esto es para mantenimiento de los locales
    self.Register = ko.observable();
    //
    self.itemStatus = ko.observableArray();
    //
    self.itemTypeBusiness = ko.observableArray();

    self.itemProductCategories = ko.observableArray();

    self.itemChannel = ko.observableArray();

    //lista de borrados
    self.itemDelete = ko.observableArray();
    self.itemType = ko.observableArray();

    //Resultados de la Búsqueda
    self.Results = ko.observableArray();

    self.Products = ko.observableArray([]);

    self.EditCustomerClick = function (id) {

        $.ajax({
            url: "/Customer/Register",
            type: "get",
            data: {
                idCustomer: id
            },
            success: function (viewHtml) {
                $("#divMain").html(viewHtml);
            },
            error: function (xhr) {
                //Do Something to handle error
            }
        });

    };

    self.itemDelete.subscribe(function (newValue) {
    });
}

function ListCustomerTemporary(id, name, action) {
    self = this;

    self.Id = ko.observable(id);
    self.Name = ko.observable(name);
    self.Action = ko.observable(action);
}

//Nuevo Cliente
function NewCustomerClickEvent() {

    $.ajax({
        url: "/Customer/Register",
        type: "get",
        data: {
            idCustomer: null
        },
        success: function (viewHtml) {
            $("#divMain").html(viewHtml);
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

//Cargar estado de cliente
function LoadStatusCustomer() {

    $.ajax({
        url: "/ServicesCatalog/GetStatusCustomers",
        type: "get",
        success: function (data) {
            if (null != data && '' != data) {
                var desData = $.parseJSON(data);
                EventHandlerLoadStatusCustomer(desData);
            }
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

function LoadTypeCustomer() {

    $.ajax({
        url: "/ServicesCatalog/GetTypesCustomers",
        type: "get",
        success: function (data) {
            if (null != data && '' != data) {
                var desData = $.parseJSON(data);
                EventHandlerLoadTypeCustomer(desData);
            }
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}


function LoadCustomerById() {
    $.ajax({
        url: "/Customer/GetOneCustomer",
        type: "get",
        data: {
            id: IdCustomer
        },
        success: function (data) {
            if (null != data && '' != data) {

                var desData = $.parseJSON(data);
                ApplyBindingCustomer(desData);
            }
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

function EventHandlerLoadStatusCustomer(data) {
    ko.mapping.fromJS(data, {}, modelView.itemStatus);
    //Ahora mandamos a cargar el tipo de clientes
    LoadTypeCustomer();
}

function EventHandlerLoadTypeCustomer(data) {
    ko.mapping.fromJS(data, {}, modelView.itemType);
    $.unblockUI();
}

function ApplyBindingCustomer(data) {
    modelView.Register = data;
    //ko.mapping.fromJS(data, {}, modelView.Register);


    if (null != data.Channels) {

        modelView.itemChannel = ko.observableArray();
        $("#lstChannel").html('');

        for (var index = 0; index < data.Channels.length; index++) {
            var oneTempChannel = data.Channels[index];

            $("#lstChannel").append(getButtonChannel(oneTempChannel.Name, oneTempChannel.Id));
            modelView.itemChannel.push(new ListCustomerTemporary(oneTempChannel.Id, oneTempChannel.Name, "BDD"));
        }
    }

    if (null != data.TypeBusiness) {

        modelView.itemTypeBusiness = ko.observableArray();
        $("#lstTypeBusiness").html('');

        for (var index = 0; index < data.TypeBusiness.length; index++) {
            var oneTempTypeBusiness = data.TypeBusiness[index];

            $("#lstTypeBusiness").append(getButtonTypeBusiness(oneTempTypeBusiness.Name, oneTempTypeBusiness.Id));
            modelView.itemTypeBusiness.push(new ListCustomerTemporary(oneTempTypeBusiness.Id, oneTempTypeBusiness.Name, "BDD"));
        }
    }

    if (null != data.ProductCategories) {
        modelView.itemProductCategories = ko.observableArray();
        $("#lstProductCategories").html('');

        for (var i = 0; i < data.ProductCategories.length; i++) {
            var objCategory = data.ProductCategories[i];

            $("#lstProductCategories").append(getButtonProductCategory(objCategory.Name, objCategory.Id));
            modelView.itemProductCategories.push(new ListCustomerTemporary(objCategory.Id, objCategory.Name, "BDD"));
        }
    }

    ko.cleanNode(document.getElementById("divRegisterClient"));
    ko.applyBindings(modelView, document.getElementById("divRegisterClient"));

    $.unblockUI();
}

function SaveCustomer() {

    var jsonRegister = ko.toJS(ko.mapping.toJS(modelView.Register));
    var isValid = true;

    if (!isValidField("divGNameClient", jsonRegister.Name)) {
        isValid = false;
    }

    if (!isValidField("divGState", jsonRegister.IdStatusCustomer)) {
        isValid = false;
    }

    if (!isValidField("divGAbbreviature", jsonRegister.Abbreviation)) {
        isValid = false;
    }

    if (!isValidField("divGTypeClient", jsonRegister.IdTypeCustomer)) {
        isValid = false;
    }

    if (!isValidField("divGContact", jsonRegister.Contact)) {
        isValid = false;
    }

    if (!isValidField("divGPhone", jsonRegister.Phone)) {
        isValid = false;
    }

    if (!isValidField("divGEmail", jsonRegister.Email)) {
        isValid = false;
    }

    showError("divError", isValid);

    if (isValid) {
        $.blockUI({ message: '' });
        $.ajax({
            url: "/Customer/SaveCustomer",
            type: "post",
            data: {
                input: ko.toJSON(modelView.Register),
                inputChannel: ko.toJSON(modelView.itemChannel),
                inputTypeBusiness: ko.toJSON(modelView.itemTypeBusiness),
                inputProductCategory: ko.toJSON(modelView.itemProductCategories)
            },
            success: function (data) {

                if (null != data && "" != data) {
                    var desData = $.parseJSON(data);
                    ApplyBindingCustomer(desData);
                    bootbox.alert("Registros Actualizados Satisfactoriamente");
                }
                else {
                    bootbox.alert("Existío un error, Vuelva a intentarlo");
                }

            },
            error: function (xhr) {
                //Do Something to handle error
            }
        });

    }

}

function CloseCustomer() {
    LoadPage("/Customer");
}

//
function getButtonChannel(label, idButton) {

    var returnHtmlFilter = "";

    returnHtmlFilter += "<a class='btn btn-bricky' href='#' ";
    returnHtmlFilter += " id='" + idButton + "'";
    returnHtmlFilter += " onclick=DeleteChannel('" + idButton + "') ";
    returnHtmlFilter += ">";
    returnHtmlFilter += label;
    returnHtmlFilter += "<i class='clip-close-2'></i>";
    returnHtmlFilter += "</a>";

    return returnHtmlFilter;
}

function getButtonTypeBusiness(label, idButton) {

    var returnHtmlFilter = "";

    returnHtmlFilter += "<a class='btn btn-bricky' href='#' ";
    returnHtmlFilter += " id='" + idButton + "'";
    returnHtmlFilter += " onclick=DeleteTypeBusiness('" + idButton + "') ";
    returnHtmlFilter += ">";
    returnHtmlFilter += label;
    returnHtmlFilter += "<i class='clip-close-2'></i>";
    returnHtmlFilter += "</a>";

    return returnHtmlFilter;
}

function getButtonProductCategory(label, idButton) {

    var returnHtmlFilter = "";

    returnHtmlFilter += "<a class='btn btn-bricky' href='#' ";
    returnHtmlFilter += " id='" + idButton + "'";
    returnHtmlFilter += " onclick=DeleteProductCategory('" + idButton + "') ";
    returnHtmlFilter += ">";
    returnHtmlFilter += label;
    returnHtmlFilter += "<i class='clip-close-2'></i>";
    returnHtmlFilter += "</a>";

    return returnHtmlFilter;
}


//
function AddChannel() {

    var valNewChannel = $("#txtNewChannel").val();
    var idButton = "ch" + countChannel++;

    if (null != valNewChannel && "" != valNewChannel.trim()) {
        valNewChannel = valNewChannel.trim();

        modelView.itemChannel.push(new ListCustomerTemporary(idButton, valNewChannel, "NEW"));
        $("#lstChannel").append(getButtonChannel(valNewChannel, idButton));
        $("#txtNewChannel").val("");
    }
}

//
function AddTypeBusiness() {

    var valNewTypeBusiness = $("#txtNewTypeBusiness").val();
    var idButton = "tb" + countTypeBusiness++;

    if (null != valNewTypeBusiness && "" != valNewTypeBusiness.trim()) {
        valNewTypeBusiness = valNewTypeBusiness.trim();

        modelView.itemTypeBusiness.push(new ListCustomerTemporary(idButton, valNewTypeBusiness, "NEW"));
        $("#lstTypeBusiness").append(getButtonTypeBusiness(valNewTypeBusiness, idButton));
        $("#txtNewTypeBusiness").val("");
    }
}

//
function DeleteChannel(idButton) {
    var button = $("#" + idButton);
    var channelDelete = null;

    if (!button.is(':empty')) {

        ko.utils.arrayForEach(modelView.itemChannel(), function (oneChannel) {

            if ("DELETE" != oneChannel.Action()) {

                if (idButton == oneChannel.Id()) {
                    if ("NEW" == oneChannel.Action()) {
                        button.remove();
                        channelDelete = oneChannel;
                    } else if ("BDD" == oneChannel.Action()) {
                        oneChannel.Action = ko.observable("DELETE");
                        button.remove();
                    }
                }
            }


        });

        if (null != channelDelete) {
            modelView.itemChannel.remove(channelDelete);
        }
    }
}

//
function DeleteTypeBusiness(idButton) {
    var button = $("#" + idButton);
    var typeBusinessDelete = null;

    if (!button.is(':empty')) {


        ko.utils.arrayForEach(modelView.itemTypeBusiness(), function (oneTypeBusiness) {

            if ("DELETE" != oneTypeBusiness.Action()) {
                if (idButton == oneTypeBusiness.Id()) {
                    if ("NEW" == oneTypeBusiness.Action()) {
                        button.remove();
                        typeBusinessDelete = oneTypeBusiness;
                        return false;
                    } else if ("BDD" == oneTypeBusiness.Action()) {
                        oneTypeBusiness.Action = ko.observable("DELETE");;
                        button.remove();
                        return false;
                    }
                }
            }


        });

        if (null != typeBusinessDelete) {
            modelView.itemTypeBusiness.remove(typeBusinessDelete);
        }
    }
}

function DeleteCustomer() {
    bootbox.confirm("Esta seguro que desea eliminar?", function (result) {
        if (result) {

            $.ajax({
                url: "/Customer/Delete",
                type: "post",
                data: {
                    input: ko.toJSON(modelView.itemDelete)
                },
                success: function (data) {
                    if (data) {

                        ko.utils.arrayForEach(modelView.itemDelete(), function (itemDeleteTemp) {

                            var rowId = itemDeleteTemp;

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

var IdProduct_Customer = null;

function CustomerProductsClickEvent() {
    if (!IdProduct_Customer) {
        IdProduct_Customer = modelView.itemDelete()[0];
    }

    if (modelView.itemDelete().length !== 1) {
        bootbox.alert("Debe seleccionar un producto...");
    } else {
        $.ajax({
            url: "/Product/GetCustomerProductList",
            type: "GET",
            data: {
                idCustomer: ko.toJSON(IdProduct_Customer)
            },
            success: function (data) {
                modelView.Products(data);
                LoadPage("/Product");
            },
            error: function (error) {
                alert(error);
            }
        });
    }

}


function AddProductCategory() {

    var valNewProductCategory = $("#txtNewProductCategory").val();
    var idButton = "pc" + window.countProductCategory++;

    if (null != valNewProductCategory && "" !== valNewProductCategory.trim()) {
        valNewProductCategory = valNewProductCategory.trim();

        modelView.itemProductCategories.push(new ListCustomerTemporary(idButton, valNewProductCategory, "NEW"));
        $("#lstProductCategories").append(getButtonProductCategory(valNewProductCategory, idButton));
        $("#txtNewProductCategory").val("");
    }
}

function DeleteProductCategory(idButton) {
    var button = $("#" + idButton);
    var productCategoryDelete = null;

    if (!button.is(':empty')) {


        ko.utils.arrayForEach(modelView.itemProductCategories(), function (object) {

            if ("DELETE" !== object.Action()) {
                if (idButton === object.Id()) {
                    if ("NEW" === object.Action()) {
                        button.remove();
                        productCategoryDelete = object;
                        return false;
                    } else if ("BDD" === object.Action()) {
                        object.Action = ko.observable("DELETE");;
                        button.remove();
                        return false;
                    }
                }
            }


        });

        if (null != productCategoryDelete) {
            modelView.itemProductCategories.remove(productCategoryDelete);
        }
    }
}