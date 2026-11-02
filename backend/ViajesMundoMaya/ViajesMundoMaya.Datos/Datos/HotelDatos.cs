using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class HotelDatos
	{
		public List<Hotel> ConsultarHoteles()
		{
			List<Hotel> lista = new List<Hotel>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarHoteles", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new Hotel
				{
					CodigoHotel = Convert.ToInt32(lector["CodigoHotel"]),
					CodigoDestino = Convert.ToInt32(lector["CodigoDestino"]),
					NombreHotel = lector["NombreHotel"].ToString() ?? string.Empty,
					Categoria = lector["Categoria"].ToString() ?? string.Empty,
					Direccion = lector["Direccion"].ToString() ?? string.Empty,
					Telefono = lector["Telefono"].ToString() ?? string.Empty,
					ServiciosIncluidos = lector["ServiciosIncluidos"].ToString() ?? string.Empty,
					NombreDestino = lector["NombreDestino"].ToString() ?? string.Empty
				});
			}

			return lista;
		}

		public bool AgregarHotel(Hotel entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarHotel", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoDestino", SqlDbType.Int).Value = entidad.CodigoDestino;
			comando.Parameters.Add("@NombreHotel", SqlDbType.VarChar, 150).Value = entidad.NombreHotel;
			comando.Parameters.Add("@Categoria", SqlDbType.VarChar, 20).Value = entidad.Categoria;
			comando.Parameters.Add("@Direccion", SqlDbType.VarChar, 250).Value = entidad.Direccion;
			comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = entidad.Telefono;
			comando.Parameters.Add("@ServiciosIncluidos", SqlDbType.VarChar, 300).Value = entidad.ServiciosIncluidos;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public Hotel? BuscarHotel(int codigoHotel)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarHotelPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoHotel", SqlDbType.Int).Value = codigoHotel;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new Hotel
				{
					CodigoHotel = Convert.ToInt32(lector["CodigoHotel"]),
					CodigoDestino = Convert.ToInt32(lector["CodigoDestino"]),
					NombreHotel = lector["NombreHotel"].ToString() ?? string.Empty,
					Categoria = lector["Categoria"].ToString() ?? string.Empty,
					Direccion = lector["Direccion"].ToString() ?? string.Empty,
					Telefono = lector["Telefono"].ToString() ?? string.Empty,
					ServiciosIncluidos = lector["ServiciosIncluidos"].ToString() ?? string.Empty,
					NombreDestino = lector["NombreDestino"].ToString() ?? string.Empty
				};
			}

			return null;
		}

		public bool EditarHotel(Hotel entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarHotel", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoHotel", SqlDbType.Int).Value = entidad.CodigoHotel;
			comando.Parameters.Add("@CodigoDestino", SqlDbType.Int).Value = entidad.CodigoDestino;
			comando.Parameters.Add("@NombreHotel", SqlDbType.VarChar, 150).Value = entidad.NombreHotel;
			comando.Parameters.Add("@Categoria", SqlDbType.VarChar, 20).Value = entidad.Categoria;
			comando.Parameters.Add("@Direccion", SqlDbType.VarChar, 250).Value = entidad.Direccion;
			comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = entidad.Telefono;
			comando.Parameters.Add("@ServiciosIncluidos", SqlDbType.VarChar, 300).Value = entidad.ServiciosIncluidos;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarHotel(int codigoHotel)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarHotel", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoHotel", SqlDbType.Int).Value = codigoHotel;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
