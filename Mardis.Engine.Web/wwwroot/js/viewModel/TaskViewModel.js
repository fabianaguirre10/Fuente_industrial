guidEmpty = "00000000-0000-0000-0000-000000000000";

var vueVM;
var vueVMS;
var idTask = "";
var espera = 0;

function LoadTaskById(idTask) {
    idTask = idTask;
    $.blockUI({ message: "cargando..." });
    $.ajax({
        type: "GET",
        url: "/Task/Get",
        // async: false,
        data: {
            idTask: idTask
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

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
Vue.directive('info-sender', {
    bind: function (el, binding, vnode) {
        el.addEventListener("change", function () {
            if (store.get(el.id) == null)
                store.set(el.id, { Idquestion: el.id, AnswerQuestion: el.value, idTask: getParameterByName('idTask'), idanswer: el.name, estado: "P" });
            else
                if (store.get(el.id).idanswer == "00000000-0000-0000-0000-000000000000" || store.get(el.id).idanswer == "")
                    store.set(el.id, { Idquestion: el.id, AnswerQuestion: el.value, idTask: getParameterByName('idTask'), idanswer: el.name, estado: "P" });
                else
                    store.set(el.id, { Idquestion: el.id, AnswerQuestion: el.value, idTask: getParameterByName('idTask'), idanswer: store.get(el.id).idanswer, estado: "P" });
            //alert(el.value);
            let storeSize = 0;
            store.each(function (value, key) {
                console.log(key, '==', value)
                if (key.estado == "P") {
                    storeSize++;
                }
            })
            if (storeSize >= 2 && espera == 0) {
                //    //preparar
                espera = 1;
                let infoList = [];
                let data = store.getAll();
                for (var property in data) {
                    if (data.hasOwnProperty(property)) {
                        if (data[property].estado == "P")
                            infoList.push(data[property]);
                    }

                }
                $.ajax({
                    url: "/Task/SaveAnswerQuestion",
                    type: "post",
                    data: {
                        AnswerQuestion: ko.toJSON(infoList),
                        fintransaccion: "no",
                        Idtask: getParameterByName('idTask')
                    },
                    success: function (data) {
                        if (data) {
                            for (var d in data) {
                                store.set(data[d].Idquestion, { Idquestion: data[d].Idquestion, AnswerQuestion: data[d].AnswerQuestion, idTask: data[d].idTask, idanswer: data[d].idAnswer, estado: data[d].estado });
                            }
                            espera = 0;

                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {

                    }
                });


            }


        });
    }
});

Vue.directive('for-events', {
    bind: function (el, binding, vnode) {
        el.addEventListener("change", function () {
            $.ajax({
                url: '/Task/SaveAnswerQuestionMultiple',
                type: "POST",
                content: "application/json; charset=utf-8",
                data: {
                    id: el.id,
                    value: el.value,
                    idanswer: el.name
                    , Idtask: getParameterByName('idTask')
                    , idstatus: vueVM.$data.poll.IdStatusTask
                },
                success: function (data) {

                },

                error: function () {


                }
                ,
                async: true, // La petición es síncrona
            });
        });

    }
      
});


function onToggle(question) {
 
    //$.ajax({
    //    url: '/Task/SaveAnswerQuestionMultiple',
    //    type: "POST",
    //    content: "application/json; charset=utf-8",
    //    data: {
    //        id: question.id,
    //        value: question.value,
    //        idanswer: question.name
    //        , Idtask: getParameterByName('idTask')
    //    },
    //    success: function (data) {

    //    },

    //    error: function () {


    //    }
    //    ,
    //    async: true, // La petición es síncrona
    //});
}
function ApplyBindingTaskService(data) {
    vueVM = new Vue({
        el: "#divPoll",
        data: {
            poll: data,
            carouselIndex:-1
        },
        methods: {
            keymonitor: function (event) {
                var charCode = (event.which) ? event.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                console.log(charCode);
                return true;
            },
            changeHandler: function (event) {
                // change of userinput, do something
                alert(event.id);
            },
            moment: function () {
                return moment();
            },
            openModal: function () {
                return openModal();
            },
            currentSlide: function (data) {
                return currentSlide(data);
            },
            Save: function () {
                return Save();
            },
            addRemoveIndex: function (index) {
                this.poll.BranchImages[index].UrlImage="";

            },
            loadImg: function (index) {
        
                var url = this.poll.BranchImages[index].UrlImage;
                window.open(url, 'Download');  

            },
        onFileChange(e, index) {
            console.log(e.target.files)
            var files = e.target.files || e.dataTransfer.files;
            if (!files.length)
                return;
            this.createImage(files[0], index);
        },
        createImage(file, index) {
            var image = new Image();
            var reader = new FileReader();
         
            reader.onload = (e) => {
                this.poll.BranchImages[index].UrlImage = e.target.result;
                updateBranchImg(this.poll.BranchImages[index].Id, this.poll.BranchImages[index].UrlImage)
            };
            reader.readAsDataURL(file);
            console.log(this.poll.BranchImages[index])
         
        }
        }
    });



    $.unblockUI();
}

function BuscarPregunta(servicio, idpregunta) {
    if (servicio != null) {
        var preguntas = servicio.ServiceDetailCollection[s];
        var seccion = servicio.ServiceDetailCollection[s].Sections;
        if (preguntas != null) {
            for (var p = 0; p <= preguntas.QuestionCollection.length - 1; p++) {
                var preguntasopciones = preguntas.QuestionCollection[p].QuestionDetailCollection;

                if (preguntas.QuestionCollection[p].CodeTypePoll == "ONE") {
                    if (preguntas.QuestionCollection[p].IdQuestionDetail == "00000000-0000-0000-0000-000000000000") {
                        return "vacia";
                    }
                } else {
                    if (preguntas.QuestionCollection[p].Answer == null) {
                        return "vacia";
                    }
                }
                if (seccion != null) {
                    for (var sec = 0; sec <= seccion.length - 1; sec++) {
                        var preguntasSec = seccion[sec].QuestionCollection;
                        for (var pc = 0; pc <= preguntasSec.length - 1; pc++) {
                            if (preguntasSec[pc].AnswerRequired == true) {
                                if (preguntasSec[pc].CodeTypePoll == "ONE") {
                                    if (preguntasSec[pc].IdQuestionDetail == "00000000-0000-0000-0000-000000000000") {
                                        return "vacia";
                                    }
                                } else {
                                    if (preguntasSec[pc].Answer == null) {
                                        return "vacia";
                                    }
                                }
                            }

                        }

                    }
                }
            }
        }
    }
}
function ValidarPreguntas() {
    var mensaje = "";
    for (var i = 0; i <= vueVM.$data.poll.ServiceCollection.length - 1; i++) {

        var servicio = vueVM.$data.poll.ServiceCollection[i];
        if (servicio != null) {
            for (var s = 0; s <= servicio.ServiceDetailCollection.length - 1; s++) {
                var preguntas = servicio.ServiceDetailCollection[s];
                var seccion = servicio.ServiceDetailCollection[s].Sections;
                if (preguntas != null) {
                    for (var p = 0; p <= preguntas.QuestionCollection.length - 1; p++) {
                        if (preguntas.QuestionCollection[p].AnswerRequired == true) {
                            if (preguntas.QuestionCollection[p].CodeTypePoll == "ONE") {
                                if (preguntas.QuestionCollection[p].IdQuestionDetail == "00000000-0000-0000-0000-000000000000") {
                                    mensaje = mensaje + "\n" + preguntas.QuestionCollection[p].Title;
                                    $("#" + preguntas.QuestionCollection[p].Id).focus();
                                } else {
                                    if (preguntas.QuestionCollection[p].IdQuestionRequired != null && preguntas.QuestionCollection[p].IdQuestionRequired != '') {

                                        var str = preguntasopciones[qd].IdQuestionRequired;
                                        if (str.indexOf("&") > -1) {
                                            var res = str.split("&");
                                            var validalogica = "";
                                            for (var j = 0; j < res.length; j++) {
                                                if (res[j] != '') {
                                                    var idque = res[j]
                                                    validalogica = BuscarPregunta(servicio.ServiceDetailCollection[s], res[j])
                                                }

                                            }
                                            if (validalogica != "") {
                                                mensaje = mensaje + "\n Seleccione una opcion de " + servicio.ServiceDetailCollection[s].Title;
                                                $("#" + preguntas.QuestionCollection[p].Id).focus();
                                            }
                                        }
                                    }
                                }
                            } else {
                                if (preguntas.QuestionCollection[p].Answer == null) {
                                    mensaje = mensaje + "\n" + preguntas.QuestionCollection[p].Title;
                                    $("#" + preguntas.QuestionCollection[p].Id).focus();
                                }
                            }
                        }
                    }
                }
                if (seccion != null) {
                    for (var sec = 0; sec <= seccion.length - 1; sec++) {
                        var preguntasSec = seccion[sec].QuestionCollection;
                        for (var pc = 0; pc <= preguntasSec.length - 1; pc++) {
                            if (preguntasSec[pc].AnswerRequired == true) {
                                if (preguntasSec[pc].CodeTypePoll == "ONE") {
                                    if (preguntasSec[pc].IdQuestionDetail == "00000000-0000-0000-0000-000000000000") {
                                        mensaje = mensaje + "\n" + preguntasSec[pc].Title + " " + seccion[sec].SectionTitle;
                                        $("#" + preguntasSec[pc].Id).focus();
                                    } else {
                                        if (preguntasSec[pc].IdQuestionRequired != null && preguntasSec[pc].IdQuestionRequired != '') {

                                            var str = preguntasSec[pc].IdQuestionRequired;
                                            if (str.indexOf("&") > -1) {
                                                var res = str.split("&");
                                                var validalogica = "";
                                                for (var j = 0; j < res.length; j++) {
                                                    if (res[j] != '') {
                                                        var idque = res[j]
                                                        validalogica = BuscarPregunta(seccion[sec], res[j])
                                                    }

                                                }
                                                if (validalogica != "") {
                                                    mensaje = mensaje + "\n Seleccione una opcion de " + seccion[sec].Title;
                                                    $("#" + preguntasSec[pc].Id).focus();
                                                }
                                            }
                                        }
                                    }
                                } else {
                                    if (preguntasSec[pc].Answer == null) {
                                        mensaje = mensaje + "\n" + preguntasSec[pc].Title + " " + seccion[sec].SectionTitle;
                                        $("#" + preguntasSec[pc].Id).focus();
                                    }
                                }
                            }

                        }

                    }
                }

            }
        }

    }
    return mensaje;
}

function Save() {
    var imgsrc = 'http://www.google.es/intl/en_com/images/logo_plain.png';
    var img = new Image();
    console.log()
    img.onerror = function () {
        alert("No hay conexion a internet.");
    }
    img.onload = function () {
        //alert("Hay conexion a internet.");
    }
    img.src = imgsrc;
    $.blockUI({ message: "" });
    var resulvalidacionC = "";
    var resulvalidacion = "";
    resulvalidacionC = ValidarPreguntas();
    if (resulvalidacionC != "") {
        resulvalidacion = "Llenar Campos Obligatorios..!!" + "\n" + resulvalidacionC;
    }
    if (resulvalidacion == "") {
        if (navigator.onLine) {

            let infoList = [];
            let data = store.getAll();
            for (var property in data) {
                if (data.hasOwnProperty(property)) {
                    if (data[property].estado == "P" || data[property].estado == "E")
                        infoList.push(data[property]);
                }

            }
            var a = infoList;

            $.ajax({
                url: "/Task/SaveAnswerQuestion",
                type: "post",
                data: {
                    AnswerQuestion: ko.toJSON(infoList),
                    fintransaccion: "ok",
                    Idtask: getParameterByName('idTask')
                    , idstatus: vueVM.$data.poll.IdStatusTask
                    , CodigoGemini: vueVM.$data.poll.CodeGemini
                },
                success: function (data) {
                    if (data) {
                        store.clearAll();
                        bootbox.alert("Registros Actualizados Satisfactoriamente");

                        window.location.href = "/Task/MyTasks";
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {

                }
            });

            //$.ajax({
            //    url: "/Task/Save",
            //    type: "post",
            //    data: {
            //        task: ko.toJSON(vueVM.$data.poll)
            //    },
            //    success: function (data) {
            //        if (data) {
            //            bootbox.alert("Registros Actualizados Satisfactoriamente");
            //            window.location.href = "/Task/MyTasks";
            //            alert("Error! no se ha encontrado la tarea" + data);
            //        } else {
            //            bootbox.alert("Error al tratar de Grabar su encuesta");
            //            window.location.href = "/Task/MyTasks";
            //            alert("Error! no se ha encontrado la tarea" + data);
            //        }
            //    },
            //    error: function (xhr, ajaxOptions, thrownError) {
            //        bootbox.alert("Error al tratar de Grabar su encuesta " + thrownError);
            //        window.location.href = "/Task/MyTasks";
            //        $.unblockUI();
            //    }
            //});
            //window.location.href = "/Task/MyTasks";
        } else {
            alert('Sin Conexión...Intente mas tarde.')
            $.unblockUI();
        }
    } else {
        alert(resulvalidacion);
        $.unblockUI();
    }
}

function ChangeStatusNotImplemented(element) {
    $.blockUI({ message: "" });
    var idTask = $(element).data("idtask");
    var idReason = $("#ddlReasons").val();
    var comment = $("#txtObservation").val();
    $.ajax({
        url: "/Task/ChangeStatusNotImplemented",
        type: "POST",
        data: {
            idTask: idTask,
            idReason: idReason,
            comment: comment
        },
        success: function (data) {

            if (data) {
                bootbox.alert("Registros Actualizados Satisfactoriamente");
                window.location.href = "/Task/MyTasks";
            } else {
                bootbox.alert("Existío un error, Vuelva a intentarlo");
            }

        },
        complete: function (data) {
            $.unblockUI();
        },
        error: function (error) {
            console.log(error);
            $.unblockUI();
        }
    });
}
function AddSection(isSectionAfter, isSectionBefore, element) {
    $.blockUI({ message: "" });
    //Caso 1: Es una nueva sección
    if (!isSectionAfter && !isSectionBefore) {

        $.get("/Service/AddSection", function (data) {
            var curr = $(element).data("ordersection");
            var sect = $(element).data("order");
            var Section = $.extend(true, {}, vueVM.$data.poll.ServiceCollection[0].ServiceDetailCollection[sect]);
            vueVM.$data.poll.ServiceCollection[0].ServiceDetailCollection.push(Section);
        });

        $.unblockUI();
    }

    //Caso 2: Ingreso una Sección antes de una seccion existente
    if (!isSectionAfter && isSectionBefore) {

        $.get("/Service/AddSection", function (data) {
            var curr = $(element).data("order");
            vueVM.$data.poll.ServiceCollection[0].ServiceDetailCollection.push(data);
        });

        $.unblockUI();
    }

    ////Caso 3: Ingreso de una sección despues de una sección existente
    if (isSectionAfter && !isSectionBefore) {

        $.get("/Service/AddSection", function (data) {
            var curr = $(element).data("order");
            vueVM.$data.poll.ServiceCollection[0].ServiceDetailCollection.push(data);
        });

        $.unblockUI();
    }




}
function ObtenerPregunta(idquestiondetail, idquestion) {
    for (var i = 0; i <= vueVM.$data.poll.ServiceCollection.length - 1; i++) {

        var servicio = vueVM.$data.poll.ServiceCollection[i];
        if (servicio != null) {
            for (var s = 0; s <= servicio.ServiceDetailCollection.length - 1; s++) {
                var preguntas = servicio.ServiceDetailCollection[s];
                var seccion = servicio.ServiceDetailCollection[s].Sections;
                if (preguntas != null) {
                    for (var p = 0; p <= preguntas.QuestionCollection.length - 1; p++) {
                        if (preguntas.QuestionCollection[p].Id == idquestion) {
                            var preguntasopciones = preguntas.QuestionCollection[p].QuestionDetailCollection
                            for (var qd = 0; qd <= preguntasopciones.length; qd++) {
                                if (preguntasopciones[qd].Id == idquestiondetail) {

                                    if (preguntasopciones[qd].IdQuestionLink != null) {
                                        $("#" + preguntasopciones[qd].IdQuestionLink).focus();
                                        if (preguntasopciones[qd].IdQuestionRequired != null && preguntasopciones[qd].IdQuestionRequired != '') {
                                            ColocarObligatorios(preguntasopciones[qd].IdQuestionRequired, idquestion);
                                        } else {
                                            LimpiarObligatorias(idquestion, preguntasopciones[qd].Id);
                                        }
                                        return;
                                    }

                                }
                            }
                        }

                    }
                }
                if (seccion != null) {
                    for (var sec = 0; sec <= seccion.length - 1; sec++) {
                        var preguntasSec = seccion[sec].QuestionCollection;
                        for (var pc = 0; pc <= preguntasSec.length - 1; pc++) {
                            if (preguntasSec[pc].Id == idquestion) {
                                var preguntasopciones = preguntasSec[pc].QuestionDetailCollection
                                for (var qd = 0; qd <= preguntasopciones.length; qd++) {
                                    if (preguntasopciones[qd].Id == idquestiondetail) {

                                        if (preguntasopciones[qd].IdQuestionLink != null) {
                                            $("#" + preguntasopciones[qd].IdQuestionLink).focus();
                                            if (preguntasopciones[qd].IdQuestionRequired != null && preguntasopciones[qd].IdQuestionRequired != '') {
                                                ColocarObligatorios(preguntasopciones[qd].IdQuestionRequired, idquestion);
                                            } else {
                                                LimpiarObligatorias(idquestion, preguntasopciones[qd].Id);
                                            }
                                            return;
                                        }

                                    }
                                }
                            }
                        }

                    }
                }

            }
        }
    }




}
function ColocarObligatorios(idquestionobligatorias, idquestiondetail) {
    var str = idquestionobligatorias;
    if (str.indexOf("&") == -1) {
        var res = str.split("|");
        for (var j = 0; j < res.length; j++) {
            for (var i = 0; i <= vueVM.$data.poll.ServiceCollection.length - 1; i++) {

                var servicio = vueVM.$data.poll.ServiceCollection[i];
                if (servicio != null) {
                    for (var s = 0; s <= servicio.ServiceDetailCollection.length - 1; s++) {
                        var preguntas = servicio.ServiceDetailCollection[s];
                        var seccion = servicio.ServiceDetailCollection[s].Sections;
                        if (preguntas != null) {
                            for (var p = 0; p <= preguntas.QuestionCollection.length - 1; p++) {

                                if (preguntas.QuestionCollection[p].Id == res[j].toLowerCase()) {

                                    preguntas.QuestionCollection[p].AnswerRequired = true;
                                }


                            }

                        }

                        if (seccion != null) {
                            for (var sec = 0; sec <= seccion.length - 1; sec++) {
                                var preguntasSec = seccion[sec].QuestionCollection;
                                for (var pc = 0; pc <= preguntasSec.length - 1; pc++) {
                                    if (preguntasSec[pc].Id == res[j].toLowerCase()) {

                                        preguntasSec[pc].AnswerRequired = true;
                                    }
                                }

                            }
                        }

                    }
                }
            }
        }
    }
}

function LimpiarObligatorias(idquestion, idquestiondetail) {
    for (var i = 0; i <= vueVM.$data.poll.ServiceCollection.length - 1; i++) {

        var servicio = vueVM.$data.poll.ServiceCollection[i];
        if (servicio != null) {
            for (var s = 0; s <= servicio.ServiceDetailCollection.length - 1; s++) {
                var preguntas = servicio.ServiceDetailCollection[s];
                var seccion = servicio.ServiceDetailCollection[s].Sections;
                if (preguntas != null) {
                    for (var p = 0; p <= preguntas.QuestionCollection.length - 1; p++) {
                        if (preguntas.QuestionCollection[p].Id == idquestion) {
                            var preguntasopciones = preguntas.QuestionCollection[p].QuestionDetailCollection
                            for (var qd = 0; qd <= preguntasopciones.length; qd++) {
                                if (preguntasopciones[qd] != undefined) {
                                    if (preguntasopciones[qd].Id != idquestiondetail) {

                                        if (preguntasopciones[qd].IdQuestionRequired != null && preguntasopciones[qd].IdQuestionRequired != '') {

                                            var str = preguntasopciones[qd].IdQuestionRequired;
                                            var res = str.split("|");
                                            for (var j = 0; j < res.length; j++) {
                                                var idque = res[j]
                                                limpiarQuestion(idque);

                                            }
                                        }

                                    }

                                }
                            }
                        }

                    }
                }
                if (seccion != null) {
                    for (var sec = 0; sec <= seccion.length - 1; sec++) {
                        var preguntasSec = seccion[sec].QuestionCollection;
                        for (var pc = 0; pc <= preguntasSec.length - 1; pc++) {
                            if (preguntasSec[pc].Id == idquestion) {
                                var preguntasopciones = preguntasSec[pc].QuestionDetailCollection
                                for (var qd = 0; qd <= preguntasopciones.length; qd++) {
                                    if (preguntasopciones[qd] != undefined) {
                                        if (preguntasopciones[qd].Id != idquestiondetail) {

                                            if (preguntasopciones[qd].IdQuestionRequired != null && preguntasopciones[qd].IdQuestionRequired != '') {
                                                var str = preguntasopciones[qd].IdQuestionRequired;
                                                var res = str.split("|");
                                                for (var j = 0; j < res.length; j++) {
                                                    var idque = res[j]
                                                    limpiarQuestion(idque);

                                                }

                                            }

                                        }
                                    }
                                }
                            }
                        }

                    }
                }

            }
        }
    }

}
function limpiarQuestion(idquestion) {
    for (var i = 0; i <= vueVM.$data.poll.ServiceCollection.length - 1; i++) {

        var servicio = vueVM.$data.poll.ServiceCollection[i];
        if (servicio != null) {
            for (var s = 0; s <= servicio.ServiceDetailCollection.length - 1; s++) {
                var preguntas = servicio.ServiceDetailCollection[s];
                var seccion = servicio.ServiceDetailCollection[s].Sections;
                if (preguntas != null) {
                    for (var p = 0; p <= preguntas.QuestionCollection.length - 1; p++) {
                        if (preguntas.QuestionCollection[p].Id == idquestion.toLowerCase()) {

                            preguntas.QuestionCollection[p].AnswerRequired = false;
                        }


                    }

                }
            }
            if (seccion != null) {
                for (var sec = 0; sec <= seccion.length - 1; sec++) {
                    var preguntasSec = seccion[sec].QuestionCollection;
                    for (var pc = 0; pc <= preguntasSec.length - 1; pc++) {
                        if (preguntasSec[pc].Id == idquestion.toLowerCase()) {
                            preguntasSec[pc].AnswerRequired = false;
                        }
                    }

                }
            }

        }
    }

}


function updateBranchImg(id, img) {
    $.blockUI({ message: "Actualizando Imagen.." });

    $.ajax({
        url: "/Task/ChangeImage",
        type: "POST",
        data: {
            idIdimg: id,
            imgdata: img
        },
        success: function (data) {

            if (data=="1") {
                bootbox.alert("Registro Actualizado Satisfactoriamente");
          
            } else {
                bootbox.alert("Existío un error, Vuelva a intentarlo");
            }

        },
        complete: function (data) {
            $.unblockUI();
        },
        error: function (error) {
            console.log(error);
            $.unblockUI();
        }
    });
}


function deleteBranchImg(id) {
    $.blockUI({ message: "Actualizando Imagen.." });

    $.ajax({
        url: "/Task/DeleteImage",
        type: "POST",
        data: {
            idIdimg: id
        },
        success: function (data) {

            if (data == "1") {
                bootbox.alert("Foto ha sido eliminada");

            } else {
                bootbox.alert("Existío un error, Vuelva a intentarlo");
            }

        },
        complete: function (data) {
            $.unblockUI();
        },
        error: function (error) {
            console.log(error);
            $.unblockUI();
        }
    });
}