using System.ComponentModel.DataAnnotations;

namespace ClubInfluApp.ViewModels
{
    public class NuevoInfluencerRedSocialViewModel
    {
        [Required(ErrorMessage = "La red social es obligatoria.")]
        public int idRedSocial { get; set; }

        [Required(ErrorMessage = "El numero de segudires es obligatorio.")]
        public int numeroSeguidores { get; set; }

    }
}
