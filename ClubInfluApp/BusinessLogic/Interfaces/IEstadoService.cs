using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Interfaces
{
    public interface IEstadoService
    {
        public List<Estado> ObtenerEstadosPorPaisYTermino(int idPais, string termino);
      
        public Estado ObtenerEstadoPrinciaplPorIdUsuarioInfluencer(int idUsuarioInfluencer);
    }
}
