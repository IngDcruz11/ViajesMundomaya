using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class TrasladoNegocio
	{
		private readonly TrasladoDatos trasladoDatos;

		public TrasladoNegocio()
		{
			trasladoDatos = new TrasladoDatos();
		}

		public List<Traslado> ConsultarTraslados()
		{
			return trasladoDatos.ConsultarTraslados();
		}

		public bool AgregarTraslado(Traslado entidad)
		{
			return trasladoDatos.AgregarTraslado(entidad);
		}

		public Traslado? BuscarTraslado(int codigoTraslado)
		{
			return trasladoDatos.BuscarTraslado(codigoTraslado);
		}

		public bool EditarTraslado(Traslado entidad)
		{
			return trasladoDatos.EditarTraslado(entidad);
		}

		public bool EliminarTraslado(int codigoTraslado)
		{
			return trasladoDatos.EliminarTraslado(codigoTraslado);
		}
	}
}
