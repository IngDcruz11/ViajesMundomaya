using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToursController : ControllerBase
	{
		private readonly TourNegocio tourNegocio;

		public ToursController()
		{
			tourNegocio = new TourNegocio();
		}

		[HttpGet("ConsultarTours")]
		public ActionResult<RespuestaApi<List<TourConsultaDTO>>> ConsultarTours()
		{
			try
			{
				List<Tour> lista = tourNegocio.ConsultarTours();

				List<TourConsultaDTO> listaConsulta = lista.Select(item => new TourConsultaDTO
				{
					CodigoTour = item.CodigoTour,
					CodigoDestino = item.CodigoDestino,
					NombreTour = item.NombreTour,
					Descripcion = item.Descripcion,
					DuracionHoras = item.DuracionHoras,
					PrecioPorPersona = item.PrecioPorPersona,
					Incluye = item.Incluye,
					NombreDestino = item.NombreDestino,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<TourConsultaDTO>>
				{
					Exito = true,
					Mensaje = "Tours consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<TourConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar tours.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarTour")]
		public ActionResult<RespuestaApi<bool>> AgregarTour([FromBody] TourAgregarDTO tourAgregarDTO)
		{
			try
			{
				Tour entidad = new Tour
				{
					CodigoDestino = tourAgregarDTO.CodigoDestino,
					NombreTour = tourAgregarDTO.NombreTour,
					Descripcion = tourAgregarDTO.Descripcion,
					DuracionHoras = tourAgregarDTO.DuracionHoras,
					PrecioPorPersona = tourAgregarDTO.PrecioPorPersona,
					Incluye = tourAgregarDTO.Incluye
				};

				bool resultado = tourNegocio.AgregarTour(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Tour agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarTour/{codigoTour:int}")]
		public ActionResult<RespuestaApi<TourConsultaDTO>> BuscarTour(int codigoTour)
		{
			try
			{
				Tour? entidad = tourNegocio.BuscarTour(codigoTour);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<TourConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				TourConsultaDTO dto = new TourConsultaDTO
				{
					CodigoTour = entidad.CodigoTour,
					CodigoDestino = entidad.CodigoDestino,
					NombreTour = entidad.NombreTour,
					Descripcion = entidad.Descripcion,
					DuracionHoras = entidad.DuracionHoras,
					PrecioPorPersona = entidad.PrecioPorPersona,
					Incluye = entidad.Incluye,
					NombreDestino = entidad.NombreDestino,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<TourConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<TourConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarTour")]
		public ActionResult<RespuestaApi<bool>> EditarTour([FromBody] TourEditarDTO tourEditarDTO)
		{
			try
			{
				Tour entidad = new Tour
				{
					CodigoTour = tourEditarDTO.CodigoTour,
					CodigoDestino = tourEditarDTO.CodigoDestino,
					NombreTour = tourEditarDTO.NombreTour,
					Descripcion = tourEditarDTO.Descripcion,
					DuracionHoras = tourEditarDTO.DuracionHoras,
					PrecioPorPersona = tourEditarDTO.PrecioPorPersona,
					Incluye = tourEditarDTO.Incluye,
					Estado = tourEditarDTO.Estado
				};

				bool resultado = tourNegocio.EditarTour(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Tour editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarTour/{codigoTour:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarTour(int codigoTour)
		{
			try
			{
				bool resultado = tourNegocio.EliminarTour(codigoTour);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Tour eliminado correctamente." : "No fue posible eliminar el registro.",
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
