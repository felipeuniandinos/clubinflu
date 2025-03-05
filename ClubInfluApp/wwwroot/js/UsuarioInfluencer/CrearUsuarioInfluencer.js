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

    const ciudadesPorPais = {
        "1": [
            { id: 1, nombre: "Bogotá" },
            { id: 2, nombre: "Medellín" }
        ],
        "2": [
            { id: 3, nombre: "Ciudad de México" },
            { id: 4, nombre: "Guadalajara" }
        ],
        "3": [
            { id: 5, nombre: "Buenos Aires" },
            { id: 6, nombre: "Córdoba" }
        ],
        "4": [
            { id: 7, nombre: "Madrid" },
            { id: 8, nombre: "Barcelona" }
        ],
        "5": [
            { id: 9, nombre: "Nueva York" },
            { id: 10, nombre: "Los Ángeles" }
        ]
    };

    function actualizarCiudades(paisId, ciudadId) {
        let paisSeleccionado = document.getElementById(paisId).value;
        let ciudadSelect = document.getElementById(ciudadId);
        ciudadSelect.innerHTML = "";

        if (ciudadesPorPais[paisSeleccionado]) {
            ciudadesPorPais[paisSeleccionado].forEach(ciudad => {
                let option = document.createElement("option");
                option.value = ciudad.id; // Ahora el value es el ID de la ciudad
                option.textContent = ciudad.nombre; // Se muestra el nombre de la ciudad
                ciudadSelect.appendChild(option);
            });
        } else {
            let option = document.createElement("option");
            option.textContent = "Selecciona un país primero";
            option.value = "";
            ciudadSelect.appendChild(option);
        }
    }

    // Asignamos los eventos de cambio para actualizar cada ciudad
    document.getElementById("pais1").addEventListener("change", () => actualizarCiudades("pais1", "ciudad1"));
    document.getElementById("pais2").addEventListener("change", () => actualizarCiudades("pais2", "ciudad2"));
    document.getElementById("pais3").addEventListener("change", () => actualizarCiudades("pais3", "ciudad3"));
    document.getElementById("pais4").addEventListener("change", () => actualizarCiudades("pais4", "ciudad4"));

    const checkboxTerminos = document.getElementById("terminos");
    const btnCrearCuenta = document.getElementById("btnCrearCuenta");

    checkboxTerminos.addEventListener("change", function () {
        btnCrearCuenta.disabled = !this.checked;
    });
});
