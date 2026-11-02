using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class ServicioProveedorNegocio
	{
		private readonly ServicioProveedorDatos servicioProveedorDatos;

		public ServicioProveedorNegocio()
		{
			servicioProveedorDatos = new ServicioProveedorDatos();
		}

		public List<ServicioProveedor> ConsultarServiciosProveedor()
		{
			return servicioProveedorDatos.ConsultarServiciosProveedor();
		}

		public bool AgregarServicioProveedor(ServicioProveedor entidad)
		{
			return servicioProveedorDatos.AgregarServicioProveedor(entidad);
		}

		public ServicioProveedor? BuscarServicioProveedor(int codigoServicioProv)
		{
			return servicioProveedorDatos.BuscarServicioProveedor(codigoServicioProv);
		}

		public bool EditarServicioProveedor(ServicioProveedor entidad)
		{
			return servicioProveedorDatos.EditarServicioProveedor(entidad);
		}

		public bool EliminarServicioProveedor(int codigoServicioProv)
		{
			return servicioProveedorDatos.EliminarServicioProveedor(codigoServicioProv);
		}
	}
}
