using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class EstadoService : IEstadoService
    {
        private readonly IEstadoRepository estadoRepository;

        public EstadoService(IEstadoRepository estadoRepository)
        {
            this.estadoRepository = estadoRepository;
        }
  
        public Estado ObtenerEstadoPrincipalPorIdUsuarioInfluencer(int idUsuarioInfluencer)
        {
            return estadoRepository.ObtenerEstadoPrincipalPorIdUsuarioInfluencer(idUsuarioInfluencer);
        }

        public List<Estado> ObtenerEstadosPorPaisYTermino(int idPais, string termino)
        {
            return estadoRepository.ObtenerEstadosPorPaisYTermino(idPais, termino);
        }
    }
}
