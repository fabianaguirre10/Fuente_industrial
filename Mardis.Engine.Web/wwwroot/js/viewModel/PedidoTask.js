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
            carouselIndex: -1
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