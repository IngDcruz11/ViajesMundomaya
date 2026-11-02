using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class ItinerarioNegocio
	{
		private readonly ItinerarioDatos itinerarioDatos;

		public ItinerarioNegocio()
		{
			itinerarioDatos = new ItinerarioDatos();
		}

		public List<Itinerario> ConsultarItinerarios()
		{
			return itinerarioDatos.ConsultarItinerarios();
		}

		public bool AgregarItinerario(Itinerario entidad)
		{
			return itinerarioDatos.AgregarItinerario(entidad);
		}

		public Itinerario? BuscarItinerario(int codigoItinerario)
		{
			return itinerarioDatos.BuscarItinerario(codigoItinerario);
		}

		public bool EditarItinerario(Itinerario entidad)
		{
			return itinerarioDatos.EditarItinerario(entidad);
		}

		public bool EliminarItinerario(int codigoItinerario)
		{
			return itinerarioDatos.EliminarItinerario(codigoItinerario);
		}
	}
}
