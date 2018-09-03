//View Model Filtros
var mySlidebars = new $.slidebars();
var urlResults = "/ServicesFilter/GetResults"

function FilterViewModel() {
    var self = this;


    self.IdFilterTable = ko.observable();
    self.IdFilterField = ko.observable();
    self.IdFilterCriteria = ko.observable();
    self.FilterValue = ko.observable();


    self.ItemFilter = ko.observableArray([new SelectViewModel("", "Seleccione el Campos")]);
    self.ItemsFields = ko.observableArray([new SelectViewModel("", "Seleccione el Campos")]);
    self.ItemsOperators = ko.observableArray([new SelectViewModel("", "Seleccione el Operador")]);




    self.IdFilterTable.subscribe(function (newValue) {

        var valueFilterTable = newValue;

        modelView.FilterViewModel.ItemsFields.removeAll();
        modelView.FilterViewModel.ItemsFields.push(new SelectViewModel("", "Seleccione el Campos"));

        if (null != valueFilterTable && "" != valueFilterTable) {
            validationFormGroup("divFilterTable", valueFilterTable);

            $.ajax({
                url: "/ServicesFilter/GetFilterFieldByTable",
                type: "post",
                data: {
                    idFilterTable: valueFilterTable
                },
                success: function (data) {
                    if (0 < data.length) {

                        for (var index = 0; index < data.length; index++) {
                            var tempFilterField = data[index];
                            modelView.FilterViewModel.ItemsFields
                                     .push(new SelectViewModel(tempFilterField.Id, tempFilterField.FieldDescription));
                        }
                    }
                },
                error: function (xhr) {
                    //Do Something to handle error
                }
            });

        } else {

            //hay que encerar el resto de combos
        }
    });

    self.IdFilterField.subscribe(function (newValue) {
        var valueFilterField = newValue;


        modelView.FilterViewModel.ItemsOperators.removeAll();
        modelView.FilterViewModel.ItemsOperators.push(new SelectViewModel("", "Seleccione el Campos"));

        if (null != valueFilterField && "" != valueFilterField) {

            validationFormGroup("divFilterField", valueFilterField);

            $.ajax({
                url: "/ServicesFilter/GetTypeFilterByField",
                type: "post",
                data: {
                    idFilterField: valueFilterField
                },
                success: function (data) {
                    if (0 < data.length) {

                        for (var index = 0; index < data.length; index++) {
                            var tempFilterOperator = data[index];
                            modelView.FilterViewModel.ItemsOperators.push(new SelectViewModel(tempFilterOperator.Id, tempFilterOperator.Name));
                        }
                    }
                },
                error: function (xhr) {
                    //Do Something to handle error
                }
            });


        } else {

            // encerar los otros combos

        }
    });


    self.IdFilterCriteria.subscribe(function (newValue) {
        var valueFilterOperator = this.value;

        if (null != valueFilterOperator && "" != valueFilterOperator) {
            validationFormGroup("divFilterOperator", valueFilterOperator);
        }
    });

    self.applyClick = function () {
        var valFilterExecution = $("#hidIdFilterExecution").val();
        var valFilterTable = $("#selFilterTable").val();
        var valFilterField = $("#selFilterField").val();
        var valFilterOperator = $("#selFilterOperator").val();
        var valFilterValue = $("#txtFilterValue").val();
        var isValid = true;

        isValid = validationFormGroup("divFilterTable", valFilterTable);

        if (!validationFormGroup("divFilterField", valFilterField)) {
            isValid = false;
        }

        if (!validationFormGroup("divFilterOperator", valFilterOperator)) {
            isValid = false;
        }

        if (!validationFormGroup("divFilterValue", valFilterValue)) {
            isValid = false;
        }

        showError("divError", isValid);

        if (isValid) {

            try {
                $.blockUI({ message: '' });

                $.ajax({
                    url: "/ServicesFilter/SaveFilterDetail",
                    type: "post",
                    data: {
                        filter: {
                            "IdFilterExecution": valFilterExecution, "IdFilterTable": valFilterTable,
                            "IdFilterField": valFilterField, "IdFilterCriteria": valFilterOperator,
                            "FilterValue": valFilterValue
                        }
                    },
                    success: function (data) {

                        if (null != data) {
                            $("#pnlToolbar").append(getFilter(data.FilterCriteria.TypeFilter.SignFilter + " " + data.Value, data.Id));
                            mySlidebars.slidebars.close();


                            //Llamada a los resultados
                            CallResults();
                            //

                        }
                    },
                    error: function (xhr) {
                        //Do Something to handle error
                    }
                });
            } finally {
                $.unblockUI();
            }

        } else {


        }
    };
}

function CallResults(propertyName, propertyValue) {
    var valFilterExecution = $("#hidIdFilterExecution").val();

    $.ajax({
        url: urlResults,
        type: "get",
        data: {
            idFilterExecution: valFilterExecution,
            propertyName: propertyName,
            propertyValue: propertyValue
        },
        success: function (data) {
            PopulateResultsInTable(data);
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

//Borrar Filtro
function DeleteFilter(idButton) {

    var button = $("#" + idButton);
    var valFilterExecution = $("#hidIdFilterExecution").val();

    if (!button.is(':empty')) {
        $.blockUI({ message: '' });

        try {
            $.ajax({
                url: "/ServicesFilter/DeleteFilterDetail",
                type: "post",
                data: {
                    idFilterExecutionDetail: idButton
                },
                success: function () {
                    button.remove();

                    //Llamada a los resultados
                    CallResults();
                    //
                },
                error: function (xhr) {
                    //Do Something to handle error
                }
            });
        } finally {
            $.unblockUI();
        }
    }
}

function CloseFilter() {
    mySlidebars.slidebars.close();
}


//Cargar Items en modelo
//propertyName y propertyValue son parametros opcionales para filtrar por campos especificos
function LoadItemsFilter(modelView, divMain, propertyName, propertyValue) {

    return $.ajax({
        url: "/ServicesFilter/GetFilterTable",
        type: "get",
        data: {
            idFilterController: IdFilterExecution
        },
        success: function (data) {
            if (0 < data.length) {

                for (var index = 0; index < data.length; index++) {
                    var tempFilter = data[index];
                    modelView.FilterViewModel.ItemFilter.push(new SelectViewModel(tempFilter.Id, tempFilter.Description));
                }

                ApplyItemFilter(modelView, divMain, propertyName, propertyValue);
            }
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

function ApplyItemFilter(modelView, divMain, propertyName, propertyValue) {
    //Llama a poblar la data 
    CallResults(propertyName, propertyValue);
}