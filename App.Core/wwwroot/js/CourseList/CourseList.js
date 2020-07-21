$(document).ready(function () {
    PopulateSelect();
    console.log("success");
    PopulateDays(2020, 4);
});


$("#yearSelector").change(function () {
    var selectedYear = $("#yearSelector").val();
    var selectedMonth = $("#monthSelector").val();
    PopulateDays(selectedYear, selectedMonth);
});

$("#monthSelector").change(function () {
    var selectedYear = $("#yearSelector").val();
    var selectedMonth = $("#monthSelector").val();
    PopulateDays(selectedYear, selectedMonth);
});

//function that populates number of days in given month
function PopulateDays(year, month) {
    var date = new Date(year, month, 0);
    console.log(date);
    var daysInMonth = date.getDate();

    var selectedDay = $("#daySelector").val();

    $("#daySelector").empty();
    for (var i = 1; i <= daysInMonth; i++) {
        $('#daySelector').append('<option value="' + i + '">' + i + '</option>');
    }

    $(`#daySelector option[value=${selectedDay}]`).prop('selected', true);
    if (selectedDay < daysInMonth) {
        $(`#daySelector option[value=${selectedDay}]`).prop('selected', true);
    }
    else {
        $(`#daySelector option[value=${daysInMonth}]`).prop('selected', true);
    }

}
//function that populates month and yars. Arrays will be given by server
function PopulateSelect() {
    var arrayList = [
        { "Id": 2020, "Name": "2020" },
        { "Id": 2021, "Name": "2021" },
        { "Id": 2022, "Name": "2022" },
        { "Id": 2023, "Name": "2023" },
        { "Id": 2024, "Name": "2024" }
    ];

    var monthList = [
        { "Id": 1, "Name": "Jan" },
        { "Id": 2, "Name": "Feb" },
        { "Id": 3, "Name": "Mar" },
        { "Id": 4, "Name": "Apr" },
        { "Id": 5, "Name": "May" },
        { "Id": 6, "Name": "Jun" },
        { "Id": 7, "Name": "Jul" },
        { "Id": 8, "Name": "Aug" },
        { "Id": 9, "Name": "Sep" },
        { "Id": 10, "Name": "Oct" },
        { "Id": 11, "Name": "Nov" },
        { "Id": 12, "Name": "Dec" }
    ];

    for (var i = 0; i < 24; i++) {
        $('#hourSelector').append('<option value="' + i + '">' + i + '</option>');
    }

    for (var i = 0; i < 60; i++) {
        $('#minuteSelector').append('<option value="' + i + '">' + i + '</option>');
    }

    for (var i = 0; i < arrayList.length; i++) {
        $('#yearSelector').append('<option value="' + arrayList[i].Id + '">' + arrayList[i].Name + '</option>');
    }

    for (var i = 0; i < monthList.length; i++) {
        $('#monthSelector').append('<option value="' + monthList[i].Id + '">' + monthList[i].Name + '</option>');
    }
}


