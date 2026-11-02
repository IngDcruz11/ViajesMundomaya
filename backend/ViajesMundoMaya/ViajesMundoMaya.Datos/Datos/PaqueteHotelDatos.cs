using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class PaqueteHotelDatos
	{
		public List<PaqueteHotel> ConsultarPaqueteHoteles()
		{
			List<PaqueteHotel> lista = new List<PaqueteHotel>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarPaqueteHoteles", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new PaqueteHotel
				{
					CodigoPaqueteHotel = Convert.ToInt32(lector["CodigoPaqueteHotel"]),
					CodigoPaquete = Convert.ToInt32(lector["CodigoPaquete"]),
					CodigoHotel = Convert.ToInt32(lector["CodigoHotel"]),
					Noches = Convert.ToInt32(lector["Noches"]),
					TipoHabitacion = lector["TipoHabitacion"].ToString() ?? string.Empty,
					IncluyeDesayuno = Convert.ToBoolean(lector["IncluyeDesayuno"]),
					PrecioPorNoche = Convert.ToDecimal(lector["PrecioPorNoche"]),
					NombrePaquete = lector["NombrePaquete"].ToString() ?? string.Empty,
					NombreHotel = lector["NombreHotel"].ToString() ?? string.Empty
				});
			}

			return lista;
		}

		public bool AgregarPaqueteHotel(PaqueteHotel entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarPaqueteHotel", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPaquete", SqlDbType.Int).Value = entidad.CodigoPaquete;
			comando.Parameters.Add("@CodigoHotel", SqlDbType.Int).Value = entidad.CodigoHotel;
			comando.Parameters.Add("@Noches", SqlDbType.Int).Value = entidad.Noches;
			comando.Parameters.Add("@TipoHabitacion", SqlDbType.VarChar, 100).Value = entidad.TipoHabitacion;
			comando.Parameters.Add("@IncluyeDesayuno", SqlDbType.Bit).Value = entidad.IncluyeDesayuno;
			SqlParameter pPrecioPorNoche = comando.Parameters.Add("@PrecioPorNoche", SqlDbType.Decimal);
			pPrecioPorNoche.Precision = 12;
			pPrecioPorNoche.Scale = 2;
			pPrecioPorNoche.Value = entidad.PrecioPorNoche;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public PaqueteHotel? BuscarPaqueteHotel(int codigoPaqueteHotel)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarPaqueteHotelPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPaqueteHotel", SqlDbType.Int).Value = codigoPaqueteHotel;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new PaqueteHotel
				{
					CodigoPaqueteHotel = Convert.ToInt32(lector["CodigoPaqueteHotel"]),
					CodigoPaquete = Convert.ToInt32(lector["CodigoPaquete"]),
					CodigoHotel = Convert.ToInt32(lector["CodigoHotel"]),
					Noches = Convert.ToInt32(lector["Noches"]),
					TipoHabitacion = lector["TipoHabitacion"].ToString() ?? string.Empty,
					IncluyeDesayuno = Convert.ToBoolean(lector["IncluyeDesayuno"]),
					PrecioPorNoche = Convert.ToDecimal(lector["PrecioPorNoche"]),
					NombrePaquete = lector["NombrePaquete"].ToString() ?? string.Empty,
					NombreHotel = lector["NombreHotel"].ToString() ?? string.Empty
				};
			}

			return null;
		}

		public bool EditarPaqueteHotel(PaqueteHotel entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarPaqueteHotel", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPaqueteHotel", SqlDbType.Int).Value = entidad.CodigoPaqueteHotel;
			comando.Parameters.Add("@CodigoPaquete", SqlDbType.Int).Value = entidad.CodigoPaquete;
			comando.Parameters.Add("@CodigoHotel", SqlDbType.Int).Value = entidad.CodigoHotel;
			comando.Parameters.Add("@Noches", SqlDbType.Int).Value = entidad.Noches;
			comando.Parameters.Add("@TipoHabitacion", SqlDbType.VarChar, 100).Value = entidad.TipoHabitacion;
			comando.Parameters.Add("@IncluyeDesayuno", SqlDbType.Bit).Value = entidad.IncluyeDesayuno;
			SqlParameter pPrecioPorNoche = comando.Parameters.Add("@PrecioPorNoche", SqlDbType.Decimal);
			pPrecioPorNoche.Precision = 12;
			pPrecioPorNoche.Scale = 2;
			pPrecioPorNoche.Value = entidad.PrecioPorNoche;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarPaqueteHotel(int codigoPaqueteHotel)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarPaqueteHotel", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPaqueteHotel", SqlDbType.Int).Value = codigoPaqueteHotel;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
