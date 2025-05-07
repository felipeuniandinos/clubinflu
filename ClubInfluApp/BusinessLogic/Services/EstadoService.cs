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
        
        //TODO: Obtener el id estado principal asociado al usuario influencer desde la base de datos
        public Estado ObtenerEstadoPrinciaplPorIdUsuarioInfluencer(int idUsuarioInfluencer)
        {
            throw new NotImplementedException();
        }

        public List<Estado> ObtenerEstadosPorPaisYTermino(int idPais, string termino)
        {
            return estadoRepository.ObtenerEstadosPorPaisYTermino(idPais, termino);
        }
    }
}
