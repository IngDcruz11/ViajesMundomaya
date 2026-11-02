using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HotelesController : ControllerBase
	{
		private readonly HotelNegocio hotelNegocio;

		public HotelesController()
		{
			hotelNegocio = new HotelNegocio();
		}

		[HttpGet("ConsultarHoteles")]
		public ActionResult<RespuestaApi<List<HotelConsultaDTO>>> ConsultarHoteles()
		{
			try
			{
				List<Hotel> lista = hotelNegocio.ConsultarHoteles();

				List<HotelConsultaDTO> listaConsulta = lista.Select(item => new HotelConsultaDTO
				{
					CodigoHotel = item.CodigoHotel,
					CodigoDestino = item.CodigoDestino,
					NombreHotel = item.NombreHotel,
					Categoria = item.Categoria,
					Direccion = item.Direccion,
					Telefono = item.Telefono,
					ServiciosIncluidos = item.ServiciosIncluidos,
					NombreDestino = item.NombreDestino,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<HotelConsultaDTO>>
				{
					Exito = true,
					Mensaje = "Hoteles consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<HotelConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar hoteles.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarHotel")]
		public ActionResult<RespuestaApi<bool>> AgregarHotel([FromBody] HotelAgregarDTO hotelAgregarDTO)
		{
			try
			{
				Hotel entidad = new Hotel
				{
					CodigoDestino = hotelAgregarDTO.CodigoDestino,
					NombreHotel = hotelAgregarDTO.NombreHotel,
					Categoria = hotelAgregarDTO.Categoria,
					Direccion = hotelAgregarDTO.Direccion,
					Telefono = hotelAgregarDTO.Telefono,
					ServiciosIncluidos = hotelAgregarDTO.ServiciosIncluidos
				};

				bool resultado = hotelNegocio.AgregarHotel(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Hotel agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarHotel/{codigoHotel:int}")]
		public ActionResult<RespuestaApi<HotelConsultaDTO>> BuscarHotel(int codigoHotel)
		{
			try
			{
				Hotel? entidad = hotelNegocio.BuscarHotel(codigoHotel);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<HotelConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				HotelConsultaDTO dto = new HotelConsultaDTO
				{
					CodigoHotel = entidad.CodigoHotel,
					CodigoDestino = entidad.CodigoDestino,
					NombreHotel = entidad.NombreHotel,
					Categoria = entidad.Categoria,
					Direccion = entidad.Direccion,
					Telefono = entidad.Telefono,
					ServiciosIncluidos = entidad.ServiciosIncluidos,
					NombreDestino = entidad.NombreDestino,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<HotelConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<HotelConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarHotel")]
		public ActionResult<RespuestaApi<bool>> EditarHotel([FromBody] HotelEditarDTO hotelEditarDTO)
		{
			try
			{
				Hotel entidad = new Hotel
				{
					CodigoHotel = hotelEditarDTO.CodigoHotel,
					CodigoDestino = hotelEditarDTO.CodigoDestino,
					NombreHotel = hotelEditarDTO.NombreHotel,
					Categoria = hotelEditarDTO.Categoria,
					Direccion = hotelEditarDTO.Direccion,
					Telefono = hotelEditarDTO.Telefono,
					ServiciosIncluidos = hotelEditarDTO.ServiciosIncluidos,
					Estado = hotelEditarDTO.Estado
				};

				bool resultado = hotelNegocio.EditarHotel(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Hotel editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarHotel/{codigoHotel:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarHotel(int codigoHotel)
		{
			try
			{
				bool resultado = hotelNegocio.EliminarHotel(codigoHotel);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Hotel eliminado correctamente." : "No fue posible eliminar el registro.",
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
