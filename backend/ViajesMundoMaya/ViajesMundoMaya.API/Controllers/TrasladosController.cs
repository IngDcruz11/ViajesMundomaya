using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TrasladosController : ControllerBase
	{
		private readonly TrasladoNegocio trasladoNegocio;

		public TrasladosController()
		{
			trasladoNegocio = new TrasladoNegocio();
		}

		[HttpGet("ConsultarTraslados")]
		public ActionResult<RespuestaApi<List<TrasladoConsultaDTO>>> ConsultarTraslados()
		{
			try
			{
				List<Traslado> lista = trasladoNegocio.ConsultarTraslados();

				List<TrasladoConsultaDTO> listaConsulta = lista.Select(item => new TrasladoConsultaDTO
				{
					CodigoTraslado = item.CodigoTraslado,
					CodigoPaquete = item.CodigoPaquete,
					CodigoTipoTransporte = item.CodigoTipoTransporte,
					Origen = item.Origen,
					Destino = item.Destino,
					FechaServicio = item.FechaServicio,
					HoraSalida = item.HoraSalida,
					HoraLlegada = item.HoraLlegada,
					NombrePaquete = item.NombrePaquete,
					TipoTransporte = item.TipoTransporte,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<TrasladoConsultaDTO>>
				{
					Exito = true,
					Mensaje = "Traslados consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<TrasladoConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar traslados.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarTraslado")]
		public ActionResult<RespuestaApi<bool>> AgregarTraslado([FromBody] TrasladoAgregarDTO trasladoAgregarDTO)
		{
			try
			{
				Traslado entidad = new Traslado
				{
					CodigoPaquete = trasladoAgregarDTO.CodigoPaquete,
					CodigoTipoTransporte = trasladoAgregarDTO.CodigoTipoTransporte,
					Origen = trasladoAgregarDTO.Origen,
					Destino = trasladoAgregarDTO.Destino,
					FechaServicio = trasladoAgregarDTO.FechaServicio,
					HoraSalida = trasladoAgregarDTO.HoraSalida,
					HoraLlegada = trasladoAgregarDTO.HoraLlegada
				};

				bool resultado = trasladoNegocio.AgregarTraslado(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Traslado agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarTraslado/{codigoTraslado:int}")]
		public ActionResult<RespuestaApi<TrasladoConsultaDTO>> BuscarTraslado(int codigoTraslado)
		{
			try
			{
				Traslado? entidad = trasladoNegocio.BuscarTraslado(codigoTraslado);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<TrasladoConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				TrasladoConsultaDTO dto = new TrasladoConsultaDTO
				{
					CodigoTraslado = entidad.CodigoTraslado,
					CodigoPaquete = entidad.CodigoPaquete,
					CodigoTipoTransporte = entidad.CodigoTipoTransporte,
					Origen = entidad.Origen,
					Destino = entidad.Destino,
					FechaServicio = entidad.FechaServicio,
					HoraSalida = entidad.HoraSalida,
					HoraLlegada = entidad.HoraLlegada,
					NombrePaquete = entidad.NombrePaquete,
					TipoTransporte = entidad.TipoTransporte,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<TrasladoConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<TrasladoConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarTraslado")]
		public ActionResult<RespuestaApi<bool>> EditarTraslado([FromBody] TrasladoEditarDTO trasladoEditarDTO)
		{
			try
			{
				Traslado entidad = new Traslado
				{
					CodigoTraslado = trasladoEditarDTO.CodigoTraslado,
					CodigoPaquete = trasladoEditarDTO.CodigoPaquete,
					CodigoTipoTransporte = trasladoEditarDTO.CodigoTipoTransporte,
					Origen = trasladoEditarDTO.Origen,
					Destino = trasladoEditarDTO.Destino,
					FechaServicio = trasladoEditarDTO.FechaServicio,
					HoraSalida = trasladoEditarDTO.HoraSalida,
					HoraLlegada = trasladoEditarDTO.HoraLlegada,
					Estado = trasladoEditarDTO.Estado
				};

				bool resultado = trasladoNegocio.EditarTraslado(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Traslado editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarTraslado/{codigoTraslado:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarTraslado(int codigoTraslado)
		{
			try
			{
				bool resultado = trasladoNegocio.EliminarTraslado(codigoTraslado);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Traslado eliminado correctamente." : "No fue posible eliminar el registro.",
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
