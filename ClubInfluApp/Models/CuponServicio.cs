﻿namespace ClubInfluApp.Models
{
    public class CuponServicio
    {
        public int idCuponServicio { get; set; }
        public string codigo { get; set; }
        public DateTime? fechaRedencion { get; set; }
        public int idOfertaServicio { get; set; }
        public int idEstadoCupon { get; set; }
        public int idInfluencer { get; set; }
    }
}
