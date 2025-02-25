using System.ComponentModel.DataAnnotations;

namespace ClubInfluApp.ViewModels
{
    public class NuevoUsuarioEmpresaViewModel
    {

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "El correo debe tener entre 6 y 100 caracteres.")]
        public string correo { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria.")]
        public string clave { get; set; }

        [Required(ErrorMessage = "La ciudad de la empresa es obligatoria.")]
        public int idCiudadEmpresa { get; set; }

        [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre de la empresa debe tener entre 3 y 100 caracteres.")]
        public string nombreEmpresa { get; set; }

        [Url(ErrorMessage = "Ingrese una URL válida (ej.: https://www.ejemplo.com).")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La URL debe tener entre 6 y 100 caracteres.")]
        public string urlEmpresa { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de contacto debe tener exactamente 10 dígitos.")]
        public string numeroContactoEmpresa { get; set; }

        [Required(ErrorMessage = "El sector de la empresa es obligatorio.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "El sector debe tener entre 10 y 500 caracteres.")]
        public string sectorEmpresa { get; set; }

        [Required(ErrorMessage = "La dirección de la empresa es obligatoria.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "La dirección debe tener entre 10 y 500 caracteres.")]
        public string direccionEmpresa { get; set; }
    }
}
