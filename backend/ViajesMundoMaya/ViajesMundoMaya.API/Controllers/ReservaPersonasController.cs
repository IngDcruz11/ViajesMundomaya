using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReservaPersonasController : ControllerBase
	{
		private readonly ReservaPersonaNegocio reservaPersonaNegocio;

		public ReservaPersonasController()
		{
			reservaPersonaNegocio = new ReservaPersonaNegocio();
		}

		[HttpGet("ConsultarReservaPersonas")]
		public ActionResult<RespuestaApi<List<ReservaPersonaConsultaDTO>>> ConsultarReservaPersonas()
		{
			try
			{
				List<ReservaPersona> lista = reservaPersonaNegocio.ConsultarReservaPersonas();

				List<ReservaPersonaConsultaDTO> listaConsulta = lista.Select(item => new ReservaPersonaConsultaDTO
				{
					CodigoReservaPersona = item.CodigoReservaPersona,
					CodigoReserva = item.CodigoReserva,
					Nombres = item.Nombres,
					Apellidos = item.Apellidos,
					DPI = item.DPI,
					FechaNacimiento = item.FechaNacimiento,
					Parentesco = item.Parentesco,
					Edad = item.Edad,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<ReservaPersonaConsultaDTO>>
				{
					Exito = true,
					Mensaje = "ReservaPersonas consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<ReservaPersonaConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar reservapersonas.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarReservaPersona")]
		public ActionResult<RespuestaApi<bool>> AgregarReservaPersona([FromBody] ReservaPersonaAgregarDTO reservaPersonaAgregarDTO)
		{
			try
			{
				ReservaPersona entidad = new ReservaPersona
				{
					CodigoReserva = reservaPersonaAgregarDTO.CodigoReserva,
					Nombres = reservaPersonaAgregarDTO.Nombres,
					Apellidos = reservaPersonaAgregarDTO.Apellidos,
					DPI = reservaPersonaAgregarDTO.DPI,
					FechaNacimiento = reservaPersonaAgregarDTO.FechaNacimiento,
					Parentesco = reservaPersonaAgregarDTO.Parentesco,
					Edad = reservaPersonaAgregarDTO.Edad
				};

				bool resultado = reservaPersonaNegocio.AgregarReservaPersona(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "ReservaPersona agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarReservaPersona/{codigoReservaPersona:int}")]
		public ActionResult<RespuestaApi<ReservaPersonaConsultaDTO>> BuscarReservaPersona(int codigoReservaPersona)
		{
			try
			{
				ReservaPersona? entidad = reservaPersonaNegocio.BuscarReservaPersona(codigoReservaPersona);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<ReservaPersonaConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				ReservaPersonaConsultaDTO dto = new ReservaPersonaConsultaDTO
				{
					CodigoReservaPersona = entidad.CodigoReservaPersona,
					CodigoReserva = entidad.CodigoReserva,
					Nombres = entidad.Nombres,
					Apellidos = entidad.Apellidos,
					DPI = entidad.DPI,
					FechaNacimiento = entidad.FechaNacimiento,
					Parentesco = entidad.Parentesco,
					Edad = entidad.Edad,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<ReservaPersonaConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<ReservaPersonaConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarReservaPersona")]
		public ActionResult<RespuestaApi<bool>> EditarReservaPersona([FromBody] ReservaPersonaEditarDTO reservaPersonaEditarDTO)
		{
			try
			{
				ReservaPersona entidad = new ReservaPersona
				{
					CodigoReservaPersona = reservaPersonaEditarDTO.CodigoReservaPersona,
					CodigoReserva = reservaPersonaEditarDTO.CodigoReserva,
					Nombres = reservaPersonaEditarDTO.Nombres,
					Apellidos = reservaPersonaEditarDTO.Apellidos,
					DPI = reservaPersonaEditarDTO.DPI,
					FechaNacimiento = reservaPersonaEditarDTO.FechaNacimiento,
					Parentesco = reservaPersonaEditarDTO.Parentesco,
					Edad = reservaPersonaEditarDTO.Edad,
					Estado = reservaPersonaEditarDTO.Estado
				};

				bool resultado = reservaPersonaNegocio.EditarReservaPersona(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "ReservaPersona editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarReservaPersona/{codigoReservaPersona:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarReservaPersona(int codigoReservaPersona)
		{
			try
			{
				bool resultado = reservaPersonaNegocio.EliminarReservaPersona(codigoReservaPersona);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "ReservaPersona eliminado correctamente." : "No fue posible eliminar el registro.",
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
