window.onload = function () {
    $.ajax({
        type: "GET",
        contentType: "application/json;charset=utf-8",
        url: "/Home/getClient/",
        dataType: "JSON",
        success: function (response) {
            if (response.length == 0) {
                return;
            }
            var $table = $("#resulttable");
            for (var i = 0; i < response.length; i++) {
                if (i == 0) {
                    var $tr = $("<tr></tr>");
                    for (var p in response[i]) {
                        $("<th></th>").text(p).appendTo($tr);
                    }
                    $table.append($tr)
                }
                var $tr = $("<tr></tr>");
                for (var p in response[i]) {
                    $("<td></td>").text(response[i][p]).appendTo($tr);
                }
                $table.append($tr)
            }
            /*preuba*/
           
            var table = document.getElementById("resulttable"), rIndex;
           
            for (var i = 1; i < table.rows.length; i++) {
                table.rows[i].onclick = function () {
                    rIndex = this.rowIndex;
             

                    console.log(this.cells[0].innerHTML);

                };
            }
        },
        error: function (result) {
            alert("Error");
        }
    });

    
};

$('#resulttable tr').click(function () {
    $('#resulttable tr').removeClass('selectedRow');
    $(this).addClass('selectedRow');
    $(this).find('td').click(function () {
        console.log(this);
    })
});

/*
var table = $("#resulttable"), rIndex;
console.log(document.getElementById("resulttable").rows.length);

for (var i = 1; i < table.rows.length; i++) {
    table.rows[i].onclick = function () {
        rIndex = this.rowIndex;
        console.log(rIndex);

        console.log(this.cells[0].innerHTML);
        
    };
} */

