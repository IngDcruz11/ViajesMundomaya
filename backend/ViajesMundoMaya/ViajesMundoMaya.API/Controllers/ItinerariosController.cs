using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ItinerariosController : ControllerBase
	{
		private readonly ItinerarioNegocio itinerarioNegocio;

		public ItinerariosController()
		{
			itinerarioNegocio = new ItinerarioNegocio();
		}

		[HttpGet("ConsultarItinerarios")]
		public ActionResult<RespuestaApi<List<ItinerarioConsultaDTO>>> ConsultarItinerarios()
		{
			try
			{
				List<Itinerario> lista = itinerarioNegocio.ConsultarItinerarios();

				List<ItinerarioConsultaDTO> listaConsulta = lista.Select(item => new ItinerarioConsultaDTO
				{
					CodigoItinerario = item.CodigoItinerario,
					CodigoPaquete = item.CodigoPaquete,
					Dia = item.Dia,
					DescripcionActividad = item.DescripcionActividad,
					Lugar = item.Lugar,
					HoraInicio = item.HoraInicio,
					HoraFin = item.HoraFin,
					NombrePaquete = item.NombrePaquete,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<ItinerarioConsultaDTO>>
				{
					Exito = true,
					Mensaje = "Itinerarios consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<ItinerarioConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar itinerarios.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarItinerario")]
		public ActionResult<RespuestaApi<bool>> AgregarItinerario([FromBody] ItinerarioAgregarDTO itinerarioAgregarDTO)
		{
			try
			{
				Itinerario entidad = new Itinerario
				{
					CodigoPaquete = itinerarioAgregarDTO.CodigoPaquete,
					Dia = itinerarioAgregarDTO.Dia,
					DescripcionActividad = itinerarioAgregarDTO.DescripcionActividad,
					Lugar = itinerarioAgregarDTO.Lugar,
					HoraInicio = itinerarioAgregarDTO.HoraInicio,
					HoraFin = itinerarioAgregarDTO.HoraFin
				};

				bool resultado = itinerarioNegocio.AgregarItinerario(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Itinerario agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarItinerario/{codigoItinerario:int}")]
		public ActionResult<RespuestaApi<ItinerarioConsultaDTO>> BuscarItinerario(int codigoItinerario)
		{
			try
			{
				Itinerario? entidad = itinerarioNegocio.BuscarItinerario(codigoItinerario);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<ItinerarioConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				ItinerarioConsultaDTO dto = new ItinerarioConsultaDTO
				{
					CodigoItinerario = entidad.CodigoItinerario,
					CodigoPaquete = entidad.CodigoPaquete,
					Dia = entidad.Dia,
					DescripcionActividad = entidad.DescripcionActividad,
					Lugar = entidad.Lugar,
					HoraInicio = entidad.HoraInicio,
					HoraFin = entidad.HoraFin,
					NombrePaquete = entidad.NombrePaquete,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<ItinerarioConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<ItinerarioConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarItinerario")]
		public ActionResult<RespuestaApi<bool>> EditarItinerario([FromBody] ItinerarioEditarDTO itinerarioEditarDTO)
		{
			try
			{
				Itinerario entidad = new Itinerario
				{
					CodigoItinerario = itinerarioEditarDTO.CodigoItinerario,
					CodigoPaquete = itinerarioEditarDTO.CodigoPaquete,
					Dia = itinerarioEditarDTO.Dia,
					DescripcionActividad = itinerarioEditarDTO.DescripcionActividad,
					Lugar = itinerarioEditarDTO.Lugar,
					HoraInicio = itinerarioEditarDTO.HoraInicio,
					HoraFin = itinerarioEditarDTO.HoraFin,
					Estado = itinerarioEditarDTO.Estado
				};

				bool resultado = itinerarioNegocio.EditarItinerario(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Itinerario editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarItinerario/{codigoItinerario:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarItinerario(int codigoItinerario)
		{
			try
			{
				bool resultado = itinerarioNegocio.EliminarItinerario(codigoItinerario);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Itinerario eliminado correctamente." : "No fue posible eliminar el registro.",
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
