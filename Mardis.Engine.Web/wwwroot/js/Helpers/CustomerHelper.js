var countChannel = 0;
var countTypeBusiness = 0;
var countProductCategory = 0;

function AddChannel() {
    $.blockUI({ message: "" });
    $("#txtNChannel").val($("#txtNewChannel").val());
}

function DeleteChannel(idChannel) {
    $.blockUI({ message: "" });
    $("#txtDChannel").val(idChannel);
}

function AddTypeBusiness() {
    $.blockUI({ message: "" });
    $("#txtNType").val($("#txtNewTypeBusiness").val());
}

function DeleteTypeBusiness(idTypeBusiness) {
    $.blockUI({ message: "" });
    $("#txtDType").val(idTypeBusiness);
}

function AddProductCategory() {
    $.blockUI({ message: "" });
    $("#txtNCategory").val($("#txtNewProductCategory").val());
}

function DeleteProductCategory(idProductCategory) {
    $.blockUI({ message: "" });
    $("#txtDCategory").val(idProductCategory);
}

var itemsSelected = [];

function SelectItem(idCustomer) {
    itemsSelected.forEach(function (item, i) {
        if (item === idCustomer) {
            itemsSelected.splice(i, 1);
        }
    });
    itemsSelected.push(idCustomer);
}

function DeleteCustomer() {
    bootbox.confirm("Esta seguro que desea eliminar?", function (result) {
        if (result) {
            $.ajax({
                url: "/Customer/Delete",
                type: "post",
                data: {
                    input: JSON.stringify(itemsSelected)
                },
                success: function (data) {
                    if (data) {
                        window.location.href = "/Customer/Index";
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