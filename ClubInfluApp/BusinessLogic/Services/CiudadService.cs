using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class CiudadService : ICiudadService
    {
        private readonly ICiudadRepository ciudadRepository;

        public CiudadService(ICiudadRepository ciudadRepository)
        {
            this.ciudadRepository = ciudadRepository;
        }

        public List<Ciudad> ObtenerCiudadesPorEstadoYTermino(int idEstado, string termino)
        {
            return ciudadRepository.ObtenerCiudadesPorEstadoYTermino(idEstado, termino);
        }
    }
}
