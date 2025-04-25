namespace ClubInfluApp.Models
{
    public class ConfiguracionCorreo
    {
        public string Servidor { get; set; }
        public int Puerto { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool Ssl { get; set; }
    }
}
