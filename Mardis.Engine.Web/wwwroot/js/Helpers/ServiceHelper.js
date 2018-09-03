function InsertSection(sectionId) {
    $("#txtSection").val(sectionId);
}

function InsertQuestion(questionId) {
    $("#txtQuestion").val(questionId);
}

function InsertAnswer(answerId) {
    $("#txtAnswer").val(answerId);
}

function ChangeCustomer() {
    $.blockUI({ message: "" });
    var url = "/Channel/GetAllChannelsByCustomerId";
    var idCustomer = $("#selCustomer").val();
    $.getJSON(url,
        { idCustomer: idCustomer },
        function (data) {
            var items = "<option>--Seleccionar--</option>";
            $("#selChannel").empty();
            $.each(data,
                function (i, customer) {
                    items += "<option value='" + customer.Id + "'>" + customer.Name + "</option>";
                });
            $("#selChannel").html(items);
            $.unblockUI();
        });
}