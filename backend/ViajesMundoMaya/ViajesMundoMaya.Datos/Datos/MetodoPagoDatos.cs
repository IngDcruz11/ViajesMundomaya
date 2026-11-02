using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class MetodoPagoDatos
	{
		public List<MetodoPago> ConsultarMetodosPago()
		{
			List<MetodoPago> lista = new List<MetodoPago>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarMetodosPago", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new MetodoPago
				{
					CodigoMetodoPago = Convert.ToInt32(lector["CodigoMetodoPago"]),
					NombreMetodoPago = lector["NombreMetodoPago"].ToString() ?? string.Empty,
					Tipo = lector["Tipo"].ToString() ?? string.Empty,
					EntidadFinanciera = lector["EntidadFinanciera"].ToString() ?? string.Empty,
					Descripcion = lector["Descripcion"].ToString() ?? string.Empty,
					ComisionPorcentaje = Convert.ToDecimal(lector["ComisionPorcentaje"])
				});
			}

			return lista;
		}

		public bool AgregarMetodoPago(MetodoPago entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarMetodoPago", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@NombreMetodoPago", SqlDbType.VarChar, 100).Value = entidad.NombreMetodoPago;
			comando.Parameters.Add("@Tipo", SqlDbType.VarChar, 50).Value = entidad.Tipo;
			comando.Parameters.Add("@EntidadFinanciera", SqlDbType.VarChar, 150).Value = entidad.EntidadFinanciera;
			comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 200).Value = entidad.Descripcion;
			SqlParameter pComisionPorcentaje = comando.Parameters.Add("@ComisionPorcentaje", SqlDbType.Decimal);
			pComisionPorcentaje.Precision = 6;
			pComisionPorcentaje.Scale = 2;
			pComisionPorcentaje.Value = entidad.ComisionPorcentaje;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public MetodoPago? BuscarMetodoPago(int codigoMetodoPago)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarMetodoPagoPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoMetodoPago", SqlDbType.Int).Value = codigoMetodoPago;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new MetodoPago
				{
					CodigoMetodoPago = Convert.ToInt32(lector["CodigoMetodoPago"]),
					NombreMetodoPago = lector["NombreMetodoPago"].ToString() ?? string.Empty,
					Tipo = lector["Tipo"].ToString() ?? string.Empty,
					EntidadFinanciera = lector["EntidadFinanciera"].ToString() ?? string.Empty,
					Descripcion = lector["Descripcion"].ToString() ?? string.Empty,
					ComisionPorcentaje = Convert.ToDecimal(lector["ComisionPorcentaje"])
				};
			}

			return null;
		}

		public bool EditarMetodoPago(MetodoPago entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarMetodoPago", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoMetodoPago", SqlDbType.Int).Value = entidad.CodigoMetodoPago;
			comando.Parameters.Add("@NombreMetodoPago", SqlDbType.VarChar, 100).Value = entidad.NombreMetodoPago;
			comando.Parameters.Add("@Tipo", SqlDbType.VarChar, 50).Value = entidad.Tipo;
			comando.Parameters.Add("@EntidadFinanciera", SqlDbType.VarChar, 150).Value = entidad.EntidadFinanciera;
			comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 200).Value = entidad.Descripcion;
			SqlParameter pComisionPorcentaje = comando.Parameters.Add("@ComisionPorcentaje", SqlDbType.Decimal);
			pComisionPorcentaje.Precision = 6;
			pComisionPorcentaje.Scale = 2;
			pComisionPorcentaje.Value = entidad.ComisionPorcentaje;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarMetodoPago(int codigoMetodoPago)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarMetodoPago", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoMetodoPago", SqlDbType.Int).Value = codigoMetodoPago;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
