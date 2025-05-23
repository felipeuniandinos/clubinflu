$(document).ready(function () {
    function inicializarSelectEstado(paisSelector, estadoSelector) {
        $(estadoSelector).select2({
            placeholder: "Escribe para buscar",
            minimumInputLength: 2,
            ajax: {
                transport: function (params, success, failure) {
                    $.ajax({
                        type: 'GET',
                        url: '/UsuarioInfluencer/ObtenerEstadosPorPaisYTermino',
                        data: {
                            termino: params.data.term,
                            idPais: $(paisSelector).val()
                        },
                        dataType: 'json',
                        success: function (response) {
                            if (response.exito) {
                                success({
                                    results: $.map(response.data, function (estado) {
                                        return {
                                            id: estado.idEstado,
                                            text: estado.estado
                                        };
                                    })
                                });
                            } else {
                                Swal.fire(response.error, "", "error");
                            }
                        },
                        error: function (xhr, status, error) {
                            console.error("Error en la carga de estados:", error);
                            failure();
                        }
                    });
                }
            }
        });

        $(paisSelector).on('change', function () {
            $(estadoSelector).val(null).trigger('change');
        });
    }

    inicializarSelectEstado("#pais1", "#idEstado");

});

function reservarOfertaServicio(idOferta) {
    var url = $("#urlReservarCuponOfertaServicio").val();

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success mx-3",
            cancelButton: "btn btn-danger mx-3",
            actions: "d-flex justify-content-center gap-3 mt-3"
        },
        buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
        title: "Condiciones para redimir el cupón",
        html: `
                <ul class="text-start">
                    <li><strong>YouTube</strong> – 1 video</li>
                    <li><strong>Otros (10K - 40K seguidores)</strong> – 3 historias</li>
                    <li><strong>Otros (40K - 100K seguidores)</strong> – 2 historias</li>
                    <li><strong>Otros (>100K seguidores)</strong> – 1 historia</li>
                    <li><strong>En el video mostrar la empresa y nosotros</strong></li>
                </ul>
                <p>¿Deseas continuar con la reservación?</p>
            `,
        icon: "info",
        showCancelButton: true,
        confirmButtonText: "Sí, continuar",
        cancelButtonText: "Cancelar",
        reverseButtons: true
    }).then((firstResult) => {
        if (firstResult.isConfirmed) {
            swalWithBootstrapButtons.fire({
                title: "¿Estás completamente seguro?",
                text: "Esta acción reservará el cupón y se aplicarán las condiciones.",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Sí, reservar",
                cancelButtonText: "Volver",
                reverseButtons: true
            }).then((secondResult) => {
                if (secondResult.isConfirmed) {
                    $.ajax({
                        url: url,
                        type: "PUT",
                        data: {
                            idOfertaServicio: idOferta
                        },
                        success: function (response) {
                            if (response.exito) {
                                swalWithBootstrapButtons.fire({
                                    title: "Cupón reservado",
                                    text: response.mensaje,
                                    icon: "success"
                                });
                            } else {
                                swalWithBootstrapButtons.fire({
                                    title: "Hubo un error - Reservación cancelada",
                                    text: response.error,
                                    icon: "info"
                                });
                            }
                        },
                        error: function (xhr, status, error) {
                            console.error("Error al actualizar el estado:", xhr.responseText);
                            swalWithBootstrapButtons.fire({
                                title: "Hubo un error - Reservación cancelada",
                                text: "No se reservó el cupón.",
                                icon: "info"
                            });
                        }
                    });
                } else {
                    swalWithBootstrapButtons.fire({
                        title: "Reservación cancelada",
                        text: "No se reservó el cupón.",
                        icon: "info"
                    });
                }
            });
        } else {
            swalWithBootstrapButtons.fire({
                title: "Reservación cancelada",
                text: "No se reservó el cupón.",
                icon: "info"
            });
        }
    });
}