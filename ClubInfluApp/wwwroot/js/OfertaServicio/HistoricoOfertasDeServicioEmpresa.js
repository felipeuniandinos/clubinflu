function validarCupon() {
    var url = $("#urlvalidarCuponDeServicioPorCodigo").val();

    Swal.fire({
        title: "Ingresa el código del cupón:",
        input: "text",
        inputAttributes: {
            autocapitalize: "on"
        },
        showCancelButton: true,
        confirmButtonText: "Validar",
        showLoaderOnConfirm: true,
        allowOutsideClick: () => !Swal.isLoading(),
    }).then((result) => {
        if (result.isConfirmed && result.value) {

            const codigoCupon = String(result.value).trim();

            $.ajax({
                url: url,
                type: "PUT",
                data: {
                    codigoDeCuponAValidar: codigoCupon
                },
                success: function (response) {
                    if (response.exito) {
                        Swal.fire({
                            title: "Validar Cupón",
                            text: response.mensaje,
                            icon: "success"
                        });
                    } else {
                        Swal.fire({
                            title: "Hubo un error",
                            text: response.error,
                            icon: "info"
                        });
                    }
                },
                error: function (xhr, status, error) {
                    Swal.fire({
                        title: "Hubo un error - Validar Cupón",
                        text: "No se valido el cupón.",
                        icon: "info"
                    });
                }
            });
        }
    });
}
