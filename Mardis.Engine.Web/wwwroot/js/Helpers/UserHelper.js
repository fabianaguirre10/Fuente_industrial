var itemsSelected = [];

function SelectItem(idCampaign) {
    itemsSelected.forEach(function (item, i) {
        if (item === idCampaign) {
            itemsSelected.splice(i, 1);
        }
    });
    itemsSelected.push(idCampaign);
}

function DeleteSelection() {
    bootbox.confirm("Esta seguro que desea eliminar?", function (result) {
        if (result) {
            $.ajax({
                url: "/User/Delete",
                type: "post",
                data: {
                    input: JSON.stringify(itemsSelected)
                },
                success: function (data) {
                    if (data) {
                        window.location.href = "/User/Index";
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