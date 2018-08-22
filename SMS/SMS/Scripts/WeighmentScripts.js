
function printOnDemand() {

    var serialNo = $("#serialNo").val();
    var date = $("#date").val();
    var vehicleNo = $("#vehicleNo").val();
    var customerName = $("#customerName").val();
    var placeOfDelivery = $("#placeOfDelivery").val();
    var grossWeight = $("#grossWeight").val();
    var tareWeight = $("#tareWeight").val();
    var netWeight = $("#netWeight").val();
    var material = $("#material").val();

    $("#pdate").html(date);
    $("#pvehicleNo").html(vehicleNo);
    $("#pcustomerName").html(customerName);
    $("#pplaceOfDelivery").html(placeOfDelivery);
    $("#pgrossWeight").html(grossWeight);
    $("#ptareWeight").html(tareWeight);
    $("#pnetWeight").html(netWeight);
    $("#pmaterial").html(material);
    $("#pserialNo").html(serialNo);

    var divToPrint = document.getElementById('DivIdToPrint');
    var newWin = window.open('', 'Print-Window');
    newWin.document.open();
    newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
    newWin.document.close();
    setTimeout(function () { newWin.close(); }, 1000);
}
function printDiv() {
    //Field settings
    var serialNo = $("#serialNo").val();
    var date = $("#date").val();
    var vehicleNo = $("#vehicleNo").val();
    var customerName = $("#customerName").val();
    var placeOfDelivery = $("#placeOfDelivery").val();
    var grossWeight = $("#grossWeight").val();
    var tareWeight = $("#tareWeight").val();
    var netWeight = $("#netWeight").val();
    var material = $("#material").val();



    $("#pdate").html(date);
    $("#pvehicleNo").html(vehicleNo);
    $("#pcustomerName").html(customerName);
    $("#pplaceOfDelivery").html(placeOfDelivery);
    $("#pgrossWeight").html(grossWeight);
    $("#ptareWeight").html(tareWeight);
    $("#pnetWeight").html(netWeight);
    $("#pmaterial").html(material);

    var serialNo = $("#serialNo").val();
    var date = $("#date").val();
    var weighmentType = $("#weighmentType").val();
    var salesMode = $("#salesMode").val();
    var unit = $("#unit").val();
    var vehicleNo = $("#vehicleNo").val();
    var driverName = $("#driverName").val();
    var customerName = $("#customerName").val();
    var placeOfDelivery = $("#placeOfDelivery").val();
    var loadType = $("#loadType").val();
    var material = $("#material").val();
    var grossWeight = $("#grossWeight").val();
    var tareWeight = $("#tareWeight").val();
    var netWeight = $("#netWeight").val();
    var rate = $("#rate").val();
    var tax = $("#tax").val();
    var rent = $("#rent").val();
    var amount = $("#amount").val();
    var netAmount = $("#netAmount").val();
    if (serialNo == "") {
        if (weighmentType == "0") {
            alert("Please select the Weighment Type");
            return false;
        }
        else if (salesMode == "0") {
            alert("Please select the Sales Mode");
            return false;
        }
        else if (unit == "0") {
            alert("Please select the Unit");
            return false;
        }
        else if (vehicleNo == null || vehicleNo == undefined || vehicleNo == "") {
            alert("Please select or enter the vehicle number");
            return false;
        }
        else if (driverName == null || driverName == undefined || driverName == "") {
            alert("Please enter the driver name");
            return false;
        }
        else if (placeOfDelivery == null || placeOfDelivery == undefined || placeOfDelivery == "") {
            alert("Please enter the Place of delivery");
            return false;
        }
        else if (customerName == null || customerName == undefined || customerName == "") {
            alert("Please select or enter the customer name");
            return false;
        }
        else {
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                cache: false,
                type: 'POST',
                data: JSON.stringify({ "serialNo": serialNo, "date": date, "weighmentType": weighmentType, "salesMode": salesMode, "unit": unit, "vehicleNo": vehicleNo, "driverName": driverName, "customerName": customerName, "placeOfDelivery": placeOfDelivery, "loadType": loadType, "material": material, "grossWeight": grossWeight, "tareWeight": tareWeight, "netWeight": netWeight, "rate": rate, "tax": tax, "rent": rent, "amount": amount, "netAmount": netAmount }),
                url: "/Weighment/SaveWeighments",
                success: function (result) {
                    $("#pserialNo").html(result[0].ReturnId);
                    var divToPrint = document.getElementById('DivIdToPrint');
                    var newWin = window.open('', 'Print-Window');
                    newWin.document.open();
                    newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
                    newWin.document.close();
                    setTimeout(function () { newWin.close(); }, 1000);
                    ShowNotification('bottom', 'right', "Empty Weighment added successfully");
                    location.reload();
                },
                error: function (data) {
                    ShowNotification('bottom', 'right', "Error. Contact Administrator");
                }
            });
        }
    }
    else {
        if (material == null || material == undefined || material == "") {
            alert("Please select or enter the Material");
            return false;
        }
        else if (netWeight == null || netWeight == undefined || netWeight == "") {
            alert("Net weight cannot be empty");
            return false;
        }
        else if (netAmount == null || netAmount == undefined || netAmount == "") {
            alert("Net Amount cannot be empty.");
            return false;
        }
        else {
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                cache: false,
                type: 'POST',
                data: JSON.stringify({ "serialNo": serialNo, "date": date, "weighmentType": weighmentType, "salesMode": salesMode, "unit": unit, "vehicleNo": vehicleNo, "driverName": driverName, "customerName": customerName, "placeOfDelivery": placeOfDelivery, "loadType": loadType, "material": material, "grossWeight": grossWeight, "tareWeight": tareWeight, "netWeight": netWeight, "rate": rate, "tax": tax, "rent": rent, "amount": amount, "netAmount": netAmount }),
                url: "/Weighment/SaveWeighments",
                success: function (result) {
                    $("#pserialNo").html(result[0].ReturnId);
                    var divToPrint = document.getElementById('DivIdToPrint');
                    var newWin = window.open('', 'Print-Window');
                    newWin.document.open();
                    newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
                    newWin.document.close();
                    setTimeout(function () { newWin.close(); }, 1000);
                    location.reload();
                    ShowNotification('bottom', 'right', "Loaded weighment updated successfully");
                },
                error: function (data) {
                    ShowNotification('bottom', 'right', "Error. Contact Administrator");
                }
            });
        }
    }
    $("#printModal").modal('hide');
}
 function TotalAmountcalc() {

        var rate = parseFloat($("#rate").val());
        var netWeight = parseFloat($("#netWeight").val());
        var totalAmount = (netWeight/1000) * rate;
        $("#amount").val(totalAmount);

        //Net amount calculation
        
        var amount = parseFloat($("#amount").val());
        var tax = parseFloat($("#tax").val());
        var rent = parseFloat($("#rent").val());
        var netAmount = amount + tax + rent;
        netAmount = isNaN(netAmount) ? 0 : netAmount;
        $("#netAmount").val(netAmount)

    }
