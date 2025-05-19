using ClubInfluApp.Models;

namespace ClubInfluApp.ViewModels
{
    public class OfertaServicioViewModel
    {
        public string imagen { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string direccion { get; set; }
        public string pais { get; set; }
        public string estado { get; set; }
        public string ciudad { get; set; }
        public string nombreCategoriaOferta { get; set; }
        public string nombreEmpresa { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public TimeSpan horaInicio { get; set; }
        public TimeSpan horaFin { get; set; }
        public int cuposDisponibles { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
