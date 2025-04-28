namespace ClubInfluApp.Models
{
    public class Influencer
    {
        public int idInfluencer { get; set; }
        public int idCiudad { get; set; }
        public int? idCiudad2 { get; set; }
        public int? idCiudad3 { get; set; }
        public int? idCiudad4 { get; set; }
        public int idGenero { get; set; }
        public string nombre { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string numeroContacto { get; set; }
    }
}
