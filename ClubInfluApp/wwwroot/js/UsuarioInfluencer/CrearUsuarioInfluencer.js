$(document).ready(function () {
    let redSocialIndex = 0;

    $("#addRedSocial").on("click", function () {
        const $container = $("#redesSocialesContainer");
        const $div = $("<div>").addClass("mb-3 red-social-entry");

        $.ajax({
            type: 'GET',
            url: '/UsuarioInfluencer/ObtenerRedesSociales',
            dataType: 'json',
            success: function (response) {
                if (response.exito) {
                    let selectHTML = `<div class="mb-2">
                                        <label class="form-label">Red Social</label>
                                        <select name="redesSociales[${redSocialIndex}].idRedSocial" class="form-select" required>
                                            <option value="">Seleccione una red social</option>`;

                    $.each(response.data, function (i, redSocial) {
                        selectHTML += `<option value="${redSocial.idRedSocial}">${redSocial.redSocial}</option>`;
                    });

                    selectHTML += `   </select>
                                    </div>

                                    <div class="mb-2">
                                        <label class="form-label">Número de Seguidores</label>
                                        <input type="number" name="redesSociales[${redSocialIndex}].numeroSeguidores" class="form-control" required />
                                    </div>

                                    <button type="button" class="btn btn-danger btn-sm removeRedSocial">Eliminar</button>
                                    <hr/>`;

                    $div.html(selectHTML);
                    $container.append($div);
                    redSocialIndex++;
                } else {
                    Swal.fire(response.error, "", "error");
                }
            },
            error: function (xhr, status, error) {
                console.error("Error en la carga de redes sociales:", error);
            }
        });
    });

    $("#redesSocialesContainer").on("click", ".removeRedSocial", function () {
        $(this).closest(".red-social-entry").remove();
    });

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

        $(paisSelector).on("change", function () {
            $(estadoSelector).val(null).trigger("change");
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
                        success: function (response) {
                            if (response.exito) {
                                success({
                                    results: $.map(response.data, function (ciudad) {
                                        return {
                                            id: ciudad.idCiudad,
                                            text: ciudad.ciudad
                                        };
                                    })
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

        $(estadoSelector).on("change", function () {
            $(ciudadSelector).val(null).trigger("change");
        });
    }

    inicializarSelectEstado("#pais1", "#estado1");
    inicializarSelectEstado("#pais2", "#estado2");
    inicializarSelectEstado("#pais3", "#estado3");
    inicializarSelectEstado("#pais4", "#estado4");

    inicializarSelectCiudad("#estado1", "#ciudad1");
    inicializarSelectCiudad("#estado2", "#ciudad2");
    inicializarSelectCiudad("#estado3", "#ciudad3");
    inicializarSelectCiudad("#estado4", "#ciudad4");

    $("#terminos").on("change", function () {
        $("#btnCrearCuenta").prop("disabled", !this.checked);
    });
});
