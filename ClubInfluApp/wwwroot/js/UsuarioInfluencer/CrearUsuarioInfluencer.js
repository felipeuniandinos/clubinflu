document.addEventListener("DOMContentLoaded", function () {
    let redSocialIndex = 0;

    document.getElementById("addRedSocial").addEventListener("click", function () {
        const container = document.getElementById("redesSocialesContainer");

        const div = document.createElement("div");
        div.classList.add("mb-3", "red-social-entry");
        div.innerHTML = `
                    <div class="mb-2">
                        <label class="form-label">Red Social</label>
                        <select name="redesSociales[${redSocialIndex}].idRedSocial" class="form-select" required>
                            <option value="">Seleccione una red social</option>
                            <option value="1">Instagram</option>
                            <option value="2">TikTok</option>
                            <option value="3">YouTube</option>
                            <option value="4">Twitter</option>
                        </select>
                    </div>

                    <div class="mb-2">
                        <label class="form-label">Número de Seguidores</label>
                        <input type="number" name="redesSociales[${redSocialIndex}].numeroSeguidores" class="form-control" required />
                    </div>

                    <button type="button" class="btn btn-danger btn-sm removeRedSocial">Eliminar</button>
                    <hr/>
                `;

        container.appendChild(div);

        // Incrementa el índice para que los nombres sean únicos
        redSocialIndex++;
    });

    document.getElementById("redesSocialesContainer").addEventListener("click", function (e) {
        if (e.target.classList.contains("removeRedSocial")) {
            e.target.parentElement.remove();
        }
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
    inicializarSelectEstado("#pais2", "#estado2");
    inicializarSelectEstado("#pais3", "#estado3");
    inicializarSelectEstado("#pais4", "#estado4");

    inicializarSelectCiudad("#estado1", "#ciudad1");
    inicializarSelectCiudad("#estado2", "#ciudad2");
    inicializarSelectCiudad("#estado3", "#ciudad3");
    inicializarSelectCiudad("#estado4", "#ciudad4");
   
    const checkboxTerminos = document.getElementById("terminos");
    const btnCrearCuenta = document.getElementById("btnCrearCuenta");

    checkboxTerminos.addEventListener("change", function () {
        btnCrearCuenta.disabled = !this.checked;
    });
});
