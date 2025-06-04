using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Interfaces
{
    public interface ICiudadService
    {
        public List<Ciudad> ObtenerCiudadesPorEstadoYTermino(int idEstado, string termino);
    }
}
