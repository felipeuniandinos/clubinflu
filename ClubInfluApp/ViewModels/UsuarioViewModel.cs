using ClubInfluApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ClubInfluApp.ViewModels
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "El correo debe tener entre 6 y 100 caracteres.")]
        public string correo { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria.")]
        public string clave { get; set; }

        [Required(ErrorMessage = "El tipo usuarios es obligatoria.")]
        public TipoUsuario tipo { get; set; }
    }
}
