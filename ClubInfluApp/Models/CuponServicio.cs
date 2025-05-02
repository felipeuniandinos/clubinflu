namespace ClubInfluApp.Models
{
    public class CuponServicio
    {
        public int idCuponServicio { get; set; }
        public int codigoCuponServicio { get; set; }
        public DateTime fechaRedencionCuponServicio { get; set; }
        public int idOfertaServicio { get; set; }
        public int idEstadoCupon { get; set; }
        public int? idInfluencer { get; set; }
    }
}
