using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class DestinoDatos
	{
		public List<Destino> ConsultarDestinos()
		{
			List<Destino> lista = new List<Destino>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarDestinos", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new Destino
				{
					CodigoDestino = Convert.ToInt32(lector["CodigoDestino"]),
					NombreDestino = lector["NombreDestino"].ToString() ?? string.Empty,
					Pais = lector["Pais"].ToString() ?? string.Empty,
					Departamento = lector["Departamento"].ToString() ?? string.Empty,
					Ciudad = lector["Ciudad"].ToString() ?? string.Empty,
					Descripcion = lector["Descripcion"].ToString() ?? string.Empty,
					Imagen = lector["Imagen"].ToString() ?? string.Empty
				});
			}

			return lista;
		}

		public bool AgregarDestino(Destino entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarDestino", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@NombreDestino", SqlDbType.VarChar, 150).Value = entidad.NombreDestino;
			comando.Parameters.Add("@Pais", SqlDbType.VarChar, 100).Value = entidad.Pais;
			comando.Parameters.Add("@Departamento", SqlDbType.VarChar, 100).Value = entidad.Departamento;
			comando.Parameters.Add("@Ciudad", SqlDbType.VarChar, 100).Value = entidad.Ciudad;
			comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 300).Value = entidad.Descripcion;
			comando.Parameters.Add("@Imagen", SqlDbType.VarChar, 300).Value = entidad.Imagen;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public Destino? BuscarDestino(int codigoDestino)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarDestinoPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoDestino", SqlDbType.Int).Value = codigoDestino;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new Destino
				{
					CodigoDestino = Convert.ToInt32(lector["CodigoDestino"]),
					NombreDestino = lector["NombreDestino"].ToString() ?? string.Empty,
					Pais = lector["Pais"].ToString() ?? string.Empty,
					Departamento = lector["Departamento"].ToString() ?? string.Empty,
					Ciudad = lector["Ciudad"].ToString() ?? string.Empty,
					Descripcion = lector["Descripcion"].ToString() ?? string.Empty,
					Imagen = lector["Imagen"].ToString() ?? string.Empty
				};
			}

			return null;
		}

		public bool EditarDestino(Destino entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarDestino", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoDestino", SqlDbType.Int).Value = entidad.CodigoDestino;
			comando.Parameters.Add("@NombreDestino", SqlDbType.VarChar, 150).Value = entidad.NombreDestino;
			comando.Parameters.Add("@Pais", SqlDbType.VarChar, 100).Value = entidad.Pais;
			comando.Parameters.Add("@Departamento", SqlDbType.VarChar, 100).Value = entidad.Departamento;
			comando.Parameters.Add("@Ciudad", SqlDbType.VarChar, 100).Value = entidad.Ciudad;
			comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 300).Value = entidad.Descripcion;
			comando.Parameters.Add("@Imagen", SqlDbType.VarChar, 300).Value = entidad.Imagen;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarDestino(int codigoDestino)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarDestino", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoDestino", SqlDbType.Int).Value = codigoDestino;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
