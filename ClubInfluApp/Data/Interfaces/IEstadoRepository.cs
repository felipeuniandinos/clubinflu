using ClubInfluApp.Models;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IEstadoRepository
    {
        public List<Estado> ObtenerEstadosPorPaisYTermino(int idPais, string termino);
    }
}
