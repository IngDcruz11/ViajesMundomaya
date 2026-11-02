using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class TipoTransporteNegocio
	{
		private readonly TipoTransporteDatos tipoTransporteDatos;

		public TipoTransporteNegocio()
		{
			tipoTransporteDatos = new TipoTransporteDatos();
		}

		public List<TipoTransporte> ConsultarTiposTransporte()
		{
			return tipoTransporteDatos.ConsultarTiposTransporte();
		}

		public bool AgregarTipoTransporte(TipoTransporte entidad)
		{
			return tipoTransporteDatos.AgregarTipoTransporte(entidad);
		}

		public TipoTransporte? BuscarTipoTransporte(int codigoTipoTransporte)
		{
			return tipoTransporteDatos.BuscarTipoTransporte(codigoTipoTransporte);
		}

		public bool EditarTipoTransporte(TipoTransporte entidad)
		{
			return tipoTransporteDatos.EditarTipoTransporte(entidad);
		}

		public bool EliminarTipoTransporte(int codigoTipoTransporte)
		{
			return tipoTransporteDatos.EliminarTipoTransporte(codigoTipoTransporte);
		}
	}
}
