namespace ClubInfluApp.Models
{
    public class TarjetaPago
    {
        public int idTarjetaPago { get; set; }
        public int idEmpresa { get; set; }
        public string numeroTarjeta { get; set; }
        public string nombreTitular { get; set; }
        public DateTime? fechaExpiracion { get; set; }
        public string codigoSeguridad { get; set; }
        public bool activo { get; set; } = true;
    }
}
