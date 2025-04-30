using System.ComponentModel.DataAnnotations;

namespace ClubInfluApp.Models
{
    public enum TipoUsuario
    {
        [Display(Name = "Empresa")]
        Empresa = 1,

        [Display(Name = "Influencer")]
        Influencer = 2,

        [Display(Name = "Administrador")]
        Administrador = 3
    }
}
