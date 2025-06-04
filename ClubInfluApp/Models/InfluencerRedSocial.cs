namespace ClubInfluApp.Models
{
    public class InfluencerRedSocial
    {
        public int idInfluencerRedSocial { get; set; }
        public int idInfluencer { get; set; }
        public int idRedSocial { get; set; }
        public int numeroSeguidores { get; set; }
        public bool activo { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public string videoEstadisticas { get; set; }
    }
}
