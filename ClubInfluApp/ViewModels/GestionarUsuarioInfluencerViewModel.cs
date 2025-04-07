using System.ComponentModel.DataAnnotations;

namespace ClubInfluApp.ViewModels
{
    public class GestionarUsuarioInfluencerViewModel
    {
        public int idUsuarioInfluencer {  get; set; }
        public string correo { get; set; }
        public DateTime usuariofechacreacion { get; set; }
        public DateTime usuariofechaactualizacion { get; set; }
        public string estadousuario { get; set; }
        public string nombre { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string numerocontacto { get; set; }
        public string ciudadprincipal { get; set; }
        public string paisciudadprincipal { get; set; }
        public string ciudadsecundaria { get; set; }
        public string paisciudadsecundaria { get; set; }
        public string ciudadterciaria { get; set; }
        public string paisciudadterciaria { get; set; }
        public string ciudadcuaternaria { get; set; }
        public string paisciudadcuaternaria { get; set; }
        public string genero { get; set; }
        
        public List<RedSocialViewModel> RedesSociales {  get; set; }
        public List<EstadoUsuarioViewModel> EstadoUsuarios { get; set; }
    }
}

