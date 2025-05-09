$(document).ready(function () {

    function inicializarSelectEstado(paisSelector, estadoSelector) {
        $(estadoSelector).select2({
            placeholder: "Escribe para buscar",
            minimumInputLength: 2,
            ajax: {
                transport: function (params, success, failure) {
                    $.ajax({
                        type: 'GET',
                        url: '/UsuarioEmpresa/ObtenerEstadosPorPaisYTermino',
                        data: {
                            termino: params.data.term,
                            idPais: $(paisSelector).val()
                        },
                        dataType: 'json',
                        success: function (response) {
                            if (response.exito) {
                                success({
                                    results: response.data.map(estado => ({
                                        id: estado.idEstado,
                                        text: estado.estado
                                    }))
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

    function inicializarSelectCiudad(estadoSelector, ciudadSelector) {
        $(ciudadSelector).select2({
            placeholder: "Escribe para buscar",
            minimumInputLength: 2,
            ajax: {
                transport: function (params, success, failure) {
                    $.ajax({
                        type: 'GET',
                        url: '/UsuarioEmpresa/ObtenerCiudadesPorEstadoYTermino',
                        data: {
                            termino: params.data.term,
                            idEstado: $(estadoSelector).val()
                        },
                        dataType: 'json',
                        success: function (response) {
                            if (response.exito) {
                                success({
                                    results: response.data.map(ciudad => ({
                                        id: ciudad.idCiudad,
                                        text: ciudad.ciudad
                                    }))
                                });
                            } else {
                                Swal.fire(response.error, "", "error");
                            }
                        },
                        error: function (xhr, status, error) {
                            console.error("Error en la carga de ciudades:", error);
                            failure();
                        }
                    });
                }
            }
        });

        $(estadoSelector).on('change', function () {
            $(ciudadSelector).val(null).trigger('change');
        });
    }

    inicializarSelectEstado("#pais1", "#estado1");
    inicializarSelectCiudad("#estado1", "#ciudad1");

    $("#terminos").on("change", function () {
        $("#btnCrearCuenta").prop("disabled", !$(this).is(":checked"));
    });
});
