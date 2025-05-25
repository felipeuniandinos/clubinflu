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
        preConfirm: (codigoCupon) => {
            return new Promise((resolve, reject) => {
                $.ajax({
                    url: url,
                    type: "PUT",
                    contentType: "application/json",
                    data: JSON.stringify({ codigoDeCuponAValidar: codigoCupon }),
                    success: function (response) {
                        if (response.success) {
                            resolve(response.message);
                        } else {
                            reject("Error: " + response.message);
                        }
                    },
                    error: function (xhr, status, error) {
                        reject("Error del servidor: " + xhr.responseText);
                    }
                });
            }).catch((errorMessage) => {
                Swal.showValidationMessage(errorMessage);
            });
        }
    }).then((result) => {
        if (result.isConfirmed && result.value) {
            Swal.fire({
                title: "Resultado",
                text: result.value,
                icon: "info"
            });
        }
    });
}
