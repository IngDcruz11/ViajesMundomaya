using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class ReservaPersonaDatos
	{
		public List<ReservaPersona> ConsultarReservaPersonas()
		{
			List<ReservaPersona> lista = new List<ReservaPersona>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarReservaPersonas", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new ReservaPersona
				{
					CodigoReservaPersona = Convert.ToInt32(lector["CodigoReservaPersona"]),
					CodigoReserva = Convert.ToInt32(lector["CodigoReserva"]),
					Nombres = lector["Nombres"].ToString() ?? string.Empty,
					Apellidos = lector["Apellidos"].ToString() ?? string.Empty,
					DPI = lector["DPI"].ToString() ?? string.Empty,
					FechaNacimiento = lector["FechaNacimiento"] == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime(Convert.ToDateTime(lector["FechaNacimiento"])),
					Parentesco = lector["Parentesco"].ToString() ?? string.Empty,
					Edad = lector["Edad"] == DBNull.Value ? (int?)null : Convert.ToInt32(lector["Edad"])
				});
			}

			return lista;
		}

		public bool AgregarReservaPersona(ReservaPersona entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarReservaPersona", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoReserva", SqlDbType.Int).Value = entidad.CodigoReserva;
			comando.Parameters.Add("@Nombres", SqlDbType.VarChar, 100).Value = entidad.Nombres;
			comando.Parameters.Add("@Apellidos", SqlDbType.VarChar, 100).Value = entidad.Apellidos;
			comando.Parameters.Add("@DPI", SqlDbType.VarChar, 20).Value = entidad.DPI;
			comando.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = entidad.FechaNacimiento.HasValue ? entidad.FechaNacimiento.Value.ToDateTime(TimeOnly.MinValue) : DBNull.Value;
			comando.Parameters.Add("@Parentesco", SqlDbType.VarChar, 50).Value = entidad.Parentesco;
			comando.Parameters.Add("@Edad", SqlDbType.Int).Value = (object?)entidad.Edad ?? DBNull.Value;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public ReservaPersona? BuscarReservaPersona(int codigoReservaPersona)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarReservaPersonaPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoReservaPersona", SqlDbType.Int).Value = codigoReservaPersona;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new ReservaPersona
				{
					CodigoReservaPersona = Convert.ToInt32(lector["CodigoReservaPersona"]),
					CodigoReserva = Convert.ToInt32(lector["CodigoReserva"]),
					Nombres = lector["Nombres"].ToString() ?? string.Empty,
					Apellidos = lector["Apellidos"].ToString() ?? string.Empty,
					DPI = lector["DPI"].ToString() ?? string.Empty,
					FechaNacimiento = lector["FechaNacimiento"] == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime(Convert.ToDateTime(lector["FechaNacimiento"])),
					Parentesco = lector["Parentesco"].ToString() ?? string.Empty,
					Edad = lector["Edad"] == DBNull.Value ? (int?)null : Convert.ToInt32(lector["Edad"])
				};
			}

			return null;
		}

		public bool EditarReservaPersona(ReservaPersona entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarReservaPersona", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoReservaPersona", SqlDbType.Int).Value = entidad.CodigoReservaPersona;
			comando.Parameters.Add("@CodigoReserva", SqlDbType.Int).Value = entidad.CodigoReserva;
			comando.Parameters.Add("@Nombres", SqlDbType.VarChar, 100).Value = entidad.Nombres;
			comando.Parameters.Add("@Apellidos", SqlDbType.VarChar, 100).Value = entidad.Apellidos;
			comando.Parameters.Add("@DPI", SqlDbType.VarChar, 20).Value = entidad.DPI;
			comando.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = entidad.FechaNacimiento.HasValue ? entidad.FechaNacimiento.Value.ToDateTime(TimeOnly.MinValue) : DBNull.Value;
			comando.Parameters.Add("@Parentesco", SqlDbType.VarChar, 50).Value = entidad.Parentesco;
			comando.Parameters.Add("@Edad", SqlDbType.Int).Value = (object?)entidad.Edad ?? DBNull.Value;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarReservaPersona(int codigoReservaPersona)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarReservaPersona", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoReservaPersona", SqlDbType.Int).Value = codigoReservaPersona;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
