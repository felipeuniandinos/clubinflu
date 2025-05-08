using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class CiudadService : ICiudadService
    {
        private readonly ICiudadRepository _ciudadRepository;

        public CiudadService(ICiudadRepository ciudadRepository)
        {
            _ciudadRepository = ciudadRepository;
        }

        public List<Ciudad> ObtenerCiudadesPorEstadoYTermino(int idEstado, string termino)
        {
            return _ciudadRepository.ObtenerCiudadesPorEstadoYTermino(idEstado, termino);
        }
    }
}
