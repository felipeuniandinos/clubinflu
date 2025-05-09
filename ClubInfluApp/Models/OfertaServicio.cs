namespace ClubInfluApp.Models
{
    public class OfertaServicio
    {
        public int idOfertaServicio { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string imagen { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public TimeSpan horaInicio { get; set; }
        public TimeSpan horaFin { get; set; }
        public int cuposDisponibles { get; set; }
        public DateTime fechaCreacion { get; set; }
        public bool activo { get; set; }
        public int idCategoriaOferta { get; set; }
        public int idEmpresa { get; set; }
    }
}
