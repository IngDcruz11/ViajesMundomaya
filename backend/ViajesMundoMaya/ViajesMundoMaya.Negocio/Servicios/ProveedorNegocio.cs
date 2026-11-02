using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class ProveedorNegocio
	{
		private readonly ProveedorDatos proveedorDatos;

		public ProveedorNegocio()
		{
			proveedorDatos = new ProveedorDatos();
		}

		public List<Proveedor> ConsultarProveedores()
		{
			return proveedorDatos.ConsultarProveedores();
		}

		public bool AgregarProveedor(Proveedor entidad)
		{
			return proveedorDatos.AgregarProveedor(entidad);
		}

		public Proveedor? BuscarProveedor(int codigoProveedor)
		{
			return proveedorDatos.BuscarProveedor(codigoProveedor);
		}

		public bool EditarProveedor(Proveedor entidad)
		{
			return proveedorDatos.EditarProveedor(entidad);
		}

		public bool EliminarProveedor(int codigoProveedor)
		{
			return proveedorDatos.EliminarProveedor(codigoProveedor);
		}
	}
}
