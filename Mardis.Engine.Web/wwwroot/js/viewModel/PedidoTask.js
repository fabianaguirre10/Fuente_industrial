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
        url: "/Pedidos/Get",
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
               // window.location.href = "/Task/MyTasks";
            }
        },
        error: function (error) {
            console.log(error);
            alert("Error! no se ha encontrado la tarea" + error);
            //window.location.href = "/Task/MyTasks";
        }
    });
}
function ApplyBindingTaskService(data) {
    vueVM = new Vue({
        el: "#divPoll",
        data: {
            poll: data,
            carouselIndex: -1,
            factActi: "0",
            idtaskstaus: data.IdStatusTask

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
                this.poll.BranchImages[index].UrlImage = "";

            },
            loadImg: function (index) {

                var url = this.poll.BranchImages[index].UrlImage;
                window.open(url, 'Download');

            },
            subtotales: function (index) {

                var data=0
                for (i in this.poll.PedidosItems) {
                    console.log(i )

                    data = data + this.poll.PedidosItems[i].total;
                }
                return "$" + parseFloat(data).toFixed(2);

            },
            IVA: function (index) {

                var data = 0;
                for (i in this.poll.PedidosItems) {
                  
                    if (this.poll.PedidosItems[i].articulos.iva == 1) {
                        data = data + this.poll.PedidosItems[i].total;
                    }
                   
                }
                return "$" + parseFloat(data*0.12).toFixed(2);

            },
            total: function (index) {

                var data = 0;
                var datasin = 0;
                for (i in this.poll.PedidosItems) {
                    if (this.poll.PedidosItems[i].articulos.iva == 1) {
                        data = data + this.poll.PedidosItems[i].total * 1.12;
                    } else { 

                        datasin = datasin + this.poll.PedidosItems[i].total;
                    }
                }
                return "$" + parseFloat(data + datasin).toFixed(2);

            },
            subtotalesFAC: function (index) {

                var data = 0
                for (i in this.poll.PedidosItems) {
                    console.log(i)

                    if (this.poll.PedidosItems[i].numero_factura == this.factActi) {
                        data = data + this.poll.PedidosItems[i].total;
                    }

                }
                return "$" + parseFloat(data).toFixed(2);

            },
            IVAFAC: function (index) {

                var data = 0
                for (i in this.poll.PedidosItems) {
                    console.log(i)
                    if (this.poll.PedidosItems[i].numero_factura == this.factActi) {
                        if (this.poll.PedidosItems[i].articulos.iva == 1) {
                            data = data + this.poll.PedidosItems[i].total;
                        }
                    }
                  
                }
                return "$" + parseFloat(data * 0.12).toFixed(2);

            },
            totalFAC: function (index) {

                var data = 0
                var datasin = 0
                for (i in this.poll.PedidosItems) {
                    console.log(i)

                    if (this.poll.PedidosItems[i].numero_factura == this.factActi) {
                        if (this.poll.PedidosItems[i].articulos.iva == 1) {
                            data = data + this.poll.PedidosItems[i].total * 1.12;
                        } else {

                            datasin = datasin + this.poll.PedidosItems[i].total;
                        }
                    }

                }
                return "$" + parseFloat(data + datasin).toFixed(2);

            },
            facturaModel: function( _model) {
                var a = ['modelIni'];
                for (i in _model) {
                    a.push(_model[i].numero_factura)
                }
                var unique = a.filter(onlyUnique); // returns ['a', 1, 2, '1']
                return unique.splice(1, unique.length);;
            },

            OpenModel: function (_model) {
                this.factActi = _model;
                $('#responsive').modal('show');
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

function onlyUnique(value, index, self) {
    return self.indexOf(value) === index;
}

// usage example:

function saveinfo() {
    $.blockUI({ message: "Generando Facturas..." });

    var booltak = 1;
    var statustActual = vueVM.$data.poll.IdStatusTask;
    if (vueVM.$data.idtaskstaus == "0ff1a786-a332-4252-aaec-8ad3f2db7bc9") {
        for (i in vueVM.$data.poll.PedidosItems) {
      
            if (vueVM.$data.poll.PedidosItems[i].numero_factura == null || vueVM.$data.poll.PedidosItems[i].numero_factura=="") {
                booltak = 0;
            } 

        }

        vueVM.$data.poll.IdStatusTask = "7b0d0269-1aef-4b73-9089-20e53698ff75";
    }

    if (booltak == 1) {
        $.ajax({
            url: "/Pedidos/Save",
            type: "post",
            data: {
                poll: ko.toJSON(vueVM.$data.poll),
                Comment: ko.toJSON(vueVM.$data.comment)
            },
            success: function (data) {
                if (data) {
                    store.clearAll();
                    bootbox.alert("Registros Actualizados Satisfactoriamente");

                    window.location.href = "/Task/MyTasks";
                }
                $.unblockUI();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                $.unblockUI();
            }
        });
    }
    else {
        vueVM.$data.poll.IdStatusTas = statustActual;
        alert("Debe ingresar todos los numeros de facturas para continuar")
        $.unblockUI();
    }
}

