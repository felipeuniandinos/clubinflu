using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Data.Repositories;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Services

{
    public class CuponServicioService : ICuponServicioService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICuponServicioRepository _cuponServicioRepository;

        public CuponServicioService(IHttpContextAccessor httpContextAccessor, ICuponServicioRepository cuponServicioRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _cuponServicioRepository = cuponServicioRepository;
        }

        public void ReservarCuponOfertaServicio(int idOfertaServicio)
        {
            int idInfluencer = ObtenerIdInfluencerActual();
            ValidarSiSePuedeReservarUnCuponParaLaOfertaServicio(idOfertaServicio, idInfluencer);
            _cuponServicioRepository.ReservarCuponOfertaServicio(idOfertaServicio, idInfluencer);
        }

        private void ValidarSiSePuedeReservarUnCuponParaLaOfertaServicio(int idOfertaServicio, int idInfluencer)
        {
            // Aquí puedes implementar la lógica para validar si se puede reservar un cupón para la oferta de servicio.
            // Por ejemplo, verificar si hay cupones disponibles o si el usuario ya ha reservado un cupón. --TODO HACERLO CON UNA FUNCION EN LA DB
        }

        private int ObtenerIdInfluencerActual()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                throw new Exception("No hay un usuario autenticado.");
            }

            string userIdStr = user.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdStr))
            {
                throw new Exception("No se encontró el ID del usuario en las claims.");
            }

            int idUsuarioInfluencer = int.Parse(userIdStr);
            // int idInfluencer = _usuarioInfluencerService.ObtenerInfluencesPorIdUsuarioInfluencer(idUsuarioInfluencer).idInfluencer; //Completar esto
            return 1; //Luego retornar lo que es
        }
    }
}
