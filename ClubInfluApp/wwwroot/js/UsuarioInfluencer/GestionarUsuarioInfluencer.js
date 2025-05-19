function aprobarSolicitud() {
    var url = $("#urlActualizarEstadoInfluencer").val();
    Swal.fire({
        title: "¿Quieres guardar los cambios?",
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: "Guardar",
        denyButtonText: "No guardar"
    }).then((result) => {
        if (result.isConfirmed) {
            var estadoSeleccionado = $('#estadoUsuario').val(); ; 
            var idUsuarioInfluencer = $("#idUsuarioInlfluencer").val();

            $.ajax({
                url: url,
                type: "PUT",
                data: {
                    idUsuarioInfluencer: idUsuarioInfluencer,
                    idEstadoUsuarioInfluencer: estadoSeleccionado
                },
                success: function (response) {
                    if (response.exito) {
                        Swal.fire(response.mensaje, "", "info");
                    } else {
                        Swal.fire(response.error, "", "error");
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error al actualizar el estado:", xhr.responseText);
                    Swal.fire("Hubo un error", "", "info");
                }
            });
        } else if (result.isDenied) {
            Swal.fire("Los cambios no se guardaron", "", "info");
        }
    });
}