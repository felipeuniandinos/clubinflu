using ClubInfluApp.Models;

namespace ClubInfluApp.Data.Interfaces
{
    public interface ICiudadRepository
    {
        public List<Ciudad> ObtenerCiudadesPorEstadoYTermino(int idEstado, string termino);
    }
}
