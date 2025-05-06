document.addEventListener("DOMContentLoaded", function () {

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
                        success: function (data) {
                            success({
                                results: data.map(estado => ({
                                    id: estado.idEstado,
                                    text: estado.estado
                                }))
                            });
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
                        url: '/UsuarioInfluencer/ObtenerCiudadesPorEstadoYTermino',
                        data: {
                            termino: params.data.term,
                            idEstado: $(estadoSelector).val()
                        },
                        dataType: 'json',
                        success: function (data) {
                            success({
                                results: data.map(ciudad => ({
                                    id: ciudad.idCiudad,
                                    text: ciudad.ciudad
                                }))
                            });
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
   
    const checkboxTerminos = document.getElementById("terminos");
    const btnCrearCuenta = document.getElementById("btnCrearCuenta");

    checkboxTerminos.addEventListener("change", function () {
        btnCrearCuenta.disabled = !this.checked;
    });
});