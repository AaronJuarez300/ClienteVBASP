//
// botones
//
$('#btnLimpiar').click(function () {
    limpiar();
});

$('#btnNuevo').click(function () {
    let item = getItem();
    $.ajax({
        type: "POST",
        url: "/Home/InsertClient/",
        data: { item: JSON.stringify(item) },
        error: function (jqXHR, textStatus, errorThrown) {
            M.toast({ html: '<b>Ha ocurrido un error.</b>' });
        },
        success: function (Data) {
            if (Data.res === "OK") {
                M.toast({ html: '<b>¡Guardado correctamente!</b>' });
                $('#modal-edit').modal('close');
              
            }
        }
    });
});

function getItem() {
    let item = {
         cli_nombre1: $("#txtNombre1").val()
        , cli_nombre2: $("#txtNombre2").val().length ? $("#txtNombre2").val() : null
        , cli_apellido1: $("#txtApellido1").val()
        , cli_apellido2: $("#txtApellido2").val().length ? $("#txtApellido2").val() : null
        , cli_apellido_casada: $("#txtApellidoC").val().length ? $("#txtApellidoC").val() : null
        , cli_direccion: $("#txtDireccion").val().length ? $("#txtDireccion").val() : null
        , cli_telefono1: $("#txtTelefono1").val().length ? parseInt($("#txtTelefono1").val()) : 0
        , cli_telefono2: $("#txtTelefono2").val().length ? parseInt($("#txtTelefono2").val()) : 0
        , cli_identificacion: $("#txtDPI").val()
        , cli_fecha_nacimiento: $("#txtFecha").val()
    };
    return item;
}

function limpiar() {
    $('#txtNombre1').val('');
    $('#txtNombre2').val('');
    $('#txtApellido1').val('');
    $('#txtApellido2').val('');
    $('#txtApellidoC').val('');
    $('#txtDireccion').val('');
    $('#txtTelefono1').val('');
    $('#txtTelefono2').val('');
    $('#txtDPI').val('');
    $('#txtFecha').val('');
    
}