using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class TourNegocio
	{
		private readonly TourDatos tourDatos;

		public TourNegocio()
		{
			tourDatos = new TourDatos();
		}

		public List<Tour> ConsultarTours()
		{
			return tourDatos.ConsultarTours();
		}

		public bool AgregarTour(Tour entidad)
		{
			return tourDatos.AgregarTour(entidad);
		}

		public Tour? BuscarTour(int codigoTour)
		{
			return tourDatos.BuscarTour(codigoTour);
		}

		public bool EditarTour(Tour entidad)
		{
			return tourDatos.EditarTour(entidad);
		}

		public bool EliminarTour(int codigoTour)
		{
			return tourDatos.EliminarTour(codigoTour);
		}
	}
}
