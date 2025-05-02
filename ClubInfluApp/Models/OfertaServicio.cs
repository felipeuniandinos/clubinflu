namespace ClubInfluApp.Models
{
    public class OfertaServicio
    {
        public int idOfertaServicio { get; set; }
        public string nombreOfertaServicio { get; set; }
        public string direccionOfertaServicio { get; set; }
        public string imagenOfertaServicio { get; set; }
        public string descripcionOfertaServicio { get; set; }
        public DateTime fechaInicioOfertaServicio { get; set; }
        public DateTime fechaFinOfertaServicio { get; set; }
        public DateTime horaInicioOfertaServicio { get; set; }
        public DateTime horaFinOfertaServicio { get; set; }
        public int cuposDisponiblesOfertaServicio { get; set; }
        public DateTime fechaCreacionOfertaServicio { get; set; }
        public bool activoOfertaServicio { get; set; }
        public int idCategoriaOferta { get; set; }
        public int idCiudad { get; set; }
    }
}
