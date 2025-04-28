namespace ClubInfluApp.Models
{
    public class UsuarioAdministrador
    {
        public int idUsuarioAdministrador { get; set; }
        public int idEstadoUsuario { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaActualizacion { get; set; }
    }
}
