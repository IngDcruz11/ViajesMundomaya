using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class TrasladoDatos
	{
		public List<Traslado> ConsultarTraslados()
		{
			List<Traslado> lista = new List<Traslado>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarTraslados", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new Traslado
				{
					CodigoTraslado = Convert.ToInt32(lector["CodigoTraslado"]),
					CodigoPaquete = Convert.ToInt32(lector["CodigoPaquete"]),
					CodigoTipoTransporte = Convert.ToInt32(lector["CodigoTipoTransporte"]),
					Origen = lector["Origen"].ToString() ?? string.Empty,
					Destino = lector["Destino"].ToString() ?? string.Empty,
					FechaServicio = DateOnly.FromDateTime(Convert.ToDateTime(lector["FechaServicio"])),
					HoraSalida = lector["HoraSalida"] == DBNull.Value ? (TimeOnly?)null : TimeOnly.FromTimeSpan((TimeSpan)lector["HoraSalida"]),
					HoraLlegada = lector["HoraLlegada"] == DBNull.Value ? (TimeOnly?)null : TimeOnly.FromTimeSpan((TimeSpan)lector["HoraLlegada"]),
					NombrePaquete = lector["NombrePaquete"].ToString() ?? string.Empty,
					TipoTransporte = lector["TipoTransporte"].ToString() ?? string.Empty
				});
			}

			return lista;
		}

		public bool AgregarTraslado(Traslado entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarTraslado", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPaquete", SqlDbType.Int).Value = entidad.CodigoPaquete;
			comando.Parameters.Add("@CodigoTipoTransporte", SqlDbType.Int).Value = entidad.CodigoTipoTransporte;
			comando.Parameters.Add("@Origen", SqlDbType.VarChar, 150).Value = entidad.Origen;
			comando.Parameters.Add("@Destino", SqlDbType.VarChar, 150).Value = entidad.Destino;
			comando.Parameters.Add("@FechaServicio", SqlDbType.Date).Value = entidad.FechaServicio.ToDateTime(TimeOnly.MinValue);
			comando.Parameters.Add("@HoraSalida", SqlDbType.Time).Value = entidad.HoraSalida.HasValue ? entidad.HoraSalida.Value.ToTimeSpan() : DBNull.Value;
			comando.Parameters.Add("@HoraLlegada", SqlDbType.Time).Value = entidad.HoraLlegada.HasValue ? entidad.HoraLlegada.Value.ToTimeSpan() : DBNull.Value;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public Traslado? BuscarTraslado(int codigoTraslado)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarTrasladoPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoTraslado", SqlDbType.Int).Value = codigoTraslado;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new Traslado
				{
					CodigoTraslado = Convert.ToInt32(lector["CodigoTraslado"]),
					CodigoPaquete = Convert.ToInt32(lector["CodigoPaquete"]),
					CodigoTipoTransporte = Convert.ToInt32(lector["CodigoTipoTransporte"]),
					Origen = lector["Origen"].ToString() ?? string.Empty,
					Destino = lector["Destino"].ToString() ?? string.Empty,
					FechaServicio = DateOnly.FromDateTime(Convert.ToDateTime(lector["FechaServicio"])),
					HoraSalida = lector["HoraSalida"] == DBNull.Value ? (TimeOnly?)null : TimeOnly.FromTimeSpan((TimeSpan)lector["HoraSalida"]),
					HoraLlegada = lector["HoraLlegada"] == DBNull.Value ? (TimeOnly?)null : TimeOnly.FromTimeSpan((TimeSpan)lector["HoraLlegada"]),
					NombrePaquete = lector["NombrePaquete"].ToString() ?? string.Empty,
					TipoTransporte = lector["TipoTransporte"].ToString() ?? string.Empty
				};
			}

			return null;
		}

		public bool EditarTraslado(Traslado entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarTraslado", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoTraslado", SqlDbType.Int).Value = entidad.CodigoTraslado;
			comando.Parameters.Add("@CodigoPaquete", SqlDbType.Int).Value = entidad.CodigoPaquete;
			comando.Parameters.Add("@CodigoTipoTransporte", SqlDbType.Int).Value = entidad.CodigoTipoTransporte;
			comando.Parameters.Add("@Origen", SqlDbType.VarChar, 150).Value = entidad.Origen;
			comando.Parameters.Add("@Destino", SqlDbType.VarChar, 150).Value = entidad.Destino;
			comando.Parameters.Add("@FechaServicio", SqlDbType.Date).Value = entidad.FechaServicio.ToDateTime(TimeOnly.MinValue);
			comando.Parameters.Add("@HoraSalida", SqlDbType.Time).Value = entidad.HoraSalida.HasValue ? entidad.HoraSalida.Value.ToTimeSpan() : DBNull.Value;
			comando.Parameters.Add("@HoraLlegada", SqlDbType.Time).Value = entidad.HoraLlegada.HasValue ? entidad.HoraLlegada.Value.ToTimeSpan() : DBNull.Value;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarTraslado(int codigoTraslado)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarTraslado", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoTraslado", SqlDbType.Int).Value = codigoTraslado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
