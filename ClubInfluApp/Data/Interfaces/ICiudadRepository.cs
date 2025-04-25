using ClubInfluApp.Models;

namespace ClubInfluApp.Data.Interfaces
{
    public interface ICiudadRepository
    {
        public List<Ciudad> ObtenerCiudadesPorPaisYTermino(int idPais, string termino);
    }
}
