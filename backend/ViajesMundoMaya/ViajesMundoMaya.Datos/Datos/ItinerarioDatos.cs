using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class ItinerarioDatos
	{
		public List<Itinerario> ConsultarItinerarios()
		{
			List<Itinerario> lista = new List<Itinerario>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarItinerarios", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new Itinerario
				{
					CodigoItinerario = Convert.ToInt32(lector["CodigoItinerario"]),
					CodigoPaquete = Convert.ToInt32(lector["CodigoPaquete"]),
					Dia = Convert.ToInt32(lector["Dia"]),
					DescripcionActividad = lector["DescripcionActividad"].ToString() ?? string.Empty,
					Lugar = lector["Lugar"].ToString() ?? string.Empty,
					HoraInicio = lector["HoraInicio"] == DBNull.Value ? (TimeOnly?)null : TimeOnly.FromTimeSpan((TimeSpan)lector["HoraInicio"]),
					HoraFin = lector["HoraFin"] == DBNull.Value ? (TimeOnly?)null : TimeOnly.FromTimeSpan((TimeSpan)lector["HoraFin"]),
					NombrePaquete = lector["NombrePaquete"].ToString() ?? string.Empty
				});
			}

			return lista;
		}

		public bool AgregarItinerario(Itinerario entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarItinerario", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPaquete", SqlDbType.Int).Value = entidad.CodigoPaquete;
			comando.Parameters.Add("@Dia", SqlDbType.Int).Value = entidad.Dia;
			comando.Parameters.Add("@DescripcionActividad", SqlDbType.VarChar, 300).Value = entidad.DescripcionActividad;
			comando.Parameters.Add("@Lugar", SqlDbType.VarChar, 150).Value = entidad.Lugar;
			comando.Parameters.Add("@HoraInicio", SqlDbType.Time).Value = entidad.HoraInicio.HasValue ? entidad.HoraInicio.Value.ToTimeSpan() : DBNull.Value;
			comando.Parameters.Add("@HoraFin", SqlDbType.Time).Value = entidad.HoraFin.HasValue ? entidad.HoraFin.Value.ToTimeSpan() : DBNull.Value;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public Itinerario? BuscarItinerario(int codigoItinerario)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarItinerarioPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoItinerario", SqlDbType.Int).Value = codigoItinerario;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new Itinerario
				{
					CodigoItinerario = Convert.ToInt32(lector["CodigoItinerario"]),
					CodigoPaquete = Convert.ToInt32(lector["CodigoPaquete"]),
					Dia = Convert.ToInt32(lector["Dia"]),
					DescripcionActividad = lector["DescripcionActividad"].ToString() ?? string.Empty,
					Lugar = lector["Lugar"].ToString() ?? string.Empty,
					HoraInicio = lector["HoraInicio"] == DBNull.Value ? (TimeOnly?)null : TimeOnly.FromTimeSpan((TimeSpan)lector["HoraInicio"]),
					HoraFin = lector["HoraFin"] == DBNull.Value ? (TimeOnly?)null : TimeOnly.FromTimeSpan((TimeSpan)lector["HoraFin"]),
					NombrePaquete = lector["NombrePaquete"].ToString() ?? string.Empty
				};
			}

			return null;
		}

		public bool EditarItinerario(Itinerario entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarItinerario", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoItinerario", SqlDbType.Int).Value = entidad.CodigoItinerario;
			comando.Parameters.Add("@CodigoPaquete", SqlDbType.Int).Value = entidad.CodigoPaquete;
			comando.Parameters.Add("@Dia", SqlDbType.Int).Value = entidad.Dia;
			comando.Parameters.Add("@DescripcionActividad", SqlDbType.VarChar, 300).Value = entidad.DescripcionActividad;
			comando.Parameters.Add("@Lugar", SqlDbType.VarChar, 150).Value = entidad.Lugar;
			comando.Parameters.Add("@HoraInicio", SqlDbType.Time).Value = entidad.HoraInicio.HasValue ? entidad.HoraInicio.Value.ToTimeSpan() : DBNull.Value;
			comando.Parameters.Add("@HoraFin", SqlDbType.Time).Value = entidad.HoraFin.HasValue ? entidad.HoraFin.Value.ToTimeSpan() : DBNull.Value;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarItinerario(int codigoItinerario)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarItinerario", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoItinerario", SqlDbType.Int).Value = codigoItinerario;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
