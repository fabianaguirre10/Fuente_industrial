function GetPersonByDocument(doc, callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/Person/GetPersonByDocument",
        async: false,
        data: { document: doc },
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert("error");
        }
    });
}

//Customers
function GetAllCurrentCustomers(callback) {
    var data;

    $.ajax({
        type: "GET",
        url: "/ServicesCatalog/GetAllCustomersByCurrentAccount",
        data: {},
        async: false,
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert("error");
        }
    });
}

function GetAllChannelsByCustomer(idCustomer, callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/Channel/GetAllChannelsByCustomerId",
        data: { idCustomer: idCustomer },
        async: false,
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert("error");
        }
    });
}

function GetAllTypeBusinessByCustomer(idCustomer, callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/ServicesCatalog/GetAllTypeBusinessByCustomerId",
        async: false,
        data: { idCustomer: idCustomer },
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert("error");
        }
    });
}

function GetCustomerById(idCustomer, callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/Customer/GetCustomerById",
        async: false,
        data: { id: IdCustomer },
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert("error");
        }
    });
}
//Branches

function GetBranchById(idBranch, callback) {
    var data;
    return $.ajax({
        type: "GET",
        url: "/Branch/GetBranchById",
        async: false,
        data: {
            IdBranch: idBranch
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert("error");
        }
    });
}

//Servicios
function GetServicesByChannelId(idChannel, callback) {
    var data;

    $.ajax({
        type: "GET",
        url: "/Service/GetServicesListByChannelId",
        async: false,
        data: {
            idChannel: idChannel
        },
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert("error");
        }
    });
}

//Campaigns

function GetStatusCampaigns(callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/ServicesCatalog/GetAllStatusCampaigns",
        data: {},
        async: false,
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert("error");
        }
    });
}

//Usuarios

function GetSupervisorList(callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/User/GetSupervisorList",
        data: {},
        async: false,
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert("error");
        }
    });
}

function GetMerchantsList(callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/User/GetMerchantsList",
        async: false,
        data: {},
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function (error) {
            alert("error");
        }
    });
}