using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class ReservaPersonaNegocio
	{
		private readonly ReservaPersonaDatos reservaPersonaDatos;

		public ReservaPersonaNegocio()
		{
			reservaPersonaDatos = new ReservaPersonaDatos();
		}

		public List<ReservaPersona> ConsultarReservaPersonas()
		{
			return reservaPersonaDatos.ConsultarReservaPersonas();
		}

		public bool AgregarReservaPersona(ReservaPersona entidad)
		{
			return reservaPersonaDatos.AgregarReservaPersona(entidad);
		}

		public ReservaPersona? BuscarReservaPersona(int codigoReservaPersona)
		{
			return reservaPersonaDatos.BuscarReservaPersona(codigoReservaPersona);
		}

		public bool EditarReservaPersona(ReservaPersona entidad)
		{
			return reservaPersonaDatos.EditarReservaPersona(entidad);
		}

		public bool EliminarReservaPersona(int codigoReservaPersona)
		{
			return reservaPersonaDatos.EliminarReservaPersona(codigoReservaPersona);
		}
	}
}
