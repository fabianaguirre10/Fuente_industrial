guidEmpty = "00000000-0000-0000-0000-000000000000";

var modelView = new MainServicesViewModel();

var vueVM;

function MainServicesViewModel() {
    var self = this;

    self.IdService;
    self.Register = ko.observable();

    self.ItemsTypeService = ko.observableArray();
    self.ItemsCustomer = ko.observableArray();
    self.ItemsChannel = ko.observableArray();
    self.ItemServices = ko.observableArray();
    self.Poll = ko.observableArray();
    self.Logic = ko.observableArray();

    self.TypePollList = ko.observableArray();

    self.EditServiceClick = function (id) {
        $.blockUI({ message: "" });
        $.ajax({
            url: "/Service/Register",
            type: "get",
            data: {
                idService: id
            },
            success: function (viewHtml) {
                $("#divMain").html(viewHtml);
            },
            error: function (xhr) {
                //Do Something to handle error
            }
        });
    };

    self.DeleteServiceClick = function (id) {
        DeleteService(id);
    };

    self.LogicNavigationClick = function (id) {
        $.blockUI({ message: "" });
        $.ajax({
            url: "/Service/GetLogic",
            type: "get",
            data: {
                inputIdService: id
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

function LogicViewModel() {
    var self = this;

    self.IdQuestion = ko.observable();
    self.IdProductCategory = ko.observable();
    self.IdProduct = ko.observable();

    self.ItemsAnswer = ko.observableArray();
}

//
function LogicAnswerViewModel() {
    self.IdAnswer = ko.observable();
    self.HasNext = ko.observable();
    self.IdQuestionLink = ko.observable();
}

//Nuevo Servicio
function NewService() {
    $.blockUI({ message: "" });
    $.ajax({
        url: "/Service/Register",
        type: "get",
        data: {
            idService: null
        },
        success: function (viewHtml) {
            $("#divMain").html(viewHtml);
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
}

//Evento Click Tipo de Servicio
function ClickTypeService(IdTypeService) {
    $.blockUI({ message: "" });
    $.ajax({
        url: "/Service/GetCustomers",
        type: "get",
        data: {
            inputTypeService: IdTypeService
        },
        success: function (data) {
            if (null != data && "" != data) {
                var desData = $.parseJSON(data);
                EventHandlerLoadCustomer(IdTypeService, desData);
            }
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });

}

//Evento Click Empleado
function ClickCustomer(idTypeService, idCustomer) {
    $.blockUI({ message: "" });
    $.ajax({
        url: "/Service/GetServices",
        type: "get",
        data: {
            inputTypeService: idTypeService,
            inputCustomer: idCustomer
        },
        success: function (data) {
            if (null != data && "" != data) {
                var desData = $.parseJSON(data);
                EventHandlerLoadServices(idTypeService, idCustomer, desData);
            }
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });

}

//
function EventHandlerLoadCustomer(IdTypeService, data) {

    var ulElement = $("#ul_" + IdTypeService);
    var htmlElement = "";

    $.each(data, function (index, value) {

        htmlElement += "<li>";
        htmlElement += '<a href=# onclick=ClickCustomer("' + IdTypeService + '","' + value.Id + '")>' + value.Name + "</a>";
        htmlElement += "</li>";
    });

    ulElement.html(htmlElement);
    $.unblockUI();
}

//
function EventHandlerLoadServices(IdTypeService, IdCustomer, data) {
    $.unblockUI();
    ko.mapping.fromJS(data, {}, modelView.ItemServices);
}

//Cargar un servicio
function LoadOneService(callBack) {

    $.ajax({
        url: "/Service/GetOneService",
        type: "get",
        async: false,
        data: {
            inputIdService: IdService
        },
        success: function (data) {
            if (null != data && "" != data) {
                var desData = $.parseJSON(data);
                callBack(desData);
            }
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });

}

//Cargar Tipos de Servicio
function LoadTypeService(callback) {

    $.ajax({
        url: "/ServicesCatalog/GetAllTypeService",
        type: "get",
        async: false,
        success: function (data) {
            if (null != data && "" != data) {
                var desData = $.parseJSON(data);
                callback(desData);
            }
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });

}

function LoadCustomer(callBack) {

    $.ajax({
        url: "/ServicesCatalog/GetAllCustomersByCurrentAccount",
        type: "get",
        async: false,
        success: function (data) {
            if (null != data && "" != data) {
                callBack(data);
            }
        },
        error: function (xhr) {

        }
    });

}

//
function SaveServices() {

    var isValid = true;

    if (!isValidField("divGNameService", $("#txtNameService").val())) {
        isValid = false;
    }

    if (!isValidField("divGTypeService", $("#selTypeService").val())) {
        isValid = false;
    }

    if (!isValidField("divGCustomer", $("#selCustomer").val())) {
        isValid = false;
    }

    if (!isValidField("divGChannel", $("#selChannel").val())) {
        isValid = false;
    }

    if (isValid) {

        $.blockUI({ message: "" });

        $.ajax({
            url: "/Service/SaveService",
            type: "get",
            data: {
                inputService: ko.toJSON(modelView.Register)
            },
            success: function (data) {
                $.unblockUI();

                if (null != data && "" != data) {
                    var desData = $.parseJSON(data);

                    bootbox.alert("Registros Actualizados Satisfactoriamente");

                    EventHandlerSaveService(desData);
                }
            },
            error: function (xhr) {

            }
        });
    }
}

//
function EventHandlerSaveService(data) {
    modelView.Register = data;

    ko.cleanNode(document.getElementById("divRegisterService"));
    ko.applyBindings(modelView, document.getElementById("divRegisterService"));

    if (null != data && emptyGuid != data.Id) {
        $("#divPollTitle").show();
        $("#btnSavePoll").prop("disabled", false);
    } else {
        $("#divPollTitle").hide();
        $("#btnSavePoll").prop("disabled", true);
    }
}

//
function CloseService() {
    LoadPage("/Service");
}

//
function CheckPhotos(idCheck, idTextBox) {

    if ($("#" + idCheck).is(":checked")) {
        document.getElementById(idTextBox).disabled = false;
    } else {
        document.getElementById(idTextBox).disabled = true;
    }
}

//
function DeleteAnswer(inputAnswer) {
    bootbox.confirm("Esta seguro que desea eliminar la respuesta?", function (result) {

        if (result) {

            $.blockUI({ message: "" });
            $.ajax({
                url: "/Service/DeleteAnswer",
                type: "post",
                data: {
                    idAnswer: inputAnswer
                },
                success: function (data) {

                    if (data) {
                        $("#pnasr_" + inputAnswer).remove();
                    }

                    $.unblockUI();
                },
                error: function (xhr) {

                }
            });

        }
        else {

        }

    });
}

//
function SavePoll() {

    $.blockUI({ message: "" });
    $("#btnSavePoll").prop("disabled", true);

    $.ajax({
        url: "/Service/GetSections",
        type: "get",
        data: {
            idService: modelView.Register.Id
        },
        success: function (data) {

            var itemSections = $.parseJSON(data);
            modelView.Poll = new ko.observableArray();

            $.each(itemSections, function (key, value) {

                var newTempSection = new PollViewModel();

                newTempSection.IdSection = value.Id;
                newTempSection.Title = $("#inti_" + value.Id).val();
                newTempSection.Weight = $("#txtValPerSec_" + value.Id).val();
                newTempSection.IsDynamic = $("#indy_" + value.Id).is(":checked") ? 1 : 0;

                $.each(value.Questions, function (keyQuestion, valueQuestion) {
                    var questionTemp = new QuestionViewModel();

                    questionTemp.IdQuestion = valueQuestion.Id;
                    questionTemp.Title = $("#txtQuestion_" + valueQuestion.Id).val();
                    questionTemp.IdTypePoll = $("#selTypePoll_" + valueQuestion.Id).val();
                    questionTemp.Weight = $("#txtValPercentage_" + valueQuestion.Id).val();
                    questionTemp.HasPhotos = $("#chkPhoto_" + valueQuestion.Id).is(":checked") ? "Y" : "N";
                    questionTemp.CountPhotos = $("#txtNumberPhotos_" + valueQuestion.Id).val();

                    $.each(valueQuestion.QuestionDetails, function (keyAnswer, valueAnswer) {

                        var answerTemp = new AnswerViewModel();

                        answerTemp.IdAnswer = valueAnswer.Id;
                        answerTemp.Title = $("#txtAnswer_" + valueAnswer.Id).val();
                        answerTemp.Weight = $("#txtAnsValPer_" + valueAnswer.Id).val();

                        questionTemp.ItemsAnswer.push(answerTemp);
                    });

                    newTempSection.ItemsQuestion.push(questionTemp);
                });


                modelView.Poll.push(newTempSection);
            });

            $.ajax({
                url: "/Service/SavePoll",
                type: "post",
                data: {
                    inputPoll: ko.toJSON(modelView.Poll)
                },
                success: function (data) {
                    if (data) {
                        bootbox.alert("Registros Actualizados Satisfactoriamente");
                        $("#btnSavePoll").prop("disabled", false);

                    } else {
                        bootbox.alert("Error al tratar de Grabar su encuesta");
                        $("#btnSavePoll").prop("disabled", false);
                    }
                },
                error: function (xhr) {
                    bootbox.alert("Error al tratar de Grabar su encuesta");
                    $("#btnSavePoll").prop("disabled", false);
                }
            });
        },
        error: function (xhr) {
            loadSuccess = false;
            bootbox.alert("Error al tratar de cargar la encuesta");
        }
    });

    $.unblockUI();
}

function OnChangeCustomer() {
    var customerId = ko.toJSON(modelView.Register.IdCustomer);

    if (null == customerId || "" == customerId || emptyGuid == customerId) {
        modelView.ItemsChannel(data);
        return;
    }

    LoadChannel(modelView.Register.IdCustomer);
}


function LoadSection(inputServiceDetail) {
    $.ajax({
        url: "/Service/GetSection",
        type: "get",
        async: false,
        data: {
            idServiceDetail: inputServiceDetail
        },
        success: function (data) {
            $("#divPollTitle").append(data);
        },
        error: function (xhr) {

        }
    });
}

function LoadQuestion(inputQuestion, inputSection) {
    $.ajax({
        url: "/Service/GetQuestion",
        type: "get",
        async: false,
        data: {
            idQuestion: inputQuestion
        },
        success: function (data) {
            $("#body_" + inputSection).append(data);
        },
        error: function (xhr) {

        }
    });
}

function LoadAnswer(inputAnswer, inputQuestion) {
    $.ajax({
        url: "/Service/GetAnswer",
        type: "get",
        async: false,
        data: {
            idAnswer: inputAnswer
        },
        success: function (data) {
            $("#pnas_" + inputQuestion).append(data);
        },
        error: function (xhr) {

        }
    });
}

//
function DeleteService(inputService) {
    bootbox.confirm("Esta seguro que desea eliminar el servicio?", function (result) {

        if (result) {
            $.blockUI({ message: "" });

            $.ajax({
                url: "/Service/DeleteService",
                type: "post",
                data: {
                    idService: inputService
                },
                success: function (data) {
                    $.unblockUI();

                    if (data) {
                        var controlId = ko.toJS(inputService);
                        $("#" + controlId).remove();
                    } else {
                        bootbox.alert("Existio un error");
                    }
                },
                error: function (xhr) {
                }
            });

        }
    });
}

function onChangeSection(idSection) {
    var valSection = $("#sec_" + idSection).val();

    if (null != valSection && "" != valSection && emptyGuid != valSection) {
        $.ajax({
            url: "/Service/GetQuestionsBySection",
            type: "get",
            data: {
                inputSection: valSection
            },
            success: function (data) {

                if (null != data) {
                    var desData = $.parseJSON(data);

                    $("#ques_" + idSection).children().remove();

                    $("#ques_" + idSection).append("<option value='" + "' selected>" + "--Sección--" + "</option>");

                    for (var index = 0; index < desData.length; index++) {
                        $("#ques_" + idSection).append("<option value='" + desData[index].Id + "' >" + desData[index].Title + "</option>");
                    }

                }
            },
            error: function (xhr) {
            }
        });
    }
    else {
        $("#ques_" + idSection).children().remove();

        $("#ques_" + idSection).append("<option value='" + "' selected>" + "--Sección--" + "</option>");
    }
}

function checkLogicNext(idAnswer) {

    if ($("#chk_" + idAnswer).is(":checked")) {
        $("#sec_" + idAnswer).attr("disabled", true);
        $("#ques_" + idAnswer).attr("disabled", true);;
    } else {
        $("#sec_" + idAnswer).removeAttr("disabled");
        $("#ques_" + idAnswer).removeAttr("disabled");
    }

}

//
function SaveLogic() {
    $.ajax({
        url: "/Service/GestAnswerByService",
        type: "get",
        data: {
            inputService: $("#hidService").val()
        },
        success: function (data) {

            if (null != data) {
                var desData = $.parseJSON(data);

                modelView.Logic = new ko.observableArray();

                $.each(desData.ServiceDetails, function (keySection, valueSection) {



                    $.each(valueSection.Questions, function (keyQuestion, valueQuestion) {

                        var tempQuestion = new LogicViewModel();

                        tempQuestion.IdQuestion = valueQuestion.Id;
                        tempQuestion.IdProductCategory = $("#spc_" + valueQuestion.Id).val();
                        tempQuestion.IdProduct = $("#spr_" + valueQuestion.Id).val();

                        $.each(valueQuestion.QuestionDetails, function (keyAnswer, valueAnswer) {

                            var tempAnswer = new LogicAnswerViewModel();

                            tempAnswer.IdAnswer = valueAnswer.Id;
                            tempAnswer.HasNext = $("#chk_" + valueAnswer.Id).is(":checked") ? "Y" : "N";
                            tempAnswer.IdQuestionLink = $("#ques_" + valueAnswer.Id).val();

                            tempQuestion.ItemsAnswer.push(tempAnswer);
                        });

                        modelView.Logic.push(tempQuestion);
                    });

                });

                $.ajax({
                    url: "/Service/SaveLogic",
                    type: "post",
                    data: {
                        inputLogic: ko.toJSON(modelView.Logic)
                    },
                    success: function (data) {

                        if (data) {
                            bootbox.alert("Lógica de Servicio Grabado Satisfactoriamente");
                        }
                        else {
                            bootbox.alert("Error al tratar de guardar la lógica");
                        }

                    },
                    error: function (xhr) {
                        //Do Something to handle error
                    }
                });

            }
        },
        error: function (xhr) {
        }
    });
}

function onChangeProductCategory(idQuestion) {
    var valProductCategory = $("#spc_" + idQuestion).val();

    if (null != valProductCategory && "" != valProductCategory && emptyGuid != valProductCategory) {
        $.ajax({
            url: "/Service/GetProducts",
            type: "get",
            data: {
                inputProductCategory: valProductCategory
            },
            success: function (data) {

                if (null != data) {
                    var desData = $.parseJSON(data);

                    $("#spr_" + idQuestion).children().remove();

                    $("#spr_" + idQuestion).append("<option value='" + "' selected>" + "--Producto--" + "</option>");

                    for (var index = 0; index < desData.length; index++) {
                        $("#spr_" + idQuestion).append("<option value='" + desData[index].Id + "' >" + desData[index].Name + "</option>");
                    }

                }
            },
            error: function (xhr) {
            }
        });
    }
    else {
        $("#spr_" + idQuestion).children().remove();

        $("#spr_" + idQuestion).append("<option value='" + "' selected>" + "--Producto--" + "</option>");
    }
}

/*------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------Cambios finales-----------------------------------------------------
------------------------------------------------------------------------------------------------------------------------*/

function LoadServiceById(idService) {
    $.blockUI({ message: "" });
    $.ajax({
        type: "GET",
        url: "/Service/Get",
        async: false,
        data: {
            idService: idService
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            ApplyBindingRegisterService(data);
        },
        error: function () {
            alert("error");
        }
    });
}

function ApplyBindingRegisterService(data) {

    vueVM = new Vue({
        el: "#divService",
        data: {
            service: data
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
    $.blockUI({ message: "" });
    $.ajax({
        url: "/Service/Save",
        type: "post",
        data: {
            service: JSON.stringify(vueVM.$data.service)
        },
        success: function (data) {
            if (data) {
                bootbox.alert("Registros Actualizados Satisfactoriamente");
                window.location.href = "/Service/Index";
            } else {
                $.unblockUI();
                bootbox.alert("Error al tratar de Grabar su encuesta, revise los campos...");
            }
        },
        error: function (xhr) {
            $.unblockUI();
            bootbox.alert("Error al tratar de Grabar su encuesta");
        }
    });
}

function AddSection(isSectionAfter, isSectionBefore, element) {
    $.blockUI({ message: "" });
    //Caso 1: Es una nueva sección
    if (!isSectionAfter && !isSectionBefore) {

        $.get("/Service/AddSection", function (data) {
            vueVM.$data.service.ServiceDetailList.push(data);
        });

        $.unblockUI();
    }

    //Caso 2: Ingreso una Sección antes de una seccion existente
    if (!isSectionAfter && isSectionBefore) {

        $.get("/Service/AddSection", function (data) {
            var curr = $(element).data("order");
            vueVM.$data.service.ServiceDetailList.splice(curr, 0, data);
        });

        $.unblockUI();
    }

    ////Caso 3: Ingreso de una sección despues de una sección existente
    if (isSectionAfter && !isSectionBefore) {

        $.get("/Service/AddSection", function (data) {
            var curr = $(element).data("order");
            vueVM.$data.service.ServiceDetailList.splice(curr + 1, 0, data);
        });

        $.unblockUI();
    }
}

function DeleteSection(element) {
    bootbox.confirm("Esta seguro que desea eliminar la Sección?", function (result) {

        if (result) {
            var section = $(element).data("idsection");
            var curr = $(element).data("order");

            if (section !== guidEmpty) {
                vueVM.$data.service.ServiceDetailList[curr].StatusRegister = "D";
            } else {
                vueVM.$data.service.ServiceDetailList.splice(curr, 1);
            }
        }
    });
}

function AddQuestion(isQuestionAfter, isQuestionBefore, element) {
    $.blockUI({ message: "" });

    var curr = $(element).data("ordersection");

    //Caso 1: Es una nueva pregunta
    if (!isQuestionAfter && !isQuestionBefore) {

        $.get("/Service/AddQuestion", function (data) {
            vueVM.$data.service.ServiceDetailList[curr].Questions.push(data);
        });

        $.unblockUI();
    }

    //Caso 2: Ingreso una Sección antes de una seccion existente
    if (!isQuestionAfter && isQuestionBefore) {

        $.get("/Service/AddQuestion", function (data) {
            var ix = $(element).data("order");
            vueVM.$data.service.ServiceDetailList[curr].Questions.splice(ix, 0, data);
        });

        $.unblockUI();
    }

    //Caso 3: Ingreso de una sección despues de una sección existente
    if (isQuestionAfter && !isQuestionBefore) {

        $.get("/Service/AddQuestion", function (data) {
            var ix = $(element).data("order");
            vueVM.$data.service.ServiceDetailList[curr].Questions.splice(ix + 1, 0, data);
        });

        $.unblockUI();
    }
}

function DeleteQuestion(element) {
    bootbox.confirm("Esta seguro que desea eliminar la Pregunta?", function (result) {

        if (result) {

            var curr = $(element).data("ordersection");
            var ix = $(element).data("order");
            var question = $(element).data("idquestion");

            if (question !== guidEmpty) {
                vueVM.$data.service.ServiceDetailList[curr].Questions[ix].StatusRegister = "D";
            } else {
                vueVM.$data.service.ServiceDetailList[curr].Questions.splice(ix, 1);
            }
        }
    });
}

function AddAnswer(element) {

    $.blockUI({ message: "" });
    var curr = $(element).data("ordersection");
    var question = $(element).data("order");

    $.get("/Service/AddAnswer", function (data) {
        vueVM.$data.service.ServiceDetailList[curr].Questions[question].QuestionDetails.push(data);
    });

    $.unblockUI();
}

function DeleteAnswer(element) {
    bootbox.confirm("Esta seguro que desea eliminar la Respuesta?", function (result) {

        if (result) {

            var curr = $(element).data("ordersection");
            var question = $(element).data("order");
            var questionDetail = $(element).data("orderdetail");
            var answer = $(element).data("idanswer");

            if (answer !== guidEmpty) {
                vueVM.$data.service.ServiceDetailList[curr].Questions[question].QuestionDetails[questionDetail].StatusRegister = "D";
            } else {
                vueVM.$data.service.ServiceDetailList[curr].Questions[question].QuestionDetails.splice(questionDetail, 1);
            }
        }
    });
}

function AddSubSection(element, isSectionAfter, isSectionBefore) {

    var curr = $(element).data("ordersection");

    //Caso 1. Es una nueva subsección
    if (!isSectionAfter && !isSectionBefore) {
        $.get("/Service/AddSection", function (data) {
            vueVM.$data.service.ServiceDetailList[curr].Sections.push(data);
        });
    }

    //Caso 2. Subsección anterior a una existente
    if (!isSectionAfter && isSectionBefore) {
        $.get("/Service/AddSection", function (data) {
            var ix = $(element).data("order");
            vueVM.$data.service.ServiceDetailList[curr].Sections.splice(ix, 0, data);
        });
    }

    //Caso 3. Subsección posterior a una existente
    if (isSectionAfter && !isSectionBefore) {
        $.get("/Service/AddSection", function (data) {
            var ix = $(element).data("order");
            vueVM.$data.service.ServiceDetailList[curr].Sections.splice(ix + 1, 0, data);
        });
    }
}

function DeleteSubSection(element) {
    bootbox.confirm("Esta seguro que desea eliminar la Pregunta?", function (result) {

        if (result) {

            var curr = $(element).data("ordersection");
            var ix = $(element).data("order");
            var subSection = $(element).data("idsubsection");

            if (subSection !== guidEmpty) {
                vueVM.$data.service.ServiceDetailList[curr].Sections[ix].StatusRegister = "D";
            } else {
                vueVM.$data.service.ServiceDetailList[curr].Sections.splice(ix, 1);
            }
        }
    });
}

function AddSubQuestion(isQuestionAfter, isQuestionBefore, element) {
    $.blockUI({ message: "" });

    var curr = $(element).data("ordersection");
    var sect = $(element).data("ordersubsection");

    //Caso 1: Es una nueva pregunta
    if (!isQuestionAfter && !isQuestionBefore) {

        $.get("/Service/AddQuestion", function (data) {
            vueVM.$data.service.ServiceDetailList[curr].Sections[sect].Questions.push(data);
        });

        $.unblockUI();
    }

    //Caso 2: Ingreso una Sección antes de una seccion existente
    if (!isQuestionAfter && isQuestionBefore) {

        $.get("/Service/AddQuestion", function (data) {
            var ix = $(element).data("order");
            vueVM.$data.service.ServiceDetailList[curr].Sections[sect].Questions.splice(ix, 0, data);
        });

        $.unblockUI();
    }

    //Caso 3: Ingreso de una sección despues de una sección existente
    if (isQuestionAfter && !isQuestionBefore) {

        $.get("/Service/AddQuestion", function (data) {
            var ix = $(element).data("order");
            vueVM.$data.service.ServiceDetailList[curr].Sections[sect].Questions.splice(ix + 1, 0, data);
        });

        $.unblockUI();
    }
}

function DeleteSubQuestion(element) {
    bootbox.confirm("Esta seguro que desea eliminar la Pregunta?", function (result) {

        if (result) {

            var curr = $(element).data("ordersection");
            var ix = $(element).data("order");
            var question = $(element).data("idquestion");
            var sect = $(element).data("ordersubsection");

            if (question !== guidEmpty) {
                vueVM.$data.service.ServiceDetailList[curr].Sections[sect].Questions[ix].StatusRegister = "D";
            } else {
                vueVM.$data.service.ServiceDetailList[curr].Sections[sect].Questions.splice(ix, 1);
            }
        }
    });
}

function DuplicateSubSection(element) {
    var curr = $(element).data("ordersection");
    var sect = $(element).data("order");

    var subSection = $.extend(true, {}, vueVM.$data.service.ServiceDetailList[curr].Sections[sect]);
    vueVM.$data.service.ServiceDetailList[curr].Sections.push(subSection);

}

function AddSubAnswer(element) {

    $.blockUI({ message: "" });
    var curr = $(element).data("ordersection");
    var subSection = $(element).data("ordersubsection");
    var question = $(element).data("orderquestion");

    $.get("/Service/AddAnswer", function (data) {
        vueVM.$data.service.ServiceDetailList[curr].Sections[subSection].Questions[question].QuestionDetails.push(data);
    });

    $.unblockUI();
}

function DeleteSubAnswer(element) {
    bootbox.confirm("Esta seguro que desea eliminar la Respuesta?", function (result) {

        if (result) {

            var curr = $(element).data("ordersection");
            var subSection = $(element).data("ordersubsection");
            var question = $(element).data("orderquestion");
            var questionDetail = $(element).data("order");
            var answer = $(element).data("idanswer");

            if (answer !== guidEmpty) {
                vueVM.$data.service.ServiceDetailList[curr].Sections[subSection].Questions[question].QuestionDetails[questionDetail].StatusRegister = "D";
            } else {
                vueVM.$data.service.ServiceDetailList[curr].Sections[subSection].Questions[question].QuestionDetails.splice(questionDetail, 1);
            }
        }
    });
}