using System.Collections.Generic;
using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Data.Repositories;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class OfertaServicioService : IOfertaServicioService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOfertaServicioRepository _ofertaServicioRepository;
        private readonly IPaisService _paisService;
        private readonly ICategoriaService _categoriaService;
        private readonly IUsuarioEmpresaService _usuarioEmpresaService;

        public OfertaServicioService(IHttpContextAccessor httpContextAccessor, IOfertaServicioRepository ofertaServicioRepository, IPaisService paisService, ICategoriaService categoriaService, IUsuarioEmpresaService usuarioEmpresaService)
        {
            _httpContextAccessor = httpContextAccessor;
            _ofertaServicioRepository = ofertaServicioRepository;
            _paisService = paisService;
            _categoriaService = categoriaService;
            _usuarioEmpresaService = usuarioEmpresaService;
        }
        public OfertasServiciosViewModel ObtenerOfertasDeServicioFiltradas(FiltroOfertasDeServicio filtroOfertasDeServicio)
        {
            OfertasServiciosViewModel ofertaServicioViewModel = new OfertasServiciosViewModel();
            List<OfertaServicioViewModel> ofertasDeServicioFiltradas = _ofertaServicioRepository.ObtenerOfertasDeServicioFiltradas(filtroOfertasDeServicio);
            ofertaServicioViewModel.ofertasServicios = ofertasDeServicioFiltradas;
            ofertaServicioViewModel.paises = _paisService.ObtenerPaises();
            ofertaServicioViewModel.categorias = _categoriaService.ObtenerCategorias();

            return ofertaServicioViewModel;
        }
        public List<OfertaServicioViewModel> ObtenerOfertasDeServicioPorEmpresa()
        {
            int idEmpresaActual = ObtenerIdEmpresaActual();
            List<OfertaServicioViewModel> ofertasDeServicioFiltradasPorEmpresa = _ofertaServicioRepository.ObtenerOfertasDeServicioPorEmpresa(idEmpresaActual);

            return ofertasDeServicioFiltradasPorEmpresa;
        }

        public int CrearOfertaServicio(NuevaOfertaServicioViewModel nuevaOfertaServicioViewModel)
        {
            string rutaImagen = GuardarImagen(nuevaOfertaServicioViewModel.archivoImagen);
            int idEmpresaActual = ObtenerIdEmpresaActual();

            OfertaServicio nuevaOferta = CrearNuevaOfertaServicio(nuevaOfertaServicioViewModel, rutaImagen, idEmpresaActual);

            if (nuevaOferta.cuposDisponibles <= 0)
            {
                throw new Exception("|BL|:El número de cupos disponibles debe ser mayor a cero.");
            }

            return _ofertaServicioRepository.CrearOfertaDeServicio(nuevaOferta);
        }

        private string GuardarImagen(IFormFile archivoImagen)
        {
            if (archivoImagen == null || archivoImagen.Length == 0)
            {
                throw new Exception("No se ha recibido un archivo de imagen válido.");
            }

            string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivoImagen.FileName);
            string carpetaDestino = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes", "ofertas_servicios");

            if (!Directory.Exists(carpetaDestino))
            {
                Directory.CreateDirectory(carpetaDestino);
            }

            string rutaFisica = Path.Combine(carpetaDestino, nombreArchivo);

            using (FileStream stream = new FileStream(rutaFisica, FileMode.Create))
            {
                archivoImagen.CopyTo(stream);
            }

            return nombreArchivo;
        }

        private OfertaServicio CrearNuevaOfertaServicio(NuevaOfertaServicioViewModel nuevaOfertaServicioViewModel, string rutaImagen, int idEmpresa)
        {
            return new OfertaServicio
            {
                idOfertaServicio = 0,
                nombre = nuevaOfertaServicioViewModel.nombre,
                direccion = nuevaOfertaServicioViewModel.direccion,
                imagen = rutaImagen,
                descripcion = nuevaOfertaServicioViewModel.descripcion,
                fechaInicio = nuevaOfertaServicioViewModel.fechaInicio,
                fechaFin = nuevaOfertaServicioViewModel.fechaFin,
                horaInicio = nuevaOfertaServicioViewModel.horaInicio,
                horaFin = nuevaOfertaServicioViewModel.horaFin,
                cuposDisponibles = nuevaOfertaServicioViewModel.cuposDisponibles,
                fechaCreacion = DateTime.Now,
                activo = true,
                idCategoriaOferta = nuevaOfertaServicioViewModel.idCategoriaOferta,
                idEmpresa = idEmpresa
            };
        }

        private int ObtenerIdEmpresaActual()
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

            int idUsuarioEmpresa = int.Parse(userIdStr);
            int idEmpresa = _usuarioEmpresaService.ObtenerEmpresaPorIdUsuarioEmpresa(idUsuarioEmpresa).idEmpresa;
            return idEmpresa;
        }

    }
}
