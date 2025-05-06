using ClubInfluApp.Models;

namespace ClubInfluApp.ViewModels
{
    public class DetalleUsuarioEmpresaViewModel
    {
        public int idUsuarioEmpresa { get; set; }

        public string correo { get; set; }

        public string estadoUsuario { get; set; }

        public DateTime fechaCreacion { get; set; }

        public string nombre { get; set; }
        public string nif { get; set; }

        public string url { get; set; }

        public string numeroContacto { get; set; }

        public string sector { get; set; }

        public string direccion { get; set; }

        public List<EstadoUsuario> estadosUsuarios { get; set; }

        public string numeroTarjeta { get; set; }

        public string nombreTitular { get; set; }

        public DateTime fechaExpiracion { get; set; }

        public string codigoSeguridad { get; set; }
        public string pais {  get; set; }
        public string estado {  get; set; }
        public string ciudad {  get; set; }
    }
}
