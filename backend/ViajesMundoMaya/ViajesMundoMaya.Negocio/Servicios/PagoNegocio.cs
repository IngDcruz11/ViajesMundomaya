using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class PagoNegocio
	{
		private readonly PagoDatos pagoDatos;

		public PagoNegocio()
		{
			pagoDatos = new PagoDatos();
		}

		public List<Pago> ConsultarPagos()
		{
			return pagoDatos.ConsultarPagos();
		}

		public bool AgregarPago(Pago entidad)
		{
			return pagoDatos.AgregarPago(entidad);
		}

		public Pago? BuscarPago(int codigoPago)
		{
			return pagoDatos.BuscarPago(codigoPago);
		}

		public bool EditarPago(Pago entidad)
		{
			return pagoDatos.EditarPago(entidad);
		}

		public bool EliminarPago(int codigoPago)
		{
			return pagoDatos.EliminarPago(codigoPago);
		}
	}
}
