namespace ClubInfluApp.Models
{
    public class UsuarioEmpresa
    {
        public int idUsuarioEmpresa { get; set; }
        public int idEmpresa { get; set; }
        public int idEstadoUsuario { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaActualizacion { get; set; }
    }
}
