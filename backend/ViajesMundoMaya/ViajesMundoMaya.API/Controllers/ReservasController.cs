using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReservasController : ControllerBase
	{
		private readonly ReservaNegocio reservaNegocio;

		public ReservasController()
		{
			reservaNegocio = new ReservaNegocio();
		}

		[HttpGet("ConsultarReservas")]
		public ActionResult<RespuestaApi<List<ReservaConsultaDTO>>> ConsultarReservas()
		{
			try
			{
				List<Reserva> lista = reservaNegocio.ConsultarReservas();

				List<ReservaConsultaDTO> listaConsulta = lista.Select(item => new ReservaConsultaDTO
				{
					CodigoReserva = item.CodigoReserva,
					CodigoCliente = item.CodigoCliente,
					CodigoEmpleado = item.CodigoEmpleado,
					CodigoPaquete = item.CodigoPaquete,
					FechaViajeInicio = item.FechaViajeInicio,
					FechaViajeFin = item.FechaViajeFin,
					EstadoReserva = item.EstadoReserva,
					Observaciones = item.Observaciones,
					FechaReserva = item.FechaReserva,
					Cliente = item.Cliente,
					Empleado = item.Empleado,
					NombrePaquete = item.NombrePaquete,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<ReservaConsultaDTO>>
				{
					Exito = true,
					Mensaje = "Reservas consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<ReservaConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar reservas.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarReserva")]
		public ActionResult<RespuestaApi<bool>> AgregarReserva([FromBody] ReservaAgregarDTO reservaAgregarDTO)
		{
			try
			{
				Reserva entidad = new Reserva
				{
					CodigoCliente = reservaAgregarDTO.CodigoCliente,
					CodigoEmpleado = reservaAgregarDTO.CodigoEmpleado,
					CodigoPaquete = reservaAgregarDTO.CodigoPaquete,
					FechaViajeInicio = reservaAgregarDTO.FechaViajeInicio,
					FechaViajeFin = reservaAgregarDTO.FechaViajeFin,
					EstadoReserva = reservaAgregarDTO.EstadoReserva,
					Observaciones = reservaAgregarDTO.Observaciones
				};

				bool resultado = reservaNegocio.AgregarReserva(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Reserva agregado correctamente." : "No fue posible agregar el registro.",
					Datos = resultado,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<bool>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al agregar el registro.",
					Datos = false,
					Detalle = ex.Message
				});
			}
		}

		[HttpGet("BuscarReserva/{codigoReserva:int}")]
		public ActionResult<RespuestaApi<ReservaConsultaDTO>> BuscarReserva(int codigoReserva)
		{
			try
			{
				Reserva? entidad = reservaNegocio.BuscarReserva(codigoReserva);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<ReservaConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				ReservaConsultaDTO dto = new ReservaConsultaDTO
				{
					CodigoReserva = entidad.CodigoReserva,
					CodigoCliente = entidad.CodigoCliente,
					CodigoEmpleado = entidad.CodigoEmpleado,
					CodigoPaquete = entidad.CodigoPaquete,
					FechaViajeInicio = entidad.FechaViajeInicio,
					FechaViajeFin = entidad.FechaViajeFin,
					EstadoReserva = entidad.EstadoReserva,
					Observaciones = entidad.Observaciones,
					FechaReserva = entidad.FechaReserva,
					Cliente = entidad.Cliente,
					Empleado = entidad.Empleado,
					NombrePaquete = entidad.NombrePaquete,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<ReservaConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<ReservaConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarReserva")]
		public ActionResult<RespuestaApi<bool>> EditarReserva([FromBody] ReservaEditarDTO reservaEditarDTO)
		{
			try
			{
				Reserva entidad = new Reserva
				{
					CodigoReserva = reservaEditarDTO.CodigoReserva,
					CodigoCliente = reservaEditarDTO.CodigoCliente,
					CodigoEmpleado = reservaEditarDTO.CodigoEmpleado,
					CodigoPaquete = reservaEditarDTO.CodigoPaquete,
					FechaViajeInicio = reservaEditarDTO.FechaViajeInicio,
					FechaViajeFin = reservaEditarDTO.FechaViajeFin,
					EstadoReserva = reservaEditarDTO.EstadoReserva,
					Observaciones = reservaEditarDTO.Observaciones,
					Estado = reservaEditarDTO.Estado
				};

				bool resultado = reservaNegocio.EditarReserva(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Reserva editado correctamente." : "No fue posible editar el registro.",
					Datos = resultado,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<bool>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al editar el registro.",
					Datos = false,
					Detalle = ex.Message
				});
			}
		}

		[HttpDelete("EliminarReserva/{codigoReserva:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarReserva(int codigoReserva)
		{
			try
			{
				bool resultado = reservaNegocio.EliminarReserva(codigoReserva);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Reserva eliminado correctamente." : "No fue posible eliminar el registro.",
					Datos = resultado,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<bool>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al eliminar el registro.",
					Datos = false,
					Detalle = ex.Message
				});
			}
		}
	}
}
