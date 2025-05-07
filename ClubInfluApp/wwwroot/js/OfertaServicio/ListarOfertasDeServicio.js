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

    inicializarSelectEstado("#pais1", "#idEstado");
});