function ReadBilledvehicles(result) {
    if (result != undefined && result != null && result != "") {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            type: 'POST',
            data: JSON.stringify({ "serialNo": result }),
            url: "/Weighment/ReadBilledvehicle",
            success: function (result) {
                $("#serialNo").val(result[0].serialNo);
                $("#date").val(result[0].dateStr);
                $("#weighmentType").val(result[0].weighmentType);
                $("#salesMode").val(result[0].salesMode);
                $("#unit").val(result[0].unit);
                $("#vehicleNo").val(result[0].vehicleNo);
                $("#driverName").val(result[0].driverName);
                $("#customerName").val(result[0].customerName);
                $("#placeOfDelivery").val(result[0].placeOfDelivery);
                $("#loadType").val("Full Load");
                $("#material").val(result[0].material);
                $("#grossWeight").val(result[0].grossWeight);
                $("#tareWeight").val(result[0].tareWeight);
                $("#netWeight").val(result[0].netWeight);
                $("#rate").val(result[0].rate);
                $("#tax").val(result[0].tax);
                $("#rent").val(result[0].rent);
                $("#amount").val(result[0].amount);
                $("#netAmount").val(result[0].netAmount);
                
                

                ////Remove disabled propoerty 
                //$('#material').prop("disabled", false);
                //$('#tax').prop("disabled", false);
                //$('#rent').prop("disabled", false);

                ////Enable disabled property 
                //$('#weighmentType').prop("disabled", true);
                //$('#salesMode').prop("disabled", true);
                //$('#unit').prop("disabled", true);
                //$('#vehicleNo').prop("disabled", true);
                //$('#driverName').prop("disabled", true);
                //$('#placeOfDelivery').prop("disabled", true);
                //$('#customerName').prop("disabled", true);
                //$('#tareWeight').prop("disabled", true);
                    
                    
            },
            error: function (data) {
                ShowNotification('bottom', 'right', "Error. Contact Administrator");
            }
        });
    }
    $("#material").focus();
}
   
