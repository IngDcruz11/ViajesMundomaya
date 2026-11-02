using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaqueteHotelesController : ControllerBase
	{
		private readonly PaqueteHotelNegocio paqueteHotelNegocio;

		public PaqueteHotelesController()
		{
			paqueteHotelNegocio = new PaqueteHotelNegocio();
		}

		[HttpGet("ConsultarPaqueteHoteles")]
		public ActionResult<RespuestaApi<List<PaqueteHotelConsultaDTO>>> ConsultarPaqueteHoteles()
		{
			try
			{
				List<PaqueteHotel> lista = paqueteHotelNegocio.ConsultarPaqueteHoteles();

				List<PaqueteHotelConsultaDTO> listaConsulta = lista.Select(item => new PaqueteHotelConsultaDTO
				{
					CodigoPaqueteHotel = item.CodigoPaqueteHotel,
					CodigoPaquete = item.CodigoPaquete,
					CodigoHotel = item.CodigoHotel,
					Noches = item.Noches,
					TipoHabitacion = item.TipoHabitacion,
					IncluyeDesayuno = item.IncluyeDesayuno,
					PrecioPorNoche = item.PrecioPorNoche,
					NombrePaquete = item.NombrePaquete,
					NombreHotel = item.NombreHotel,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<PaqueteHotelConsultaDTO>>
				{
					Exito = true,
					Mensaje = "PaqueteHoteles consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<PaqueteHotelConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar paquetehoteles.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarPaqueteHotel")]
		public ActionResult<RespuestaApi<bool>> AgregarPaqueteHotel([FromBody] PaqueteHotelAgregarDTO paqueteHotelAgregarDTO)
		{
			try
			{
				PaqueteHotel entidad = new PaqueteHotel
				{
					CodigoPaquete = paqueteHotelAgregarDTO.CodigoPaquete,
					CodigoHotel = paqueteHotelAgregarDTO.CodigoHotel,
					Noches = paqueteHotelAgregarDTO.Noches,
					TipoHabitacion = paqueteHotelAgregarDTO.TipoHabitacion,
					IncluyeDesayuno = paqueteHotelAgregarDTO.IncluyeDesayuno,
					PrecioPorNoche = paqueteHotelAgregarDTO.PrecioPorNoche
				};

				bool resultado = paqueteHotelNegocio.AgregarPaqueteHotel(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "PaqueteHotel agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarPaqueteHotel/{codigoPaqueteHotel:int}")]
		public ActionResult<RespuestaApi<PaqueteHotelConsultaDTO>> BuscarPaqueteHotel(int codigoPaqueteHotel)
		{
			try
			{
				PaqueteHotel? entidad = paqueteHotelNegocio.BuscarPaqueteHotel(codigoPaqueteHotel);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<PaqueteHotelConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				PaqueteHotelConsultaDTO dto = new PaqueteHotelConsultaDTO
				{
					CodigoPaqueteHotel = entidad.CodigoPaqueteHotel,
					CodigoPaquete = entidad.CodigoPaquete,
					CodigoHotel = entidad.CodigoHotel,
					Noches = entidad.Noches,
					TipoHabitacion = entidad.TipoHabitacion,
					IncluyeDesayuno = entidad.IncluyeDesayuno,
					PrecioPorNoche = entidad.PrecioPorNoche,
					NombrePaquete = entidad.NombrePaquete,
					NombreHotel = entidad.NombreHotel,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<PaqueteHotelConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<PaqueteHotelConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarPaqueteHotel")]
		public ActionResult<RespuestaApi<bool>> EditarPaqueteHotel([FromBody] PaqueteHotelEditarDTO paqueteHotelEditarDTO)
		{
			try
			{
				PaqueteHotel entidad = new PaqueteHotel
				{
					CodigoPaqueteHotel = paqueteHotelEditarDTO.CodigoPaqueteHotel,
					CodigoPaquete = paqueteHotelEditarDTO.CodigoPaquete,
					CodigoHotel = paqueteHotelEditarDTO.CodigoHotel,
					Noches = paqueteHotelEditarDTO.Noches,
					TipoHabitacion = paqueteHotelEditarDTO.TipoHabitacion,
					IncluyeDesayuno = paqueteHotelEditarDTO.IncluyeDesayuno,
					PrecioPorNoche = paqueteHotelEditarDTO.PrecioPorNoche,
					Estado = paqueteHotelEditarDTO.Estado
				};

				bool resultado = paqueteHotelNegocio.EditarPaqueteHotel(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "PaqueteHotel editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarPaqueteHotel/{codigoPaqueteHotel:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarPaqueteHotel(int codigoPaqueteHotel)
		{
			try
			{
				bool resultado = paqueteHotelNegocio.EliminarPaqueteHotel(codigoPaqueteHotel);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "PaqueteHotel eliminado correctamente." : "No fue posible eliminar el registro.",
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
