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
});