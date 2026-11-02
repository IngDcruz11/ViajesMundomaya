using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class PagoDatos
	{
		public List<Pago> ConsultarPagos()
		{
			List<Pago> lista = new List<Pago>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarPagos", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new Pago
				{
					CodigoPago = Convert.ToInt32(lector["CodigoPago"]),
					CodigoReserva = Convert.ToInt32(lector["CodigoReserva"]),
					CodigoMetodoPago = Convert.ToInt32(lector["CodigoMetodoPago"]),
					Monto = Convert.ToDecimal(lector["Monto"]),
					Referencia = lector["Referencia"].ToString() ?? string.Empty,
					NumeroTransaccion = lector["NumeroTransaccion"].ToString() ?? string.Empty,
					EstadoPago = lector["EstadoPago"].ToString() ?? string.Empty,
					FechaPago = Convert.ToDateTime(lector["FechaPago"]),
					NombreMetodoPago = lector["NombreMetodoPago"].ToString() ?? string.Empty
				});
			}

			return lista;
		}

		public bool AgregarPago(Pago entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarPago", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoReserva", SqlDbType.Int).Value = entidad.CodigoReserva;
			comando.Parameters.Add("@CodigoMetodoPago", SqlDbType.Int).Value = entidad.CodigoMetodoPago;
			SqlParameter pMonto = comando.Parameters.Add("@Monto", SqlDbType.Decimal);
			pMonto.Precision = 12;
			pMonto.Scale = 2;
			pMonto.Value = entidad.Monto;
			comando.Parameters.Add("@Referencia", SqlDbType.VarChar, 100).Value = entidad.Referencia;
			comando.Parameters.Add("@NumeroTransaccion", SqlDbType.VarChar, 100).Value = entidad.NumeroTransaccion;
			comando.Parameters.Add("@EstadoPago", SqlDbType.VarChar, 20).Value = entidad.EstadoPago;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public Pago? BuscarPago(int codigoPago)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarPagoPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPago", SqlDbType.Int).Value = codigoPago;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new Pago
				{
					CodigoPago = Convert.ToInt32(lector["CodigoPago"]),
					CodigoReserva = Convert.ToInt32(lector["CodigoReserva"]),
					CodigoMetodoPago = Convert.ToInt32(lector["CodigoMetodoPago"]),
					Monto = Convert.ToDecimal(lector["Monto"]),
					Referencia = lector["Referencia"].ToString() ?? string.Empty,
					NumeroTransaccion = lector["NumeroTransaccion"].ToString() ?? string.Empty,
					EstadoPago = lector["EstadoPago"].ToString() ?? string.Empty,
					FechaPago = Convert.ToDateTime(lector["FechaPago"]),
					NombreMetodoPago = lector["NombreMetodoPago"].ToString() ?? string.Empty
				};
			}

			return null;
		}

		public bool EditarPago(Pago entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarPago", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPago", SqlDbType.Int).Value = entidad.CodigoPago;
			comando.Parameters.Add("@CodigoReserva", SqlDbType.Int).Value = entidad.CodigoReserva;
			comando.Parameters.Add("@CodigoMetodoPago", SqlDbType.Int).Value = entidad.CodigoMetodoPago;
			SqlParameter pMonto = comando.Parameters.Add("@Monto", SqlDbType.Decimal);
			pMonto.Precision = 12;
			pMonto.Scale = 2;
			pMonto.Value = entidad.Monto;
			comando.Parameters.Add("@Referencia", SqlDbType.VarChar, 100).Value = entidad.Referencia;
			comando.Parameters.Add("@NumeroTransaccion", SqlDbType.VarChar, 100).Value = entidad.NumeroTransaccion;
			comando.Parameters.Add("@EstadoPago", SqlDbType.VarChar, 20).Value = entidad.EstadoPago;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarPago(int codigoPago)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarPago", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPago", SqlDbType.Int).Value = codigoPago;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
