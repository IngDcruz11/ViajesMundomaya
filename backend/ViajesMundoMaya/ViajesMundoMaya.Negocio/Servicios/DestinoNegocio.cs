using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class DestinoNegocio
	{
		private readonly DestinoDatos destinoDatos;

		public DestinoNegocio()
		{
			destinoDatos = new DestinoDatos();
		}

		public List<Destino> ConsultarDestinos()
		{
			return destinoDatos.ConsultarDestinos();
		}

		public bool AgregarDestino(Destino entidad)
		{
			return destinoDatos.AgregarDestino(entidad);
		}

		public Destino? BuscarDestino(int codigoDestino)
		{
			return destinoDatos.BuscarDestino(codigoDestino);
		}

		public bool EditarDestino(Destino entidad)
		{
			return destinoDatos.EditarDestino(entidad);
		}

		public bool EliminarDestino(int codigoDestino)
		{
			return destinoDatos.EliminarDestino(codigoDestino);
		}
	}
}
