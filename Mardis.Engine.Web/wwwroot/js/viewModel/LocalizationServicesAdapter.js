function GetAllCountries(callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/ServicesLocalization/GetAllCountries",
        data: {},
        async: false,
        success: function (resp) {
            data = resp;
            callback(data);
        },
        error: function () { }
    });
}

function GetProvincesByIdCountry(idCountry, callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/ServicesLocalization/GetProvincesByCountryId",
        async: false,
        data: {
            countryId: idCountry
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

function GetDistrictsByIdProvince(idProvince, callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/ServicesLocalization/GetDistrictsByProvinceId",
        async: false,
        data: {
            idProvince: idProvince
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

function GetParishesByIdDistrict(idDistrict, callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/ServicesLocalization/GetParishesByDistrictId",
        async: false,
        data: {
            idDistrict: idDistrict
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

function GetSectorsByIdDistrict(idDistrict, callback) {
    var data;
    $.ajax({
        type: "GET",
        url: "/ServicesLocalization/GetSectorsByDistrictId",
        async: false,
        data: {
            idDistrict: idDistrict
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