//function that will send the data home
function SendDataToController() {
    var selectedYear = $("#yearSelector").val();
    var selectedMonth = $("#monthSelector").val();
    var selectedDay = $("#daySelector").val();
    var selectedHour = $("#hourSelector").val();
    var selectedMinutes = $("#minuteSelector").val();

    var selectedDateString = `${selectedDay}.${selectedMonth}.${selectedYear} ${selectedHour}:${selectedMinutes}:0`;
    console.log(selectedDateString);

    var capacity = $("#maxCapacity").val();
    var location = $("#location").val();
    var courseName = $("#course-name").val();
    var description = $("#description").val();
    if ($("#isOpenedForRegistration").prop("checked")) {
        var isOpenedForRegistration = true;
    }
    else {
        var isOpenedForRegistration = false;
    }

    var numCapacity = parseInt(capacity, 10);

    var verificationToken = $('input[name="__RequestVerificationToken"]', $('#newEntry')).val();
    console.log(verificationToken);

    var creatorUserName = $('#currentUser').val();

    console.log('user: ');
    console.log(creatorUserName);

    keyData = {
        CreatedBy: creatorUserName,
        FriendlyCourseDate: selectedDateString,
        CourseName: courseName,
        CourseLocation: location,
        CourseShortDescription: description,
        CourseCurrentCapacity: 0,
        CourseMaxCapacity: numCapacity,
        IsOpened: isOpenedForRegistration
    }
    //transfers data to server
    console.log(keyData);
    SetLoadingActive(true);

    var dataWithAntiforgeryToken = $.extend(keyData, { '__RequestVerificationToken': verificationToken });
    console.log(dataWithAntiforgeryToken);

    var dataType = 'application/json; charset=utf-8';
    console.log('Submitting form...');
    $.ajax({
        type: 'POST',
        url: '/CourseCalendar/Save',
        dataType: 'json',
        contentType: dataType,
        headers: { 'RequestVerificationToken': verificationToken },
        data: JSON.stringify(keyData),
        success: function (result) {
            console.log('Data received: ');
            console.log(result);
            ClearForm();
            SetLoadingActive(false);
            $("#modal-addEntry").modal('toggle');
            window.location.reload(true);
        },
        error: function (jqXHR, textStatus, errorThrow) {
            console.log("error");
            ClearForm();
            SetLoadingActive(false);
        }
    });
}

function SetLoadingActive(IsLoading) {
    var appendElement = '<div class="loading-container"><div class="spinner-border text-primary" id="loading-spinner"></div></div>'
    if (IsLoading == false) {
        $(".loading-container").remove();
        console.log("delete");
    }
    if (IsLoading == true) {
        //append loading div
        $(".modal-body").append(appendElement);
    }

}

function ClearForm() {
    $("#maxCapacity").val("");
    $("#location").val("");
    $("#course-name").val("");
    $("#description").val("");
}

function TimeMoments() {
    var selectedYear = $("#yearSelector").val();
    var selectedMonth = $("#monthSelector").val();
    var selectedDay = $("#daySelector").val();
    var selectedHour = $("#hourSelector").val();
    var selectedMinutes = $("#minuteSelector").val();

    var selectedDateString = `${selectedDay}.${selectedMonth}.${selectedYear} ${selectedHour}:${selectedMinutes}:0`;
    console.log(selectedDateString);

    var selectedDate = new Date(selectedYear, selectedMonth, 0);
    console.log(selectedDate);

    selectedDate.setDate(selectedDay);
    selectedDate.setHours(selectedHour);
    selectedDate.setMinutes(selectedMinutes);
    console.log(selectedDate.toDateString());

    var formatDate = 20;

    var responseDate = moment(selectedDateString, 'D.M.YYYY H:m:s');
    console.log(responseDate);
}


function PerformDeleteAction(itemId) {
    console.log(itemId);
    var confirmResult = window.confirm("Are you sure?");
    console.log(confirmResult);

    if (confirmResult == true) {
        var verificationToken = $('input[name="__RequestVerificationToken"]', $('#panel-entries-list')).val();
        console.log(verificationToken);
        var data = { objId: itemId };
        console.log(data);
        $.ajax({
            url: '/CourseCalendar/Delete',
            type: "POST",
            contentType: 'application/x-www-form-urlencoded',
            headers: { 'RequestVerificationToken': verificationToken },
            data: data,
            success: function (result) {
                console.log(result);
                location.reload();
            }
        });
    }
}

function GetCourseData(itemId) {
    ChangeModalValues(true);
    var data = { objId: itemId };
    var dataType = 'application/x-www-form-urlencoded';
    $.ajax({
        type: 'GET',
        url: '/TrainCourse/GetById',
        contentType: dataType,
        data: data,
        success: function (result) {
            console.log('Data received: ');
            console.log(result);
            console.log(result.CourseLocation);
            FillForm(result);
        }
    });
}

