using ClubInfluApp.Models;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IVideoPublicidadRepository
    {
        public List<string> ObtenerUrldeVideosPorIdCupon(int idCuponServicio);
    }
}
