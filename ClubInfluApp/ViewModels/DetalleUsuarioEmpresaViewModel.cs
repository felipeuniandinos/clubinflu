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

        /*
             public int idCiudadEmpresa { get; set; }

    public int? idCiudad2Empresa { get; set; }

    public int? idCiudad3Empresa { get; set; }

    public int? idCiudad4Empresa { get; set; }
         
         */
    }
}
