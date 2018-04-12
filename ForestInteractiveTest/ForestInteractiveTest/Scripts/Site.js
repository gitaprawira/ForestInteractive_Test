$(document).on('ready', function () {
    $("#myFile").fileinput({
        showUpload: false,
        dropZoneEnabled: false,
        maxFileCount: 10,
        mainClass: "input-group-lg"
    });
});

$('#message').keyup(function () {
    var val = $(this).val();
    var reg = new RegExp("^[a-zA-Z0-9\!;:\,.\?^\*\) \(+=._ -]{20,160}$");
    var x = reg.test(val)
    if (x) {
        $('#modal').removeAttr("disabled");
        $('#regex').hide();
    } else {
        $('#modal').attr("disabled", true);
        $('#regex').show();
    }
})

$('#save').click(function () {
    var text = $('#message').val();
    console.log(text);
    $.ajax({
        url: "/Home/Check?message=" + text,
        method: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (e) {
            if (e == false) {
                var formData = new FormData();
                var totalFiles = document.getElementById("FileUpload").files.length;

                for (var i = 0; i < totalFiles; i++) {
                    var file = document.getElementById("FileUpload").files[i];

                    formData.append("FileUpload", file);
                }

                $.ajax({
                    url: "/Home/Save?message=" + text,
                    method: 'POST',
                    data: formData,
                    dataType: 'json',
                    cache: false,
                    contentType: false,
                    processData: false,
                    async: false,
                    success: function () {
                        alert("Save Success");
                    },
                    error: function () {
                        alert("Error while save to server!");
                    }
                });
            } else {
                alert("Cannot submit the same message 2 times in one day")
            }
        },
        error: function () {
            alert("Error while calling the server!");
        }
    });
    location.reload();
});