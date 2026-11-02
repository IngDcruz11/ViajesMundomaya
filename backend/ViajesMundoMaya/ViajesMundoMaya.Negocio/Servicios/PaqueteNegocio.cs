using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class PaqueteNegocio
	{
		private readonly PaqueteDatos paqueteDatos;

		public PaqueteNegocio()
		{
			paqueteDatos = new PaqueteDatos();
		}

		public List<Paquete> ConsultarPaquetes()
		{
			return paqueteDatos.ConsultarPaquetes();
		}

		public bool AgregarPaquete(Paquete entidad)
		{
			return paqueteDatos.AgregarPaquete(entidad);
		}

		public Paquete? BuscarPaquete(int codigoPaquete)
		{
			return paqueteDatos.BuscarPaquete(codigoPaquete);
		}

		public bool EditarPaquete(Paquete entidad)
		{
			return paqueteDatos.EditarPaquete(entidad);
		}

		public bool EliminarPaquete(int codigoPaquete)
		{
			return paqueteDatos.EliminarPaquete(codigoPaquete);
		}
	}
}
