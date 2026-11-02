using ViajesMundoMaya.Datos.Datos;
using ViajesMundoMaya.Entidades.Entidades;

namespace ViajesMundoMaya.Negocio.Servicios
{
	public class EmpleadoNegocio
	{
		private readonly EmpleadoDatos empleadoDatos;

		public EmpleadoNegocio()
		{
			empleadoDatos = new EmpleadoDatos();
		}

		public List<Empleado> ConsultarEmpleados()
		{
			return empleadoDatos.ConsultarEmpleados();
		}

		public bool AgregarEmpleado(Empleado entidad)
		{
			return empleadoDatos.AgregarEmpleado(entidad);
		}

		public Empleado? BuscarEmpleado(int codigoEmpleado)
		{
			return empleadoDatos.BuscarEmpleado(codigoEmpleado);
		}

		public bool EditarEmpleado(Empleado entidad)
		{
			return empleadoDatos.EditarEmpleado(entidad);
		}

		public bool EliminarEmpleado(int codigoEmpleado)
		{
			return empleadoDatos.EliminarEmpleado(codigoEmpleado);
		}
	}
}
