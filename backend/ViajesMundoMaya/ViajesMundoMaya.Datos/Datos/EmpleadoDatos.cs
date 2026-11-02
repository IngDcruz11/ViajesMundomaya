using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class EmpleadoDatos
	{
		public List<Empleado> ConsultarEmpleados()
		{
			List<Empleado> lista = new List<Empleado>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarEmpleados", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new Empleado
				{
					CodigoEmpleado = Convert.ToInt32(lector["CodigoEmpleado"]),
					Nombres = lector["Nombres"].ToString() ?? string.Empty,
					Apellidos = lector["Apellidos"].ToString() ?? string.Empty,
					Puesto = lector["Puesto"].ToString() ?? string.Empty,
					Telefono = lector["Telefono"].ToString() ?? string.Empty,
					CorreoElectronico = lector["CorreoElectronico"].ToString() ?? string.Empty,
					FechaContratacion = DateOnly.FromDateTime(Convert.ToDateTime(lector["FechaContratacion"])),
					Salario = Convert.ToDecimal(lector["Salario"])
				});
			}

			return lista;
		}

		public bool AgregarEmpleado(Empleado entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarEmpleado", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@Nombres", SqlDbType.VarChar, 100).Value = entidad.Nombres;
			comando.Parameters.Add("@Apellidos", SqlDbType.VarChar, 100).Value = entidad.Apellidos;
			comando.Parameters.Add("@Puesto", SqlDbType.VarChar, 100).Value = entidad.Puesto;
			comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = entidad.Telefono;
			comando.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar, 150).Value = entidad.CorreoElectronico;
			comando.Parameters.Add("@FechaContratacion", SqlDbType.Date).Value = entidad.FechaContratacion.ToDateTime(TimeOnly.MinValue);
			SqlParameter pSalario = comando.Parameters.Add("@Salario", SqlDbType.Decimal);
			pSalario.Precision = 12;
			pSalario.Scale = 2;
			pSalario.Value = entidad.Salario;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public Empleado? BuscarEmpleado(int codigoEmpleado)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarEmpleadoPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoEmpleado", SqlDbType.Int).Value = codigoEmpleado;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new Empleado
				{
					CodigoEmpleado = Convert.ToInt32(lector["CodigoEmpleado"]),
					Nombres = lector["Nombres"].ToString() ?? string.Empty,
					Apellidos = lector["Apellidos"].ToString() ?? string.Empty,
					Puesto = lector["Puesto"].ToString() ?? string.Empty,
					Telefono = lector["Telefono"].ToString() ?? string.Empty,
					CorreoElectronico = lector["CorreoElectronico"].ToString() ?? string.Empty,
					FechaContratacion = DateOnly.FromDateTime(Convert.ToDateTime(lector["FechaContratacion"])),
					Salario = Convert.ToDecimal(lector["Salario"])
				};
			}

			return null;
		}

		public bool EditarEmpleado(Empleado entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarEmpleado", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoEmpleado", SqlDbType.Int).Value = entidad.CodigoEmpleado;
			comando.Parameters.Add("@Nombres", SqlDbType.VarChar, 100).Value = entidad.Nombres;
			comando.Parameters.Add("@Apellidos", SqlDbType.VarChar, 100).Value = entidad.Apellidos;
			comando.Parameters.Add("@Puesto", SqlDbType.VarChar, 100).Value = entidad.Puesto;
			comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = entidad.Telefono;
			comando.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar, 150).Value = entidad.CorreoElectronico;
			comando.Parameters.Add("@FechaContratacion", SqlDbType.Date).Value = entidad.FechaContratacion.ToDateTime(TimeOnly.MinValue);
			SqlParameter pSalario = comando.Parameters.Add("@Salario", SqlDbType.Decimal);
			pSalario.Precision = 12;
			pSalario.Scale = 2;
			pSalario.Value = entidad.Salario;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarEmpleado(int codigoEmpleado)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarEmpleado", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoEmpleado", SqlDbType.Int).Value = codigoEmpleado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
