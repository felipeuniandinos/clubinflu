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
    ////
    $(".btn-reservar-cupon").on("click", function (e) {
        e.preventDefault();
        var url = $("#urlReservarCuponOfertaServicio").val();

        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-success mx-3",
                cancelButton: "btn btn-danger mx-3",
                actions: "d-flex justify-content-center gap-3 mt-3"
            },
            buttonsStyling: false
        });

        // 🔁 Primera validación: mostrar condiciones
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
                // ✅ Segunda validación
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
                        swalWithBootstrapButtons.fire({
                            title: "Cupón reservado",
                            text: "Se han aplicado las condiciones correctamente.",
                            icon: "success"
                        });
                        /////
                        $.ajax({
                            url: url,
                            type: "PUT",
                            data: {
                                idUsuarioInfluencer: 1,
                                idCuponServicio: 20,
                                idestadocupon: 2,
                                fecharedencion = '2025-05-21'
                            },
                            success: function (response) {
                                if (response.exito) {
                                    Swal.fire(response.mensaje, "", "info");
                                } else {
                                    Swal.fire(response.error, "", "error");
                                }
                            },
                            error: function (xhr, status, error) {
                                console.error("Error al Reservar Cupon Servicio:", xhr.responseText);
                                Swal.fire("Hubo un error", "", "info");
                            }
                        });
                        ////
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
    });
});