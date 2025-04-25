using ClubInfluApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ClubInfluApp.ViewModels
{
    public class NuevoUsuarioInfluencerViewModel
    {
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "El correo debe tener entre 6 y 100 caracteres.")]
        public string correo { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria.")]
        public string clave { get; set; }

        [Required(ErrorMessage = "La ciudad de la empresa es obligatoria.")]
        public int idCiudad { get; set; }

        public int? idCiudad2 { get; set; }

        public int? idCiudad3 { get; set; }

        public int? idCiudad4 { get; set; }

        [Required(ErrorMessage = "El genero es obligatorio.")]
        public int idGenero { get; set; }

        [Required(ErrorMessage = "El genero es obligatorio.")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "El nombre debe tener entre 10 y 200 caracteres.")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime fechaNacimiento { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de contacto debe tener exactamente 10 dígitos.")]
        public string numeroContacto { get; set; }

        public List<NuevoInfluencerRedSocialViewModel> redesSociales { get; set; }

        public List<Pais> paises { get; set; }

    }
}
