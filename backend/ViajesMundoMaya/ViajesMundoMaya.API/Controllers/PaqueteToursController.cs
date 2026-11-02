using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaqueteToursController : ControllerBase
	{
		private readonly PaqueteTourNegocio paqueteTourNegocio;

		public PaqueteToursController()
		{
			paqueteTourNegocio = new PaqueteTourNegocio();
		}

		[HttpGet("ConsultarPaqueteTours")]
		public ActionResult<RespuestaApi<List<PaqueteTourConsultaDTO>>> ConsultarPaqueteTours()
		{
			try
			{
				List<PaqueteTour> lista = paqueteTourNegocio.ConsultarPaqueteTours();

				List<PaqueteTourConsultaDTO> listaConsulta = lista.Select(item => new PaqueteTourConsultaDTO
				{
					CodigoPaqueteTour = item.CodigoPaqueteTour,
					CodigoPaquete = item.CodigoPaquete,
					CodigoTour = item.CodigoTour,
					Incluido = item.Incluido,
					Orden = item.Orden,
					Observaciones = item.Observaciones,
					NombrePaquete = item.NombrePaquete,
					NombreTour = item.NombreTour,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<PaqueteTourConsultaDTO>>
				{
					Exito = true,
					Mensaje = "PaqueteTours consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<PaqueteTourConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar paquetetours.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarPaqueteTour")]
		public ActionResult<RespuestaApi<bool>> AgregarPaqueteTour([FromBody] PaqueteTourAgregarDTO paqueteTourAgregarDTO)
		{
			try
			{
				PaqueteTour entidad = new PaqueteTour
				{
					CodigoPaquete = paqueteTourAgregarDTO.CodigoPaquete,
					CodigoTour = paqueteTourAgregarDTO.CodigoTour,
					Incluido = paqueteTourAgregarDTO.Incluido,
					Orden = paqueteTourAgregarDTO.Orden,
					Observaciones = paqueteTourAgregarDTO.Observaciones
				};

				bool resultado = paqueteTourNegocio.AgregarPaqueteTour(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "PaqueteTour agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarPaqueteTour/{codigoPaqueteTour:int}")]
		public ActionResult<RespuestaApi<PaqueteTourConsultaDTO>> BuscarPaqueteTour(int codigoPaqueteTour)
		{
			try
			{
				PaqueteTour? entidad = paqueteTourNegocio.BuscarPaqueteTour(codigoPaqueteTour);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<PaqueteTourConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				PaqueteTourConsultaDTO dto = new PaqueteTourConsultaDTO
				{
					CodigoPaqueteTour = entidad.CodigoPaqueteTour,
					CodigoPaquete = entidad.CodigoPaquete,
					CodigoTour = entidad.CodigoTour,
					Incluido = entidad.Incluido,
					Orden = entidad.Orden,
					Observaciones = entidad.Observaciones,
					NombrePaquete = entidad.NombrePaquete,
					NombreTour = entidad.NombreTour,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<PaqueteTourConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<PaqueteTourConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarPaqueteTour")]
		public ActionResult<RespuestaApi<bool>> EditarPaqueteTour([FromBody] PaqueteTourEditarDTO paqueteTourEditarDTO)
		{
			try
			{
				PaqueteTour entidad = new PaqueteTour
				{
					CodigoPaqueteTour = paqueteTourEditarDTO.CodigoPaqueteTour,
					CodigoPaquete = paqueteTourEditarDTO.CodigoPaquete,
					CodigoTour = paqueteTourEditarDTO.CodigoTour,
					Incluido = paqueteTourEditarDTO.Incluido,
					Orden = paqueteTourEditarDTO.Orden,
					Observaciones = paqueteTourEditarDTO.Observaciones,
					Estado = paqueteTourEditarDTO.Estado
				};

				bool resultado = paqueteTourNegocio.EditarPaqueteTour(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "PaqueteTour editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarPaqueteTour/{codigoPaqueteTour:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarPaqueteTour(int codigoPaqueteTour)
		{
			try
			{
				bool resultado = paqueteTourNegocio.EliminarPaqueteTour(codigoPaqueteTour);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "PaqueteTour eliminado correctamente." : "No fue posible eliminar el registro.",
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
