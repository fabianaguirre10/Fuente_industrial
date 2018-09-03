function GetAllChannelsByCustomerId(idCustomer, callback) {
    var data;

    $.ajax({
        url: "/Channel/GetAllChannelsByCustomerId",
        type: "get",
        async: false,
        data: {
            idCustomer: idCustomer
        },
        success: function (resp) {
            debugger;
            if (null != resp && "" !== resp) {
                data = resp;
                callback(data);
            }
        },
        error: function (xhr) {
            alert(xhr);
        }
    });
}