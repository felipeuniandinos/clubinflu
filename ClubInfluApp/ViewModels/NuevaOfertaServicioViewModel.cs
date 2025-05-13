using ClubInfluApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ClubInfluApp.ViewModels
{
    public class NuevaOfertaServicioViewModel
    {
        [Required(ErrorMessage = "El nombre de la oferta de servicio es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre debe tener como máximo 100 caracteres.")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string direccion { get; set; }

        [Required(ErrorMessage = "Debe subir una imagen.")]
        [Display(Name = "Imagen de la oferta")]
        public IFormFile archivoImagen { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime fechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime fechaFin { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        [DataType(DataType.Time)]
        public TimeSpan horaInicio { get; set; }

        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        [DataType(DataType.Time)]
        public TimeSpan horaFin { get; set; }

        [Required(ErrorMessage = "Debe indicar el número de cupos disponibles.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe haber al menos un cupo disponible.")]
        public int cuposDisponibles { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        [Display(Name = "Categoría de la oferta")]
        public int idCategoriaOferta { get; set; }

        public List<CategoriaOferta> categorias { get; set; } = new();
    }
}
