using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class PaqueteDatos
	{
		public List<Paquete> ConsultarPaquetes()
		{
			List<Paquete> lista = new List<Paquete>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarPaquetes", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new Paquete
				{
					CodigoPaquete = Convert.ToInt32(lector["CodigoPaquete"]),
					CodigoProveedor = Convert.ToInt32(lector["CodigoProveedor"]),
					NombrePaquete = lector["NombrePaquete"].ToString() ?? string.Empty,
					Descripcion = lector["Descripcion"].ToString() ?? string.Empty,
					PrecioBase = Convert.ToDecimal(lector["PrecioBase"]),
					DuracionDias = Convert.ToInt32(lector["DuracionDias"]),
					Incluye = lector["Incluye"].ToString() ?? string.Empty,
					Imagen = lector["Imagen"].ToString() ?? string.Empty,
					NombreProveedor = lector["NombreProveedor"].ToString() ?? string.Empty
				});
			}

			return lista;
		}

		public bool AgregarPaquete(Paquete entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarPaquete", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = entidad.CodigoProveedor;
			comando.Parameters.Add("@NombrePaquete", SqlDbType.VarChar, 150).Value = entidad.NombrePaquete;
			comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 300).Value = entidad.Descripcion;
			SqlParameter pPrecioBase = comando.Parameters.Add("@PrecioBase", SqlDbType.Decimal);
			pPrecioBase.Precision = 12;
			pPrecioBase.Scale = 2;
			pPrecioBase.Value = entidad.PrecioBase;
			comando.Parameters.Add("@DuracionDias", SqlDbType.Int).Value = entidad.DuracionDias;
			comando.Parameters.Add("@Incluye", SqlDbType.VarChar, 300).Value = entidad.Incluye;
			comando.Parameters.Add("@Imagen", SqlDbType.VarChar, 300).Value = entidad.Imagen;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public Paquete? BuscarPaquete(int codigoPaquete)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarPaquetePorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPaquete", SqlDbType.Int).Value = codigoPaquete;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new Paquete
				{
					CodigoPaquete = Convert.ToInt32(lector["CodigoPaquete"]),
					CodigoProveedor = Convert.ToInt32(lector["CodigoProveedor"]),
					NombrePaquete = lector["NombrePaquete"].ToString() ?? string.Empty,
					Descripcion = lector["Descripcion"].ToString() ?? string.Empty,
					PrecioBase = Convert.ToDecimal(lector["PrecioBase"]),
					DuracionDias = Convert.ToInt32(lector["DuracionDias"]),
					Incluye = lector["Incluye"].ToString() ?? string.Empty,
					Imagen = lector["Imagen"].ToString() ?? string.Empty,
					NombreProveedor = lector["NombreProveedor"].ToString() ?? string.Empty
				};
			}

			return null;
		}

		public bool EditarPaquete(Paquete entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarPaquete", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPaquete", SqlDbType.Int).Value = entidad.CodigoPaquete;
			comando.Parameters.Add("@CodigoProveedor", SqlDbType.Int).Value = entidad.CodigoProveedor;
			comando.Parameters.Add("@NombrePaquete", SqlDbType.VarChar, 150).Value = entidad.NombrePaquete;
			comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 300).Value = entidad.Descripcion;
			SqlParameter pPrecioBase = comando.Parameters.Add("@PrecioBase", SqlDbType.Decimal);
			pPrecioBase.Precision = 12;
			pPrecioBase.Scale = 2;
			pPrecioBase.Value = entidad.PrecioBase;
			comando.Parameters.Add("@DuracionDias", SqlDbType.Int).Value = entidad.DuracionDias;
			comando.Parameters.Add("@Incluye", SqlDbType.VarChar, 300).Value = entidad.Incluye;
			comando.Parameters.Add("@Imagen", SqlDbType.VarChar, 300).Value = entidad.Imagen;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarPaquete(int codigoPaquete)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarPaquete", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPaquete", SqlDbType.Int).Value = codigoPaquete;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
