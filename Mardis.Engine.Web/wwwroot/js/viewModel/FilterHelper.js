var actionName = "";
var controllerName = "";

var valueFilter = "";
var criteriaFilter = "";
var nameFilter = "";
var idFilter = "";

//var filters = [];
function LoadPedidos() {

   
    $.ajax({
        type: "GET",
        url: "/Pedidos/Get",
        // async: false,
        data: {
          
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data) {
                store.clearAll();
                ApplyBindingTaskService(data);

            } else {
                alert("Error! no se ha encontrado la tarea" + error);
                window.location.href = "/Task/MyTasks";
            }
        },
        error: function (error) {
            console.log(error);
            alert("Error! no se ha encontrado la tarea" + error);
            window.location.href = "/Task/MyTasks";
        }
    });
}
//DAMOS COMNPORTAMIENTO AL BOTÓN DEL FILTRO
$("#btnFilter").popover({
    placement: "bottom",
    title: "Aplicar filtros a pantalla actual",
    html: true,
    content: $("#popover-content").html(),
    container: "body"
}).on("click", function () {
    $(".input-sm").on("input", function (e) {
        if (e.target.id === "txtValueFilter") {
            valueFilter = e.target.value;
        }
        if (e.target.id === "selFilterName") {
            nameFilter = e.target.selectedOptions[0].innerHTML;
            idFilter = e.target.value;
        }
        if (e.target.id === "selCriteria") {
            criteriaFilter = e.target.value;
        }
    });
    $(".btn-primary").on("click", function () {
        if (valueFilter.length >= 1) {
            if (idFilter === "") {
                idFilter = $("#selFilterName").val();
            }
            if (criteriaFilter === "") {
                criteriaFilter = $("#selCriteria").val();
            }
            if (nameFilter === "") {
                nameFilter = $("#selFilterName option:selected").text();
            }
            var filterValue = { IdFilter: idFilter, Criteria: criteriaFilter, Value: valueFilter, NameFilter: nameFilter, Visible: true };
            //filters.push(filterValue);
            var filterString = JSON.stringify(filterValue);
            var url = "\\controller\\action?filterValues=filtt".replace("action", actionName).replace("controller", controllerName).replace("filtt", filterString);
            window.location.href = url;
        }
    });
});

function RemoveFilter(element) {
    var idFilter = element.id;
    filters.forEach(function (item, i) {
        if (item.IdFilter === idFilter) {
            filters.splice(i, 1);
        }
    });
    var url = "\\controller\\action?filterValues=filtt&deleteFilter=true".replace("action", actionName).replace("controller", controllerName).replace("filtt", JSON.stringify(filters));
    window.location.href = url;
}

function OpenPaginatedView() {
    $.blockUI({ message: "" });
    $.post("/" + controllerName + "/" + actionName,
    {
        filterValues: JSON.stringify(filters)
    },
    function (data, status) {
        debugger;
        $("#divControllerContent").html(data);
        $.unblockUI();
    });
}