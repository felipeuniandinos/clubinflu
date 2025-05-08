using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository generoRepository;
        public GeneroService(IGeneroRepository generoRepository)
        {
            this.generoRepository = generoRepository;
        }
        public List<Genero> ObtenerGeneros()
        {
            return generoRepository.ObtenerGeneros();
        }
    }
}
