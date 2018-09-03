

function MainBulkLoadViewModel() {
    var self = this;

    self.Results = ko.observableArray();
    self.RegisterBulkLoad = ko.observable();
}

function LoadBulkLoadRegisters()
{
    $.blockUI({ message: '' });

    $.ajax({
        url: "/BulkLoad/GetResults",
        type: "get",
        data: {
           
        },
        success: function (data) {
            var desData = $.parseJSON(data);

            if (null != desData) {
                modelView.Results = desData;
            }

            ko.cleanNode(document.getElementById(divMain));
            ko.applyBindings(modelView, document.getElementById(divMain));

            $.unblockUI();
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });

}

function IndexBulkLoad() {
    $.blockUI({ message: '' });

    $.ajax({
        url: "/BulkLoad/Index",
        type: "get",
        data: {
        },
        success: function (viewHtml) {
            $("#divMain").html(viewHtml);
            $.unblockUI();
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

function CreateNewLoad(inputCatalog)
{
    $.blockUI({ message: '' });

    $.ajax({
        url: "/BulkLoad/LoadFile",
        type: "get",
        data: {
            idBulkCatalog: inputCatalog
        },
        success: function (viewHtml) {
            $("#divMain").html(viewHtml);
            $.unblockUI();
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

function ProcessLoad() {
    var inputCatalog = $("#idBulkCatalog").val();
    var inputNameFile = $("#nameFile").val();

    $.blockUI({ message: '' });

    $.ajax({
        url: "/BulkLoad/ResumeBulkLoad",
        type: "get",
        data: {
            idBulkCatalog: inputCatalog,
            nameFile: inputNameFile
        },
        success: function (viewHtml) {
            $("#divMain").html(viewHtml);
            $.unblockUI();
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

//
function InitProcess() {
    var inputCatalog = $("#idBulkCatalog").val();
    var inputNameFile = $("#nameFile").val();
    var characteristicProcess = $('input[name=optionsRadios]:checked').val();

    $.blockUI({ message: '' });

    $.ajax({
        url: "/BulkLoad/InitProcess",
        type: "post",
        data: {
            inputBulkCatalog: inputCatalog,
            characteristBulk: characteristicProcess,
            fileName: inputNameFile
        },
        success: function (data) {
            $("#idProcess").val(data);
            $("#btnApplyProcess").prop('disabled', true);
         

            $.unblockUI();

            startRefresh();
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

function startRefresh() {
   

    $.ajax({
        url: "/BulkLoad/GetBulkLoadById",
        type: "get",
        data: {
            inputProcess: $("#idProcess").val()
        },
        success: function (data) {

            var desData = $.parseJSON(data);

            console.log(desData);
          
            if (null != desData) {
                
                $("#idStatusProcess").text(desData.BulkLoadStatus.Name);
                $("#idTotalAdd").text(desData.TotalAdded);
                $("#idTotalUpdate").text(desData.TotalUpdated);
                $("#idTotalDelete").text(desData.TotalFailed);
                $("#idCurrentTotal").text(desData.CurrentFile);
                $("#idTotalRegister").text(desData.TotalRegister);

                
                if ("A" != desData.BulkLoadStatus.Code) {
                    setTimeout(startRefresh, 4000);
                }
            }

           
        },
        error: function (xhr) {
            //Do Something to handle error

        }
    });

    
}