using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class TourDatos
	{
		public List<Tour> ConsultarTours()
		{
			List<Tour> lista = new List<Tour>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarTours", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new Tour
				{
					CodigoTour = Convert.ToInt32(lector["CodigoTour"]),
					CodigoDestino = Convert.ToInt32(lector["CodigoDestino"]),
					NombreTour = lector["NombreTour"].ToString() ?? string.Empty,
					Descripcion = lector["Descripcion"].ToString() ?? string.Empty,
					DuracionHoras = Convert.ToInt32(lector["DuracionHoras"]),
					PrecioPorPersona = Convert.ToDecimal(lector["PrecioPorPersona"]),
					Incluye = lector["Incluye"].ToString() ?? string.Empty,
					NombreDestino = lector["NombreDestino"].ToString() ?? string.Empty
				});
			}

			return lista;
		}

		public bool AgregarTour(Tour entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarTour", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoDestino", SqlDbType.Int).Value = entidad.CodigoDestino;
			comando.Parameters.Add("@NombreTour", SqlDbType.VarChar, 150).Value = entidad.NombreTour;
			comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 300).Value = entidad.Descripcion;
			comando.Parameters.Add("@DuracionHoras", SqlDbType.Int).Value = entidad.DuracionHoras;
			SqlParameter pPrecioPorPersona = comando.Parameters.Add("@PrecioPorPersona", SqlDbType.Decimal);
			pPrecioPorPersona.Precision = 12;
			pPrecioPorPersona.Scale = 2;
			pPrecioPorPersona.Value = entidad.PrecioPorPersona;
			comando.Parameters.Add("@Incluye", SqlDbType.VarChar, 300).Value = entidad.Incluye;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public Tour? BuscarTour(int codigoTour)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarTourPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoTour", SqlDbType.Int).Value = codigoTour;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new Tour
				{
					CodigoTour = Convert.ToInt32(lector["CodigoTour"]),
					CodigoDestino = Convert.ToInt32(lector["CodigoDestino"]),
					NombreTour = lector["NombreTour"].ToString() ?? string.Empty,
					Descripcion = lector["Descripcion"].ToString() ?? string.Empty,
					DuracionHoras = Convert.ToInt32(lector["DuracionHoras"]),
					PrecioPorPersona = Convert.ToDecimal(lector["PrecioPorPersona"]),
					Incluye = lector["Incluye"].ToString() ?? string.Empty,
					NombreDestino = lector["NombreDestino"].ToString() ?? string.Empty
				};
			}

			return null;
		}

		public bool EditarTour(Tour entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarTour", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoTour", SqlDbType.Int).Value = entidad.CodigoTour;
			comando.Parameters.Add("@CodigoDestino", SqlDbType.Int).Value = entidad.CodigoDestino;
			comando.Parameters.Add("@NombreTour", SqlDbType.VarChar, 150).Value = entidad.NombreTour;
			comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 300).Value = entidad.Descripcion;
			comando.Parameters.Add("@DuracionHoras", SqlDbType.Int).Value = entidad.DuracionHoras;
			SqlParameter pPrecioPorPersona = comando.Parameters.Add("@PrecioPorPersona", SqlDbType.Decimal);
			pPrecioPorPersona.Precision = 12;
			pPrecioPorPersona.Scale = 2;
			pPrecioPorPersona.Value = entidad.PrecioPorPersona;
			comando.Parameters.Add("@Incluye", SqlDbType.VarChar, 300).Value = entidad.Incluye;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarTour(int codigoTour)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarTour", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoTour", SqlDbType.Int).Value = codigoTour;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
