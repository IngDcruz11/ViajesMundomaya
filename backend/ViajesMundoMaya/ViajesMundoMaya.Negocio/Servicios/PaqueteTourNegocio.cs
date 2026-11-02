using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class PaqueteTourNegocio
	{
		private readonly PaqueteTourDatos paqueteTourDatos;

		public PaqueteTourNegocio()
		{
			paqueteTourDatos = new PaqueteTourDatos();
		}

		public List<PaqueteTour> ConsultarPaqueteTours()
		{
			return paqueteTourDatos.ConsultarPaqueteTours();
		}

		public bool AgregarPaqueteTour(PaqueteTour entidad)
		{
			return paqueteTourDatos.AgregarPaqueteTour(entidad);
		}

		public PaqueteTour? BuscarPaqueteTour(int codigoPaqueteTour)
		{
			return paqueteTourDatos.BuscarPaqueteTour(codigoPaqueteTour);
		}

		public bool EditarPaqueteTour(PaqueteTour entidad)
		{
			return paqueteTourDatos.EditarPaqueteTour(entidad);
		}

		public bool EliminarPaqueteTour(int codigoPaqueteTour)
		{
			return paqueteTourDatos.EliminarPaqueteTour(codigoPaqueteTour);
		}
	}
}
