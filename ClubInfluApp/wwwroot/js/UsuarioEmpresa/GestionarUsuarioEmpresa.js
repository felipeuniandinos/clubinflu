let estadoInicial = document.getElementById("estadoUsuario").value;
document.getElementById("showAlert").addEventListener("click", function () {
	Swal.fire({
		title: "¿Quieres guardar los cambios?",
		showDenyButton: true,
		showCancelButton: true,
		confirmButtonText: "Guardar",
		denyButtonText: "No guardar"
	}).then((result) => {
		if (result.isConfirmed) {
			let estadoSeleccionado = document.getElementById("estadoUsuario").value;
			let idUsuarioEmpresa = document.querySelector("input[name='idUsuarioEmpresa']").value;
			let idActualEstadoUsuario = document.querySelector("input[name='idActualEstadoUsuario']").value;

			if (estadoSeleccionado !== estadoInicial) {

				$.ajax({
					url: urlActualizarEstado,
					type: "PUT",
					data: {
						idUsuarioEmpresa: idUsuarioEmpresa,
						idActualEstadoUsuario: idActualEstadoUsuario,
						idNuevoEstadoUsuario: estadoSeleccionado
					},
					success: function (response) {
						Swal.fire(response.mensaje, "", "info");
					},
					error: function (xhr, status, error) {
						console.error("Error al actualizar el estado:", xhr.responseText);
						Swal.fire("Hubo un error", "", "info");
					}
				});

			} else {
				Swal.fire("NO SE HA CAMBIADO EL ESTADO DE USUARIO", "", "info");
			}
		} else if (result.isDenied) {
			Swal.fire("Los cambios no se guardaron", "", "info");
		}
	});
});