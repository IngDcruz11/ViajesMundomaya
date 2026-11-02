using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class MetodoPagoNegocio
	{
		private readonly MetodoPagoDatos metodoPagoDatos;

		public MetodoPagoNegocio()
		{
			metodoPagoDatos = new MetodoPagoDatos();
		}

		public List<MetodoPago> ConsultarMetodosPago()
		{
			return metodoPagoDatos.ConsultarMetodosPago();
		}

		public bool AgregarMetodoPago(MetodoPago entidad)
		{
			return metodoPagoDatos.AgregarMetodoPago(entidad);
		}

		public MetodoPago? BuscarMetodoPago(int codigoMetodoPago)
		{
			return metodoPagoDatos.BuscarMetodoPago(codigoMetodoPago);
		}

		public bool EditarMetodoPago(MetodoPago entidad)
		{
			return metodoPagoDatos.EditarMetodoPago(entidad);
		}

		public bool EliminarMetodoPago(int codigoMetodoPago)
		{
			return metodoPagoDatos.EliminarMetodoPago(codigoMetodoPago);
		}
	}
}
