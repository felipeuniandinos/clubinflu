using ClubInfluApp.Models;

namespace ClubInfluApp.ViewModels
{
    public class ListadoCuponesServiciosOfertadosViewModel
    {
        public List<CategoriaOferta> categoriasOfertas { get; set; } //para el selector de categorias
        public List<CuponServicio> cuponeServicios { get; set; } //para el numero de cuponenes disponibles??

        public OfertaServicio OfertaServicio { get; set; } //para el detalle de la oferta y demas informacion

        ///Lo que prendo hacer es trear la informacion para la vista de listado de cupones y servicios ofertados, clas cate
        ///gorias para el selector de categorias, los cupones para iterar y mostrar en la vista y no se como relaciones con
        ///las ofertas de servicios, ya que no tengo claro como relacionarlo, o si hay que hace rotro viwmodel
    }
}
