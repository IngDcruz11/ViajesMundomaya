using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class TipoTransporteDatos
	{
		public List<TipoTransporte> ConsultarTiposTransporte()
		{
			List<TipoTransporte> lista = new List<TipoTransporte>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarTiposTransporte", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new TipoTransporte
				{
					CodigoTipoTransporte = Convert.ToInt32(lector["CodigoTipoTransporte"]),
					NombreTipo = lector["NombreTipo"].ToString() ?? string.Empty,
					Descripcion = lector["Descripcion"].ToString() ?? string.Empty,
					Capacidad = Convert.ToInt32(lector["Capacidad"]),
					EquipajePermitido = lector["EquipajePermitido"].ToString() ?? string.Empty,
					Comodidades = lector["Comodidades"].ToString() ?? string.Empty
				});
			}

			return lista;
		}

		public bool AgregarTipoTransporte(TipoTransporte entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarTipoTransporte", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@NombreTipo", SqlDbType.VarChar, 100).Value = entidad.NombreTipo;
			comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 200).Value = entidad.Descripcion;
			comando.Parameters.Add("@Capacidad", SqlDbType.Int).Value = entidad.Capacidad;
			comando.Parameters.Add("@EquipajePermitido", SqlDbType.VarChar, 100).Value = entidad.EquipajePermitido;
			comando.Parameters.Add("@Comodidades", SqlDbType.VarChar, 200).Value = entidad.Comodidades;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public TipoTransporte? BuscarTipoTransporte(int codigoTipoTransporte)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarTipoTransportePorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoTipoTransporte", SqlDbType.Int).Value = codigoTipoTransporte;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new TipoTransporte
				{
					CodigoTipoTransporte = Convert.ToInt32(lector["CodigoTipoTransporte"]),
					NombreTipo = lector["NombreTipo"].ToString() ?? string.Empty,
					Descripcion = lector["Descripcion"].ToString() ?? string.Empty,
					Capacidad = Convert.ToInt32(lector["Capacidad"]),
					EquipajePermitido = lector["EquipajePermitido"].ToString() ?? string.Empty,
					Comodidades = lector["Comodidades"].ToString() ?? string.Empty
				};
			}

			return null;
		}

		public bool EditarTipoTransporte(TipoTransporte entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarTipoTransporte", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoTipoTransporte", SqlDbType.Int).Value = entidad.CodigoTipoTransporte;
			comando.Parameters.Add("@NombreTipo", SqlDbType.VarChar, 100).Value = entidad.NombreTipo;
			comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 200).Value = entidad.Descripcion;
			comando.Parameters.Add("@Capacidad", SqlDbType.Int).Value = entidad.Capacidad;
			comando.Parameters.Add("@EquipajePermitido", SqlDbType.VarChar, 100).Value = entidad.EquipajePermitido;
			comando.Parameters.Add("@Comodidades", SqlDbType.VarChar, 200).Value = entidad.Comodidades;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarTipoTransporte(int codigoTipoTransporte)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarTipoTransporte", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoTipoTransporte", SqlDbType.Int).Value = codigoTipoTransporte;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
