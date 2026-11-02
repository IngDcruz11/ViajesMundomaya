using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class PaqueteHotelNegocio
	{
		private readonly PaqueteHotelDatos paqueteHotelDatos;

		public PaqueteHotelNegocio()
		{
			paqueteHotelDatos = new PaqueteHotelDatos();
		}

		public List<PaqueteHotel> ConsultarPaqueteHoteles()
		{
			return paqueteHotelDatos.ConsultarPaqueteHoteles();
		}

		public bool AgregarPaqueteHotel(PaqueteHotel entidad)
		{
			return paqueteHotelDatos.AgregarPaqueteHotel(entidad);
		}

		public PaqueteHotel? BuscarPaqueteHotel(int codigoPaqueteHotel)
		{
			return paqueteHotelDatos.BuscarPaqueteHotel(codigoPaqueteHotel);
		}

		public bool EditarPaqueteHotel(PaqueteHotel entidad)
		{
			return paqueteHotelDatos.EditarPaqueteHotel(entidad);
		}

		public bool EliminarPaqueteHotel(int codigoPaqueteHotel)
		{
			return paqueteHotelDatos.EliminarPaqueteHotel(codigoPaqueteHotel);
		}
	}
}
