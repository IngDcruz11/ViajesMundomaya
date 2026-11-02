using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class ClienteNegocio
	{
		private readonly ClienteDatos clienteDatos;

		public ClienteNegocio()
		{
			clienteDatos = new ClienteDatos();
		}

		public List<Cliente> ConsultarClientes()
		{
			return clienteDatos.ConsultarClientes();
		}

		public bool AgregarCliente(Cliente entidad)
		{
			return clienteDatos.AgregarCliente(entidad);
		}

		public Cliente? BuscarCliente(int codigoCliente)
		{
			return clienteDatos.BuscarCliente(codigoCliente);
		}

		public bool EditarCliente(Cliente entidad)
		{
			return clienteDatos.EditarCliente(entidad);
		}

		public bool EliminarCliente(int codigoCliente)
		{
			return clienteDatos.EliminarCliente(codigoCliente);
		}
	}
}
