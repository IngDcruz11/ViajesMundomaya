using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class PaqueteTourDatos
	{
		public List<PaqueteTour> ConsultarPaqueteTours()
		{
			List<PaqueteTour> lista = new List<PaqueteTour>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarPaqueteTours", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new PaqueteTour
				{
					CodigoPaqueteTour = Convert.ToInt32(lector["CodigoPaqueteTour"]),
					CodigoPaquete = Convert.ToInt32(lector["CodigoPaquete"]),
					CodigoTour = Convert.ToInt32(lector["CodigoTour"]),
					Incluido = Convert.ToBoolean(lector["Incluido"]),
					Orden = Convert.ToInt32(lector["Orden"]),
					Observaciones = lector["Observaciones"].ToString() ?? string.Empty,
					NombrePaquete = lector["NombrePaquete"].ToString() ?? string.Empty,
					NombreTour = lector["NombreTour"].ToString() ?? string.Empty
				});
			}

			return lista;
		}

		public bool AgregarPaqueteTour(PaqueteTour entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarPaqueteTour", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPaquete", SqlDbType.Int).Value = entidad.CodigoPaquete;
			comando.Parameters.Add("@CodigoTour", SqlDbType.Int).Value = entidad.CodigoTour;
			comando.Parameters.Add("@Incluido", SqlDbType.Bit).Value = entidad.Incluido;
			comando.Parameters.Add("@Orden", SqlDbType.Int).Value = entidad.Orden;
			comando.Parameters.Add("@Observaciones", SqlDbType.VarChar, 200).Value = entidad.Observaciones;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public PaqueteTour? BuscarPaqueteTour(int codigoPaqueteTour)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarPaqueteTourPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPaqueteTour", SqlDbType.Int).Value = codigoPaqueteTour;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new PaqueteTour
				{
					CodigoPaqueteTour = Convert.ToInt32(lector["CodigoPaqueteTour"]),
					CodigoPaquete = Convert.ToInt32(lector["CodigoPaquete"]),
					CodigoTour = Convert.ToInt32(lector["CodigoTour"]),
					Incluido = Convert.ToBoolean(lector["Incluido"]),
					Orden = Convert.ToInt32(lector["Orden"]),
					Observaciones = lector["Observaciones"].ToString() ?? string.Empty,
					NombrePaquete = lector["NombrePaquete"].ToString() ?? string.Empty,
					NombreTour = lector["NombreTour"].ToString() ?? string.Empty
				};
			}

			return null;
		}

		public bool EditarPaqueteTour(PaqueteTour entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarPaqueteTour", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPaqueteTour", SqlDbType.Int).Value = entidad.CodigoPaqueteTour;
			comando.Parameters.Add("@CodigoPaquete", SqlDbType.Int).Value = entidad.CodigoPaquete;
			comando.Parameters.Add("@CodigoTour", SqlDbType.Int).Value = entidad.CodigoTour;
			comando.Parameters.Add("@Incluido", SqlDbType.Bit).Value = entidad.Incluido;
			comando.Parameters.Add("@Orden", SqlDbType.Int).Value = entidad.Orden;
			comando.Parameters.Add("@Observaciones", SqlDbType.VarChar, 200).Value = entidad.Observaciones;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarPaqueteTour(int codigoPaqueteTour)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarPaqueteTour", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoPaqueteTour", SqlDbType.Int).Value = codigoPaqueteTour;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
