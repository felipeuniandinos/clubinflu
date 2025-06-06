﻿using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.Data.Interfaces
{
    public interface ICuponServicioRepository
    {
        public void ReservarCuponOfertaServicio(int idOfertaServicio, int idUsuarioInfluencer);
        public string ValidarCuponOfertaServicio(int idOfertaServicio, int idInfluencer);
        public OfertaServicioViewModel ObtenetCodigoNombreOfertaPorOfertaServicio(int idOfertaServicio);
        public List<CuponServicioViewModel> ObtenerCuponesPorOfertaServicio(int idOfertaServicio);
        public string validarCuponDeServicioPorCodigo(string codigoDeCuponAValidar);
        public List<CuponServicioViewModel> ListarCuponesServicioPorInfluencer(int idInfluencer);
        public CuponServicioViewModel ObtenerCuponServicioPorIdCuponServicio(int idCuponServicio);
        public CuponServicioViewModel SubirVideoCuponServicio(int idCuponServicio, VideoCupon videoCupon);
        public List<string> ObtenerCuponesPorFinalizar(int idInfluencer);
    }
}
