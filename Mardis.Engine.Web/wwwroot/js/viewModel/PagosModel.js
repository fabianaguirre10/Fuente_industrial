var aboutPage = new Vue({
    el: '#aboutPage',
    data: {
        message: 'Perfil Equipos',
        sexo: 'F'
    }
});


function LoadPagos(Idpagos) {
    idQeu = Idpagos;
    $.blockUI({ message: "cargando..." });
    $.ajax({
        type: "GET",
        url: "/Pagos/GetProfile",
        // async: false,
        data: {
            IdPago: Idpagos
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data) {
                //LoadImages(idEq)
                ApplyBindingPagos(data);


            } else {
                bootbox.alert("Error! no se ha encontrado la tarea" + error);
                //window.location.href = "/Equipment/Index";
            }
        },
        error: function (error) {
            console.log(error);
            bootbox.alert("Error! no se ha encontrado la tarea" + error);
            //window.location.href = "/Equipment/Index";
        }
    });

}


var vueVM;
function ApplyBindingPagos(data) {

    vueVM = new Vue({
        el: "#VueDataPagos",
        data: {
            poll: data

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
            moment: function (date) {

                return moment(date);
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
            ExistsImg: function (e) {
                for (i in this.poll.EquipamentImg) {
                    console.log(this.poll.EquipamentImg[i])
                    if (this.poll.EquipamentImg[i].ContentType == e.toString()) return true;
                }



            }
        }

    });

    if (vueVM.poll.Branches.Idbranch != '00000000-0000-0000-0000-000000000000') {
        var marcador = [{ LatitudeBranch: vueVM.poll.Branches.LatitudeBranch, LenghtBranch: vueVM.poll.Branches.LenghtBranch, Name: vueVM.poll.Branches.Name }];
        LoadMarkers(marcador);
    }
    $.unblockUI();
}