$(document).ready(function () {
    $("#weighmentType").focus();
    $('.form-group').on('focusout', function () {
        $(this).removeClass('is-empty');
        $(this).addClass('is-focused');
    });
    var fullDate = new Date();
    var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : '0' + (fullDate.getMonth() + 1);
    var hours = fullDate.getHours();
    var minutes = fullDate.getMinutes();
    var ampm = (hours >= 12) ? "PM" : "AM";
    var currentDate = fullDate.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear() + " " + hours + ":" + minutes+" "+ampm;
    var Vehiclestr = "";
    var customerStr = "";
    var markup = "";
    var Materialstr = "";
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        cache: false,
        type: 'POST',
        //data: JSON.stringify({ "vehicleNo": vehicleNo }),
        url: "/Weighment/GetMasterData",
        success: function (result) {
            for (var i = 0; i < result[0].length; ++i) {
                Vehiclestr += '<option value="' + result[0][i].vehicleNo + '" />'; // Storing options in variable
            }

            var vehicleNumberList = document.getElementById("vehicleNumberList");
            vehicleNumberList.innerHTML = Vehiclestr;

            for (var i = 0; i < result[1].length; ++i) {
                customerStr += '<option value="' + result[1][i].partyName + '" />'; // Storing options in variable
            }
            for (var i = 0; i < result[2].length; ++i) {
                customerStr += '<option value="' + result[1][i].partyName + '" />'; // Storing options in variable
            }

            var customerList = document.getElementById("customerList");
            customerList.innerHTML = customerStr;

            for (var i = 0; i < result[3].length; ++i) {
                markup += "<tr><td><font style='font-size:25px;'>" + result[3][i].serialNo + "</td><td><font style='font-size:25px;'><a href='#' onclick='ReadBilledvehicles(" + result[3][i].serialNo + ")'>" + result[3][i].vehicleNo + "</a></td></tr>";
            }
            $("#emptytable table tbody").append(markup);
            for (var i = 0; i < result[4].length; ++i) {
                Materialstr += '<option value="' + result[4][i].material + '" />'; // Storing options in variable
            }
            var materailList = document.getElementById("materailList");
            materailList.innerHTML = Materialstr;
                
            
        },
        error: function (data) {
            ShowNotification('bottom', 'right', "Error. Contact Administrator");
        }
    }
        
    );
    $("#serialNo").val("");
    $("#date").val(currentDate);
    $("#weighmentType").val("0");
    $("#salesMode").val("0");
    $("#unit").val("0");
    $("#vehicleNo").val("");
    $("#driverName").val("Default");
    $("#customerName").val("");
    $("#placeOfDelivery").val("");
    $("#loadType").val("Empty");
    $("#material").val("");
    $("#grossWeight").val("");
    $("#tareWeight").val("");
    $("#netWeight").val("");
    $("#rate").val("0");
    $("#tax").val("0");
    $("#rent").val("0");
    $("#amount").val("0");
    $("#netAmount").val("0");

    $("#tax").on('input', function () {
        TotalAmountcalc();
        var tax = 0;
        var rent = 0;
        var amount = 0;
        var netAmount = 0;
         amount = parseFloat($("#amount").val());
         tax = parseFloat($("#tax").val());
         rent = parseFloat($("#rent").val());
         netAmount = amount + tax + rent;
        netAmount = isNaN(netAmount) ? 0 : netAmount;
        $("#netAmount").val(netAmount)
            
    });
    $("#rent").on('input', function () {
        TotalAmountcalc();
        var tax = 0;
        var rent = 0;
        var amount = 0;
        var netAmount = 0;
        amount = parseFloat($("#amount").val());
        tax = parseFloat($("#tax").val());
        rent = parseFloat($("#rent").val());
        netAmount = amount + tax + rent;
        netAmount = isNaN(netAmount) ? 0 : netAmount;
        $("#netAmount").val(netAmount)
           
    });
    $("#rate").on('input', function () {
        TotalAmountcalc();
        var tax = 0;
        var rent = 0;
        var amount = 0;
        var netAmount = 0;
        amount = parseFloat($("#amount").val());
        tax = parseFloat($("#tax").val());
        rent = parseFloat($("#rent").val());
        netAmount = amount + tax + rent;
        netAmount = isNaN(netAmount) ? 0 : netAmount;
        $("#netAmount").val(netAmount)

    });
    $("#amount").on('input', function () {
        TotalAmountcalc();
        var tax = 0;
        var rent = 0;
        var amount = 0;
        var netAmount = 0;
        amount = parseFloat($("#amount").val());
        tax = parseFloat($("#tax").val());
        rent = parseFloat($("#rent").val());
        netAmount = amount + tax + rent;
        netAmount = isNaN(netAmount) ? 0 : netAmount;
        $("#netAmount").val(netAmount)

    });
    $("#weighmentType").on('change', function () {
        var weghmentType = $("#weighmentType").val();
        if (weghmentType == "Sales") {
            $("#loadType").val("Empty");
        }
        else if (weghmentType == "Purchase") {
            $("#loadType").val("Full Load");
        }
        else
            $("#loadType").val("Empty");

    });
    
    $("#tareWeight").on('input', function () {
        CalculateNetWeight();
        TotalAmountcalc();
    });

   
});
function printDialogOpen() {
    $("#printModal").modal('show');
}
function getVehicleDetails()
{
    var vehicleNo = $("#vehicleNo").val();
    if (vehicleNo != "" && vehicleNo != undefined && vehicleNo != null) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            type: 'POST',
            data: JSON.stringify({ "vehicleNo": vehicleNo }),
            url: "/Weighment/GetSelectVehicleDetails",
            success: function (result) {
                $("#tareWeight").val(result[0]);
                $("#customerName").val(result[1]);
                CalculateNetWeight();
            },
            error: function (data) {
                ShowNotification('bottom', 'right', "Error. Contact Administrator");
            }
        });
    }
}
function GetMaterialRate() {
    var material = $("#material").val();
    if (material != "" && material != undefined && material != null) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            type: 'POST',
            data: JSON.stringify({ "material": material }),
            url: "/Weighment/GetSelectMaterialDetails",
            success: function (result) {
                $("#rate").val(result[0]);
                TotalAmountcalc();
            },
            error: function (data) {
                ShowNotification('bottom', 'right', "Error. Contact Administrator");
            }
        });
    }
}
function EnableMaterialDetails()
{
    var weighmentType = $("#weighmentType").val();
    if (weighmentType != null && weighmentType != undefined && weighmentType != "") {
        if (weighmentType == "Purchase" ) {
            //Remove disabled propoerty 
            $('#material').prop("disabled", false);
            $('#tax').prop("disabled", false);
            $('#rent').prop("disabled", false);
            $("#loadType").val("Full Load");
        }
        else if (weighmentType == "Sales" || weighmentType == "0") {
            $('#material').prop("disabled", true);
            $('#tax').prop("disabled", true);
            $('#rent').prop("disabled", true);
            $("#loadType").val("Empty");
        }
    }
    return false;

}
function SaveWeighment() {
    $("#printModal").modal('hide');
    var serialNo = $("#serialNo").val();
    var date =$("#date").val();
    var weighmentType =$("#weighmentType").val();
    var salesMode =$("#salesMode").val();
    var unit =$("#unit").val();
    var vehicleNo=$("#vehicleNo").val();
    var driverName=$("#driverName").val();
    var customerName = $("#customerName").val();
    var placeOfDelivery = $("#placeOfDelivery").val();
    var loadType=$("#loadType").val();
    var material= $("#material").val();
    var grossWeight= $("#grossWeight").val();
    var tareWeight=$("#tareWeight").val();
    var netWeight =$("#netWeight").val();
    var rate= $("#rate").val();
    var tax=$("#tax").val();
    var rent= $("#rent").val();
    var amount=$("#amount").val();
    var netAmount = $("#netAmount").val();
    if (serialNo == "") {
        if (weighmentType == "0") {
            alert("Please select the Weighment Type");
            return false;
        }
        else if(salesMode=="0"){
            alert("Please select the Sales Mode");
            return false;
        }
        else if (unit == "0") {
            alert("Please select the Unit");
            return false;
        }
        else if (vehicleNo == null || vehicleNo == undefined || vehicleNo=="") {
            alert("Please select or enter the vehicle number");
            return false;
        }
        else if (driverName == null || driverName == undefined || driverName == "") {
            alert("Please enter the driver name");
            return false;
        }
        else if (placeOfDelivery == null || placeOfDelivery == undefined || placeOfDelivery == "") {
            alert("Please enter the Place of delivery");
            return false;
        }
        else if (customerName == null || customerName == undefined || customerName == "") {
            alert("Please select or enter the customer name");
            return false;
        }
        else {
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                cache: false,
                type: 'POST',
                data: JSON.stringify({ "serialNo": serialNo, "date": date, "weighmentType": weighmentType, "salesMode": salesMode, "unit": unit, "vehicleNo": vehicleNo, "driverName": driverName, "customerName": customerName, "placeOfDelivery": placeOfDelivery, "loadType": loadType, "material": material, "grossWeight": grossWeight, "tareWeight": tareWeight, "netWeight": netWeight, "rate": rate, "tax": tax, "rent": rent, "amount": amount, "netAmount": netAmount }),
                url: "/Weighment/SaveWeighments",
                success: function (result) {
                    
                    location.reload();
                    ShowNotification('bottom', 'right', "Empty Weighment added successfully");
                },
                error: function (data) {
                    ShowNotification('bottom', 'right', "Error. Contact Administrator");
                }
            });
        }
    }
    else {
        if (material == null || material == undefined || material == "") {
            alert("Please select or enter the Material");
            return false;
        }
        else if (netWeight == null || netWeight == undefined || netWeight == "") {
            alert("Net weight cannot be empty");
            return false;
        }
        else if (netAmount == null || netAmount == undefined || netAmount == "") {
            alert("Net Amount cannot be empty.");
            return false;
        }
        else {
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                cache: false,
                type: 'POST',
                data: JSON.stringify({ "serialNo": serialNo, "date": date, "weighmentType": weighmentType, "salesMode": salesMode, "unit": unit, "vehicleNo": vehicleNo, "driverName": driverName, "customerName": customerName, "placeOfDelivery": placeOfDelivery, "loadType": loadType, "material": material, "grossWeight": grossWeight, "tareWeight": tareWeight, "netWeight": netWeight, "rate": rate, "tax": tax, "rent": rent, "amount": amount, "netAmount": netAmount }),
                url: "/Weighment/SaveWeighments",
                success: function (result) {
                    location.reload();
                    ShowNotification('bottom', 'right', "Loaded weighment updated successfully");
                },
                error: function (data) {
                    ShowNotification('bottom', 'right', "Error. Contact Administrator");
                }
            });
        }
    }
       
}
function ScanTareWeight() {


    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        cache: false,
        type: 'POST',
        //data: JSON.stringify({ "material": material }),
        url: "/Weighment/ScanGrossWeight",
        success: function (result) {
            grossWeightLabel = document.getElementById('grossWeightLabel');
            var trimmed = result.replace(/\b0+/g, "");
            grossWeightLabel.innerHTML = trimmed;
            $("#tareWeight").val(trimmed);
            CalculateNetWeight();
        },
        error: function (data) {
            ShowNotification('bottom', 'right', "Error. Contact Administrator");
        }
    });
    $("#tareWeight").focus();
}
function ScanGrossWeight() {

        
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        cache: false,
        type: 'POST',
        //data: JSON.stringify({ "material": material }),
        url: "/Weighment/ScanGrossWeight",
        success: function (result) {
            grossWeightLabel = document.getElementById('grossWeightLabel');
            var serialNo = $("#serialNo").val();
            var trimmed = result.replace(/\b0+/g, "");
            grossWeightLabel.innerHTML = trimmed;
            $("#grossWeight").val(trimmed);
            CalculateNetWeight();
        },
        error: function (data) {
            ShowNotification('bottom', 'right', "Error. Contact Administrator");
        }
    });
    $("#grossWeight").focus();
}
function CalculateNetWeight() {
    var grossWeight=parseFloat($("#grossWeight").val());
    var tareWeight = parseFloat($("#tareWeight").val());
    var netWeightTotal = 0;
    if (grossWeight > 0 && tareWeight > 0)
        netWeightTotal = grossWeight - tareWeight;
    $("#netWeight").val(netWeightTotal);
    TotalAmountcalc();


}





