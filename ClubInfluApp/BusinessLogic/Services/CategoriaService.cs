using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public List<CategoriaOferta> ObtenerCategorias()
        {
            return categoriaRepository.ObtenerCategorias();
        }
    }
   
}
