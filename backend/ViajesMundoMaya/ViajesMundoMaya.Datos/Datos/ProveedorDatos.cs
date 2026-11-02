using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class ProveedorDatos
	{
		public List<Proveedor> ConsultarProveedores()
		{
			List<Proveedor> lista = new List<Proveedor>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarProveedores", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new Proveedor
				{
					CodigoProveedor = Convert.ToInt32(lector["CodigoProveedor"]),
					NombreProveedor = lector["NombreProveedor"].ToString() ?? string.Empty,
					TipoProveedor = lector["TipoProveedor"].ToString() ?? string.Empty,
					Contacto = lector["Contacto"].ToString() ?? string.Empty,
					Telefono = lector["Telefono"].ToString() ?? string.Empty,
					CorreoElectronico = lector["CorreoElectronico"].ToString() ?? string.Empty,
					Direccion = lector["Direccion"].ToString() ?? string.Empty
				});
			}

			return lista;
		}

		public bool AgregarProveedor(Proveedor entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarProveedor", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@NombreProveedor", SqlDbType.VarChar, 150).Value = entidad.NombreProveedor;
			comando.Parameters.Add("@TipoProveedor", SqlDbType.VarChar, 50).Value = entidad.TipoProveedor;
			comando.Parameters.Add("@Contacto", SqlDbType.VarChar, 100).Value = entidad.Contacto;
			comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = entidad.Telefono;
			comando.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar, 150).Value = entidad.CorreoElectronico;
			comando.Parameters.Add("@Direccion", SqlDbType.VarChar, 250).Value = entidad.Direccion;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public Proveedor? BuscarProveedor(int codigoProveedor)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarProveedorPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = codigoProveedor;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new Proveedor
				{
					CodigoProveedor = Convert.ToInt32(lector["CodigoProveedor"]),
					NombreProveedor = lector["NombreProveedor"].ToString() ?? string.Empty,
					TipoProveedor = lector["TipoProveedor"].ToString() ?? string.Empty,
					Contacto = lector["Contacto"].ToString() ?? string.Empty,
					Telefono = lector["Telefono"].ToString() ?? string.Empty,
					CorreoElectronico = lector["CorreoElectronico"].ToString() ?? string.Empty,
					Direccion = lector["Direccion"].ToString() ?? string.Empty
				};
			}

			return null;
		}

		public bool EditarProveedor(Proveedor entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarProveedor", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = entidad.CodigoProveedor;
			comando.Parameters.Add("@NombreProveedor", SqlDbType.VarChar, 150).Value = entidad.NombreProveedor;
			comando.Parameters.Add("@TipoProveedor", SqlDbType.VarChar, 50).Value = entidad.TipoProveedor;
			comando.Parameters.Add("@Contacto", SqlDbType.VarChar, 100).Value = entidad.Contacto;
			comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = entidad.Telefono;
			comando.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar, 150).Value = entidad.CorreoElectronico;
			comando.Parameters.Add("@Direccion", SqlDbType.VarChar, 250).Value = entidad.Direccion;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarProveedor(int codigoProveedor)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarProveedor", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = codigoProveedor;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
