using ClubInfluApp.Models;

namespace ClubInfluApp.ViewModels
{
    public class OfertaServicioViewModel
    {
        public string imagen { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int nombreCategoria { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public TimeSpan horaInicio { get; set; }
        public TimeSpan horaFin { get; set; }
    }
}
