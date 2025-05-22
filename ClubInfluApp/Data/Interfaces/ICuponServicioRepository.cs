using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.Data.Interfaces
{
    public interface ICuponServicioRepository
    {
        public void ReservarCuponOfertaServicio(int idOfertaServicio, int idUsuarioInfluencer);

        public string ValidarCuponOfertaServicio(int idOfertaServicio, int idInfluencer);

        public OfertaServicioViewModel ObtenetCodigoNombreOfertaPorOfertaServicio(int idOfertaServicio);
    }
}
