using Microsoft.Data.SqlClient;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Utilidades.Configuracion;
using System.Data;

namespace ViajesMundoMaya.Datos.Datos
{
	public class ReservaDatos
	{
		public List<Reserva> ConsultarReservas()
		{
			List<Reserva> lista = new List<Reserva>();

			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_ConsultarReservas", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			while (lector.Read())
			{
				lista.Add(new Reserva
				{
					CodigoReserva = Convert.ToInt32(lector["CodigoReserva"]),
					CodigoCliente = Convert.ToInt32(lector["CodigoCliente"]),
					CodigoEmpleado = Convert.ToInt32(lector["CodigoEmpleado"]),
					CodigoPaquete = Convert.ToInt32(lector["CodigoPaquete"]),
					FechaViajeInicio = DateOnly.FromDateTime(Convert.ToDateTime(lector["FechaViajeInicio"])),
					FechaViajeFin = DateOnly.FromDateTime(Convert.ToDateTime(lector["FechaViajeFin"])),
					EstadoReserva = lector["EstadoReserva"].ToString() ?? string.Empty,
					Observaciones = lector["Observaciones"].ToString() ?? string.Empty,
					FechaReserva = Convert.ToDateTime(lector["FechaReserva"]),
					Cliente = lector["Cliente"].ToString() ?? string.Empty,
					Empleado = lector["Empleado"].ToString() ?? string.Empty,
					NombrePaquete = lector["NombrePaquete"].ToString() ?? string.Empty
				});
			}

			return lista;
		}

		public bool AgregarReserva(Reserva entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_AgregarReserva", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoCliente", SqlDbType.Int).Value = entidad.CodigoCliente;
			comando.Parameters.Add("@CodigoEmpleado", SqlDbType.Int).Value = entidad.CodigoEmpleado;
			comando.Parameters.Add("@CodigoPaquete", SqlDbType.Int).Value = entidad.CodigoPaquete;
			comando.Parameters.Add("@FechaViajeInicio", SqlDbType.Date).Value = entidad.FechaViajeInicio.ToDateTime(TimeOnly.MinValue);
			comando.Parameters.Add("@FechaViajeFin", SqlDbType.Date).Value = entidad.FechaViajeFin.ToDateTime(TimeOnly.MinValue);
			comando.Parameters.Add("@EstadoReserva", SqlDbType.VarChar, 20).Value = entidad.EstadoReserva;
			comando.Parameters.Add("@Observaciones", SqlDbType.VarChar, 300).Value = entidad.Observaciones;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public Reserva? BuscarReserva(int codigoReserva)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_BuscarReservaPorId", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoReserva", SqlDbType.Int).Value = codigoReserva;

			conexion.Open();
			using SqlDataReader lector = comando.ExecuteReader();

			if (lector.Read())
			{
				return new Reserva
				{
					CodigoReserva = Convert.ToInt32(lector["CodigoReserva"]),
					CodigoCliente = Convert.ToInt32(lector["CodigoCliente"]),
					CodigoEmpleado = Convert.ToInt32(lector["CodigoEmpleado"]),
					CodigoPaquete = Convert.ToInt32(lector["CodigoPaquete"]),
					FechaViajeInicio = DateOnly.FromDateTime(Convert.ToDateTime(lector["FechaViajeInicio"])),
					FechaViajeFin = DateOnly.FromDateTime(Convert.ToDateTime(lector["FechaViajeFin"])),
					EstadoReserva = lector["EstadoReserva"].ToString() ?? string.Empty,
					Observaciones = lector["Observaciones"].ToString() ?? string.Empty,
					FechaReserva = Convert.ToDateTime(lector["FechaReserva"]),
					Cliente = lector["Cliente"].ToString() ?? string.Empty,
					Empleado = lector["Empleado"].ToString() ?? string.Empty,
					NombrePaquete = lector["NombrePaquete"].ToString() ?? string.Empty
				};
			}

			return null;
		}

		public bool EditarReserva(Reserva entidad)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EditarReserva", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoReserva", SqlDbType.Int).Value = entidad.CodigoReserva;
			comando.Parameters.Add("@CodigoCliente", SqlDbType.Int).Value = entidad.CodigoCliente;
			comando.Parameters.Add("@CodigoEmpleado", SqlDbType.Int).Value = entidad.CodigoEmpleado;
			comando.Parameters.Add("@CodigoPaquete", SqlDbType.Int).Value = entidad.CodigoPaquete;
			comando.Parameters.Add("@FechaViajeInicio", SqlDbType.Date).Value = entidad.FechaViajeInicio.ToDateTime(TimeOnly.MinValue);
			comando.Parameters.Add("@FechaViajeFin", SqlDbType.Date).Value = entidad.FechaViajeFin.ToDateTime(TimeOnly.MinValue);
			comando.Parameters.Add("@EstadoReserva", SqlDbType.VarChar, 20).Value = entidad.EstadoReserva;
			comando.Parameters.Add("@Observaciones", SqlDbType.VarChar, 300).Value = entidad.Observaciones;
			comando.Parameters.Add("@Estado", SqlDbType.Bit).Value = entidad.Estado;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}

		public bool EliminarReserva(int codigoReserva)
		{
			using SqlConnection conexion = new SqlConnection(ConexionSQL.CadenaConexion);
			using SqlCommand comando = new SqlCommand("sp_EliminarReserva", conexion);
			comando.CommandType = CommandType.StoredProcedure;

			comando.Parameters.Add("@CodigoReserva", SqlDbType.Int).Value = codigoReserva;

			conexion.Open();
			comando.ExecuteNonQuery();

			return true;
		}
	}
}
