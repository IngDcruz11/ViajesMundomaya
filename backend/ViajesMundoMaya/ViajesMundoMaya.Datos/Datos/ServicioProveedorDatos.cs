using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class ServicioProveedorDatos
	{
		public List<ServicioProveedor> ConsultarServiciosProveedor()
		{
			List<ServicioProveedor> lista = new List<ServicioProveedor>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarServiciosProveedor", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new ServicioProveedor
				{
					CodigoServicioProv = Convert.ToInt32(lector["CodigoServicioProv"]),
					CodigoProveedor = Convert.ToInt32(lector["CodigoProveedor"]),
					TipoServicio = lector["TipoServicio"].ToString() ?? string.Empty,
					Descripcion = lector["Descripcion"].ToString() ?? string.Empty,
					PrecioReferencia = Convert.ToDecimal(lector["PrecioReferencia"]),
					Moneda = lector["Moneda"].ToString() ?? string.Empty,
					NombreProveedor = lector["NombreProveedor"].ToString() ?? string.Empty
				});
			}

			return lista;
		}

		public bool AgregarServicioProveedor(ServicioProveedor entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarServicioProveedor", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = entidad.CodigoProveedor;
			comando.Parameters.Add("@TipoServicio", SqlDbType.VarChar, 50).Value = entidad.TipoServicio;
			comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 300).Value = entidad.Descripcion;
			SqlParameter pPrecioReferencia = comando.Parameters.Add("@PrecioReferencia", SqlDbType.Decimal);
			pPrecioReferencia.Precision = 12;
			pPrecioReferencia.Scale = 2;
			pPrecioReferencia.Value = entidad.PrecioReferencia;
			comando.Parameters.Add("@Moneda", SqlDbType.VarChar, 10).Value = entidad.Moneda;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public ServicioProveedor? BuscarServicioProveedor(int codigoServicioProv)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarServicioProveedorPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoServicioProv", SqlDbType.Int).Value = codigoServicioProv;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new ServicioProveedor
				{
					CodigoServicioProv = Convert.ToInt32(lector["CodigoServicioProv"]),
					CodigoProveedor = Convert.ToInt32(lector["CodigoProveedor"]),
					TipoServicio = lector["TipoServicio"].ToString() ?? string.Empty,
					Descripcion = lector["Descripcion"].ToString() ?? string.Empty,
					PrecioReferencia = Convert.ToDecimal(lector["PrecioReferencia"]),
					Moneda = lector["Moneda"].ToString() ?? string.Empty,
					NombreProveedor = lector["NombreProveedor"].ToString() ?? string.Empty
				};
			}

			return null;
		}

		public bool EditarServicioProveedor(ServicioProveedor entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarServicioProveedor", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoServicioProv", SqlDbType.Int).Value = entidad.CodigoServicioProv;
			comando.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = entidad.CodigoProveedor;
			comando.Parameters.Add("@TipoServicio", SqlDbType.VarChar, 50).Value = entidad.TipoServicio;
			comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 300).Value = entidad.Descripcion;
			SqlParameter pPrecioReferencia = comando.Parameters.Add("@PrecioReferencia", SqlDbType.Decimal);
			pPrecioReferencia.Precision = 12;
			pPrecioReferencia.Scale = 2;
			pPrecioReferencia.Value = entidad.PrecioReferencia;
			comando.Parameters.Add("@Moneda", SqlDbType.VarChar, 10).Value = entidad.Moneda;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarServicioProveedor(int codigoServicioProv)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarServicioProveedor", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoServicioProv", SqlDbType.Int).Value = codigoServicioProv;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
