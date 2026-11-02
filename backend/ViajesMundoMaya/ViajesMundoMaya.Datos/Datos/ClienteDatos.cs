using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class ClienteDatos
	{
		public List<Cliente> ConsultarClientes()
		{
			List<Cliente> lista = new List<Cliente>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarClientes", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new Cliente
				{
					CodigoCliente = Convert.ToInt32(lector["CodigoCliente"]),
					Nombres = lector["Nombres"].ToString() ?? string.Empty,
					Apellidos = lector["Apellidos"].ToString() ?? string.Empty,
					DPI = lector["DPI"].ToString() ?? string.Empty,
					Telefono = lector["Telefono"].ToString() ?? string.Empty,
					CorreoElectronico = lector["CorreoElectronico"].ToString() ?? string.Empty,
					Direccion = lector["Direccion"].ToString() ?? string.Empty,
					FechaRegistro = DateOnly.FromDateTime(Convert.ToDateTime(lector["FechaRegistro"]))
				});
			}

			return lista;
		}

		public bool AgregarCliente(Cliente entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarCliente", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@Nombres", SqlDbType.VarChar, 100).Value = entidad.Nombres;
			comando.Parameters.Add("@Apellidos", SqlDbType.VarChar, 100).Value = entidad.Apellidos;
			comando.Parameters.Add("@DPI", SqlDbType.VarChar, 20).Value = entidad.DPI;
			comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = entidad.Telefono;
			comando.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar, 150).Value = entidad.CorreoElectronico;
			comando.Parameters.Add("@Direccion", SqlDbType.VarChar, 250).Value = entidad.Direccion;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public Cliente? BuscarCliente(int codigoCliente)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarClientePorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoCliente", SqlDbType.Int).Value = codigoCliente;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new Cliente
				{
					CodigoCliente = Convert.ToInt32(lector["CodigoCliente"]),
					Nombres = lector["Nombres"].ToString() ?? string.Empty,
					Apellidos = lector["Apellidos"].ToString() ?? string.Empty,
					DPI = lector["DPI"].ToString() ?? string.Empty,
					Telefono = lector["Telefono"].ToString() ?? string.Empty,
					CorreoElectronico = lector["CorreoElectronico"].ToString() ?? string.Empty,
					Direccion = lector["Direccion"].ToString() ?? string.Empty,
					FechaRegistro = DateOnly.FromDateTime(Convert.ToDateTime(lector["FechaRegistro"]))
				};
			}

			return null;
		}

		public bool EditarCliente(Cliente entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarCliente", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoCliente", SqlDbType.Int).Value = entidad.CodigoCliente;
			comando.Parameters.Add("@Nombres", SqlDbType.VarChar, 100).Value = entidad.Nombres;
			comando.Parameters.Add("@Apellidos", SqlDbType.VarChar, 100).Value = entidad.Apellidos;
			comando.Parameters.Add("@DPI", SqlDbType.VarChar, 20).Value = entidad.DPI;
			comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = entidad.Telefono;
			comando.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar, 150).Value = entidad.CorreoElectronico;
			comando.Parameters.Add("@Direccion", SqlDbType.VarChar, 250).Value = entidad.Direccion;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarCliente(int codigoCliente)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarCliente", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoCliente", SqlDbType.Int).Value = codigoCliente;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
