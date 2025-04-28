namespace ClubInfluApp.Models
{
    public class Empresa
    {
        public int idEmpresa { get; set; }
        public int idCiudad { get; set; }
        public int? idCiudad2 { get; set; }
        public int? idCiudad3 { get; set; }
        public int? idCiudad4 { get; set; }
        public string nombre { get; set; }
        public string nif { get; set; }
        public string url { get; set; }
        public string numeroContacto { get; set; }
        public string sector { get; set; }
        public string direccion { get; set; }
    }
}