function PerformSignonAction(itemId) {
    ChangeModalValues(true);
    var data = { objId: itemId };
    var dataType = 'application/x-www-form-urlencoded';
    $.ajax({
        type: 'GET',
        url: '/CourseCalendar/SignUserToTerm',
        contentType: dataType,
        data: data,
        success: function (result) {
            console.log('Data received: ');
            location.reload();
        }
    });
}

function PerformSignoffAction(itemId) {
    ChangeModalValues(true);
    var data = { objId: itemId };
    var dataType = 'application/x-www-form-urlencoded';
    $.ajax({
        type: 'GET',
        url: '/CourseCalendar/SignUserOffTerm',
        contentType: dataType,
        data: data,
        success: function (result) {
            console.log('Data received: ');
            location.reload();
        }
    });
}

function ChangeModalValues(IsUpdateOrNew) {
    $("#modal-submit-btn", $("#modal-addEntry")).attr("onclick", "SendUpdateToController()");
}

function FillForm(data) {
    $("#modal-addEntry").modal('toggle');
    console.log(data.courseLocation);
    console.log(data.CourseLocation);
    $("#itemId", $("#modal-addEntry")).val(data.id);
    $("#location", $("#modal-addEntry")).val(data.courseLocation);
    $("#course-name", $("#modal-addEntry")).val(data.courseName);
    $("#description", $("#modal-addEntry")).val(data.courseShortDescription);
    $("#maxCapacity", $("#modal-addEntry")).val(data.courseMaxCapacity);
}

function SendUpdateToController() {
    var selectedYear = $("#yearSelector").val();
    var selectedMonth = $("#monthSelector").val();
    var selectedDay = $("#daySelector").val();
    var selectedHour = $("#hourSelector").val();
    var selectedMinutes = $("#minuteSelector").val();

    var selectedDateString = `${selectedDay}.${selectedMonth}.${selectedYear} ${selectedHour}:${selectedMinutes}:0`;
    console.log(selectedDateString);

    var entityId = $("#itemId").val();
    var capacity = $("#maxCapacity").val();
    var location = $("#location").val();
    var courseName = $("#course-name").val();
    var description = $("#description").val();
    if ($("#isOpenedForRegistration").prop("checked")) {
        var isOpenedForRegistration = true;
    }
    else {
        var isOpenedForRegistration = false;
    }

    var numCapacity = parseInt(capacity, 10);

    var verificationToken = $('input[name="__RequestVerificationToken"]', $('#newEntry')).val();
    console.log(verificationToken);

    var creatorUserName = $('#currentUser').val();

    console.log(creatorUserName);

    entityIdInt = parseInt(entityId, 10);

    keyData = {
        Id: entityIdInt,
        CreatedBy: creatorUserName,
        FriendlyCourseDate: selectedDateString,
        CourseName: courseName,
        CourseLocation: location,
        CourseShortDescription: description,
        CourseCurrentCapacity: 0,
        CourseMaxCapacity: numCapacity,
        IsOpened: isOpenedForRegistration
    }
    //transfers data to server
    console.log(keyData);
    SetLoadingActive(true);

    var dataWithAntiforgeryToken = $.extend(keyData, { '__RequestVerificationToken': verificationToken });
    console.log(dataWithAntiforgeryToken);

    var dataType = 'application/json; charset=utf-8';
    console.log('Submitting form...');
    $.ajax({
        type: 'POST',
        url: '/CourseCalendar/Update',
        dataType: 'json',
        contentType: dataType,
        headers: { 'RequestVerificationToken': verificationToken },
        data: JSON.stringify(keyData),
        success: function (result) {
            console.log('Data received: ');
            console.log(result);
            ClearForm();
            SetLoadingActive(false);
            $("#modal-addEntry").modal('toggle');
            window.location.reload(true);
        },
        error: function (jqXHR, textStatus, errorThrow) {
            console.log("error");
            ClearForm();
            SetLoadingActive(false);
        }
    });
}