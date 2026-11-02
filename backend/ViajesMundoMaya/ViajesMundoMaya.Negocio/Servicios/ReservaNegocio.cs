using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class ReservaNegocio
	{
		private readonly ReservaDatos reservaDatos;

		public ReservaNegocio()
		{
			reservaDatos = new ReservaDatos();
		}

		public List<Reserva> ConsultarReservas()
		{
			return reservaDatos.ConsultarReservas();
		}

		public bool AgregarReserva(Reserva entidad)
		{
			return reservaDatos.AgregarReserva(entidad);
		}

		public Reserva? BuscarReserva(int codigoReserva)
		{
			return reservaDatos.BuscarReserva(codigoReserva);
		}

		public bool EditarReserva(Reserva entidad)
		{
			return reservaDatos.EditarReserva(entidad);
		}

		public bool EliminarReserva(int codigoReserva)
		{
			return reservaDatos.EliminarReserva(codigoReserva);
		}
	}
}
