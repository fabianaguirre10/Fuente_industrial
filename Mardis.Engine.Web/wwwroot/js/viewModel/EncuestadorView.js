var data_e
var coount = 1
var route_bind;
function LoadEquipment(idEq) {
    route_bind = idEq;
    $.blockUI({ message: "cargando..." });
    $.ajax({
        type: "GET",
        url: "/Campaign/GetEncuestador",
        // async: false,
        data: {
            route: idEq
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data) {
                data_e = data
                if (coount == 1) {
                    ApplyBindingEquipment();
                    GetEncuestador() 
                    console.log(coount)
                }
                if (coount > 1) {
                    vueVM.addRemoveall()

                    jQuery.each(data, function (index, value) {
                        //   
                     
                        vueVM.addlistPredio(value)
                    });
                }
                coount++;
               // $('#responsive').modal('show');
                $.unblockUI();
            } else {
                bootbox.alert("Error! no se ha encontrado la tarea" + error);
                window.location.href = "/Equipment/Index";
            }
        },
        error: function (error) {
            console.log(error);
            bootbox.alert("Error! no se ha encontrado la tarea" + error);
            window.location.href = "/Equipment/Index";
        }
    });

}
function dataEnc() {

    return data_e

}

function GetEncuestador() {

    $.ajax({
        type: "GET",
        url: "/Campaign/GetActiveEncuestador",
        // async: false,
        data: {
            id: '1'
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data) {
                vueVM.encuestadores.splice(0, 1);
                jQuery.each(data, function (index, value) {
                    //   

                    vueVM.addEncuestador(value)
                });

            } else {
                bootbox.alert("Error! no se ha encontrado la tarea" + error);
                window.location.href = "/Campaign/Route";
            }
        },
        error: function (error) {
            console.log(error);
            bootbox.alert("Error! no se ha encontrado la tarea" + error);
            window.location.href = "/Campaign/Route";
        }
    });

    return null;
}
var vueVM;
function ApplyBindingEquipment() {

    vueVM = new Vue({
        el: "#VueEcuestador",
        data: {
            poll: dataEnc(),
            imei: null,
            encuestadores: [
                { name: 'Todos', abbr: '0', id:'1', phone:'' }
            ],
        customFilter(item, queryText, itemText) {
            const hasValue = val => val != null ? val : ''
            const text = hasValue(item.name)
            const query = hasValue(queryText)
            return text.toString()
                .toLowerCase()
                .indexOf(query.toString().toLowerCase()) > -1
        }
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
           
            },
            moment: function (date) {

                return moment(date);
            },
            CloseModal: function () {
       
                $('#responsive').modal('hide');
            }, OpenModalEncuesta: function (e) {
              
                $('#responsive').modal('show');
            },

            currentSlide: function (data) {
                return currentSlide(data);
            },
            Save: function () {
                return Save();
            },
            addlistPredio: function (_model) {
                this.poll.push(_model)
            

            },

            addEncuestador: function (_model) {
           
                this.encuestadores.push(_model)
                

            },
            addRemoveall: function (_model) {
                var len = this.poll.length
            
                for (i = len - 1; i >= 0; i--) {
                    console.log("I:" + len)
                    this.poll.splice(i, 1);
                }
               
              
            },
            addItem: function () {
                SaveEnc(this.imei.id)
                var person = { Name: this.imei.abbr, Code: this.imei.id, Phone: this.imei.phone };
  
                this.poll.push(person)
            },
            addRemove: function (index,imei) {
              
                deleteEmcuestador(imei,index)
       
            },

                addRemoveIndex: function (index) {
                    console.log("Eliminar" + index)

                    this.poll.splice(index, 1);

            }
        }

    });
}
function deleteEmcuestador(id, index) {

    $.blockUI({ message: "Eliminando..." });

    $.ajax({
        url: "/Campaign/deleteEcuestador",
        type: "post",
        data: {
            route: route_bind,
            imeid: id

        },
        success: function (data) {

            if (data == '1') {
        
                vueVM.addRemoveIndex(index);
                $.unblockUI();
            }

            if (data == '-1') {
                bootbox.alert("Error en la eliminacion")
                $.unblockUI();
            }
            $.unblockUI();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $.unblockUI();
        }
    });


}

function SaveEnc(imei) {
    $.blockUI({ message: "Agregando Encuestador..." });
    $.ajax({
        type: "GET",
        url: "/Campaign/SaveEncuestador",
        // async: false,
        data: {
            route: route_bind,
            id: imei
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data == '1') {
                bootbox.alert("Se agregó correctamente el encuestador en la Ruta.")
                $.unblockUI();
            }

            if (data == '-1') {
                bootbox.alert("Error en la Encuestador")
                $.unblockUI();
            }
            $.unblockUI();
        },
        error: function (error) {
            console.log(error);
            bootbox.alert("Error! no se ha encontrado la tarea" + error);
            window.location.href = "/Campaign/Route";
        }
    });

    return null;
}


function SaveAccount(status) {
    $.blockUI({ message: "Actualizando ruta. El proceso puede tarda algunos segundos." });
    $.ajax({
        type: "GET",
        url: "/Campaign/deleteAccount",
        // async: false,
        data: {
            active: status
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data == '1') {
                bootbox.alert("Se actualizo todas las rutas.")
                $.unblockUI();
                window.location.href = "/Campaign/Route";
            }

            if (data == '-1') {
                bootbox.alert("Error al actualizar rutas")
                $.unblockUI();
            }
            $.unblockUI();
        },
        error: function (error) {
            console.log(error);
            bootbox.alert("Error! no se ha encontrado la tarea" + error);
            window.location.href = "/Campaign/Route";
        }
    });

    return null;
}