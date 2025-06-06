﻿using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Data.Repositories;
using ClubInfluApp.Helpers;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Services

{
    public class CuponServicioService : ICuponServicioService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICuponServicioRepository _cuponServicioRepository;
        private readonly IUsuarioInfluencerService _usuarioInfluencerService;

        public CuponServicioService(IHttpContextAccessor httpContextAccessor, ICuponServicioRepository cuponServicioRepository, IUsuarioInfluencerService usuarioInfluencerService)
        {
            _httpContextAccessor = httpContextAccessor;
            _cuponServicioRepository = cuponServicioRepository;
            _usuarioInfluencerService = usuarioInfluencerService;
        }

        public void ReservarCuponOfertaServicio(int idOfertaServicio)
        {
            int idInfluencer = ObtenerIdInfluencerActual();
            string respuestaValidacion = ValidarSiSePuedeReservarUnCuponParaLaOfertaServicio(idOfertaServicio, idInfluencer);
            if (respuestaValidacion == "Validación exitosa: Puede redimir el cupón.")
            {
                _cuponServicioRepository.ReservarCuponOfertaServicio(idOfertaServicio, idInfluencer);
                EnviarCorreoReservarCuponOfertaServicio(idInfluencer, idOfertaServicio);
            }
            else
            {
                throw new Exception(respuestaValidacion);
            }
        }

        private string ValidarSiSePuedeReservarUnCuponParaLaOfertaServicio(int idOfertaServicio, int idInfluencer)
        {
            return _cuponServicioRepository.ValidarCuponOfertaServicio(idOfertaServicio, idInfluencer);
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
            int idInfluencer = _usuarioInfluencerService.ObtenerInfluencerPorIdUsuarioInfluencer(idUsuarioInfluencer).idInfluencer;
            return idInfluencer;
        }

        private void EnviarCorreoReservarCuponOfertaServicio(int idUsuarioInfluencer, int idOfertaServicio)
        {
            GestionarUsuarioInfluencerViewModel usuarioInfluencer = _usuarioInfluencerService.GestionarUsuarioInfluencer(idUsuarioInfluencer);
            OfertaServicioViewModel codigoNombreOfertaServicio = _cuponServicioRepository.ObtenetCodigoNombreOfertaPorOfertaServicio(idOfertaServicio);
            if (usuarioInfluencer == null)
            {
                throw new Exception("No se encontró el usuario influencer con ese id");
            }

            NotificacionesCorreoHelper.EnviarCorreo(
                new List<string> { usuarioInfluencer.correo },
                "Reserva Cupon Oferta Servicio - ClubInflu",
                $@" Estimado/a <strong>{usuarioInfluencer.nombre}</strong>,<br /><br />
                    Le informamos que, la reserva de oferta servicio  <strong>{codigoNombreOfertaServicio.nombre}</strong> se realizo correctamente, su codigo de reserva en
                    <strong>ClubInflu</strong> es: <strong>{codigoNombreOfertaServicio.codigo}</strong>.
                    <br /><br /> 
                    Para más información, por favor contáctese con nosotros al <strong>+1 (555) 123-4567</strong> o escriba a <strong>soporte@clubinflu.com</strong>.
                "
            );
        }

        public List<CuponServicioViewModel> ObtenerCuponesPorOfertaServicio(int idOfertaServicio)
        {
            return _cuponServicioRepository.ObtenerCuponesPorOfertaServicio(idOfertaServicio);
        }

        public string validarCuponDeServicioPorCodigo(string codigoDeCuponAValidar)
        {
            string validacionCupon = _cuponServicioRepository.validarCuponDeServicioPorCodigo(codigoDeCuponAValidar);
            if (!validacionCupon.Equals("El cupón ha sido validado correctamente."))
            {
                throw new Exception(validacionCupon);
            }
            return validacionCupon;
        }

        public List<CuponServicioViewModel> ListarCuponesServicioPorInfluencer()
        {
            int idInfluencer = ObtenerIdInfluencerActual();
            return _cuponServicioRepository.ListarCuponesServicioPorInfluencer(idInfluencer);
        }

        public List<string> ObtenerCuponesPorFinalizar()
        {
            int idInfluencer = ObtenerIdInfluencerActual();
            List<string> cuponesPorFinalizar = _cuponServicioRepository.ObtenerCuponesPorFinalizar(idInfluencer);
          
            return cuponesPorFinalizar;
        }

        public CuponServicioViewModel ObtenerCuponServicioPorIdCuponServicio(int idCuponServicio)
        {
            return _cuponServicioRepository.ObtenerCuponServicioPorIdCuponServicio(idCuponServicio);

        }

        public CuponServicioViewModel SubirVideoCuponServicio(int idCuponServicio, IFormFile video)
        {
            VideoCupon videoCupon = CrearVideoCupon(idCuponServicio, video);
            return _cuponServicioRepository.SubirVideoCuponServicio(idCuponServicio, videoCupon);
        }

        private string GuardarVideo(IFormFile archivoVideo)
        {
            if (archivoVideo == null || archivoVideo.Length == 0)
            {
                throw new Exception("No se ha recibido un archivo de video válido.");
            }

            string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivoVideo.FileName);
            string carpetaDestino = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", "cupones");

            if (!Directory.Exists(carpetaDestino))
            {
                Directory.CreateDirectory(carpetaDestino);
            }

            string rutaFisica = Path.Combine(carpetaDestino, nombreArchivo);

            using (FileStream stream = new FileStream(rutaFisica, FileMode.Create))
            {
                archivoVideo.CopyTo(stream);
            }

            return nombreArchivo;
        }

        private VideoCupon CrearVideoCupon(int idCuponServicio, IFormFile video)
        {
            DateTime fechaActual = System.DateTime.Now;
            string rutaVideo = GuardarVideo(video);

            VideoCupon cuponServicio = new VideoCupon
            {
                idVideoCupon = 0,
                videoCupon = rutaVideo,
                fechaCreacion = fechaActual,
                idCuponServicio = idCuponServicio
            };

            return cuponServicio;
        }

    }
}
