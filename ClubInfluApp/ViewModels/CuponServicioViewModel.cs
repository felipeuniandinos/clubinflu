namespace ClubInfluApp.ViewModels
{
    public class CuponServicioViewModel
    {
        public int idCuponServicio { get; set; }
        public string codigo { get; set; }
        public DateTime? fechaRedencion { get; set; }
        public int idOfertaServicio { get; set; }
        public string nombreEstadoCupon { get; set; }
        public string? nombreInfluencer { get; set; }
        public List<string> videoPublicidad { get; set; }
        public string nombreOfertaServicio { get; set; }
    }
}
