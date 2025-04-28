using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Interfaces
{
    public interface ICiudadService
    {
        public List<Ciudad> ObtenerCiudadesPorPaisYTermino(int idPais, string termino);
    }
}
