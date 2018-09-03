guidEmpty = "00000000-0000-0000-0000-000000000000";

function MainProductViewModel() {
    var self = this;

    self.FilterViewModel = new FilterViewModel();

    self.RegisterProduct = ko.observable();

    self.ProductList = ko.observableArray([]);

    self.Results = ko.observableArray([]);

    self.ProductCategories = ko.observableArray();

    self.itemDelete = ko.observableArray();

    self.EditProductClick = function(id) {
        $.ajax({
            url: "/Product/Register",
            type: "get",
            data: {
                idProduct: id
            },
            success: function (viewHtml) {
                $("#divMain").html(viewHtml);
            },
            error: function (xhr) {
                //Do Something to handle error
            }
        });
    };
}

function SaveProduct() {
    $.blockUI({ message: "" });
    $.ajax({
        url: "/Product/SaveProduct",
        type: "post",
        data: {
            product: modelView.RegisterProduct
        },
        success: function (data) {
            if (null != data && "" !== data) {
                var desData = $.parseJSON(data);
                ApplyBindingProduct(desData);
                bootbox.alert("Registros Actualizados Satisfactoriamente");
            }
            else {
                bootbox.alert("Existío un error, Vuelva a intentarlo");
                $.unblockUI();
            }

        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

function NewProductClickEvent() {
    window.LoadPage("/Product/Register");
}

function LoadProductById() {

    $.ajax({
        url: "/Product/GetProductById",
        type: "get",
        data: {
            idProduct: window.IdProduct
        },
        success: function (data) {
            if (null != data && '' !== data) {

                var desData = $.parseJSON(data);
                desData.IdCustomer = IdCustomer;
                ApplyBindingProduct(desData);
            }
        },
        error: function (xhr) {
            alert(xhr);
        }
    });
}

function LoadProductCategories() {
    $.ajax({
        url: "/Product/GetProductCategoryList",
        type: "get",
        data: {
            idCustomer: IdCustomer
        },
        success: function (data) {
            modelView.ProductCategories(data);
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

function InitializeProductsObjects() {
    LoadProductCategories();
}

function ApplyBindingProduct(data) {
    modelView.RegisterProduct = data;

    ko.cleanNode(document.getElementById("divRegisterProduct"));
    ko.applyBindings(modelView, document.getElementById("divRegisterProduct"));

    $.unblockUI();
}

function Exit() {
    window.LoadPage("/Customer");
}

function DeleteProduct() {
    bootbox.confirm("Esta seguro que desea eliminar?", function (result) {
        if (result) {

            $.ajax({
                url: "/Product/Delete",
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