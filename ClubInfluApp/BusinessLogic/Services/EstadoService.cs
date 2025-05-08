using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class EstadoService : IEstadoService
    {
        private readonly IEstadoRepository _estadoRepository;

        public EstadoService(IEstadoRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }

        public List<Estado> ObtenerEstadosPorPaisYTermino(int idPais, string termino)
        {
            return _estadoRepository.ObtenerEstadosPorPaisYTermino(idPais, termino);
        }
    }
}
