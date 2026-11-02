using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class HotelNegocio
	{
		private readonly HotelDatos hotelDatos;

		public HotelNegocio()
		{
			hotelDatos = new HotelDatos();
		}

		public List<Hotel> ConsultarHoteles()
		{
			return hotelDatos.ConsultarHoteles();
		}

		public bool AgregarHotel(Hotel entidad)
		{
			return hotelDatos.AgregarHotel(entidad);
		}

		public Hotel? BuscarHotel(int codigoHotel)
		{
			return hotelDatos.BuscarHotel(codigoHotel);
		}

		public bool EditarHotel(Hotel entidad)
		{
			return hotelDatos.EditarHotel(entidad);
		}

		public bool EliminarHotel(int codigoHotel)
		{
			return hotelDatos.EliminarHotel(codigoHotel);
		}
	}
}
