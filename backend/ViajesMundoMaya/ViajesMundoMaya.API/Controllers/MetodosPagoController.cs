using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MetodosPagoController : ControllerBase
	{
		private readonly MetodoPagoNegocio metodoPagoNegocio;

		public MetodosPagoController()
		{
			metodoPagoNegocio = new MetodoPagoNegocio();
		}

		[HttpGet("ConsultarMetodosPago")]
		public ActionResult<RespuestaApi<List<MetodoPagoConsultaDTO>>> ConsultarMetodosPago()
		{
			try
			{
				List<MetodoPago> lista = metodoPagoNegocio.ConsultarMetodosPago();

				List<MetodoPagoConsultaDTO> listaConsulta = lista.Select(item => new MetodoPagoConsultaDTO
				{
					CodigoMetodoPago = item.CodigoMetodoPago,
					NombreMetodoPago = item.NombreMetodoPago,
					Tipo = item.Tipo,
					EntidadFinanciera = item.EntidadFinanciera,
					Descripcion = item.Descripcion,
					ComisionPorcentaje = item.ComisionPorcentaje,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<MetodoPagoConsultaDTO>>
				{
					Exito = true,
					Mensaje = "MetodosPago consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<MetodoPagoConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar metodospago.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarMetodoPago")]
		public ActionResult<RespuestaApi<bool>> AgregarMetodoPago([FromBody] MetodoPagoAgregarDTO metodoPagoAgregarDTO)
		{
			try
			{
				MetodoPago entidad = new MetodoPago
				{
					NombreMetodoPago = metodoPagoAgregarDTO.NombreMetodoPago,
					Tipo = metodoPagoAgregarDTO.Tipo,
					EntidadFinanciera = metodoPagoAgregarDTO.EntidadFinanciera,
					Descripcion = metodoPagoAgregarDTO.Descripcion,
					ComisionPorcentaje = metodoPagoAgregarDTO.ComisionPorcentaje
				};

				bool resultado = metodoPagoNegocio.AgregarMetodoPago(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "MetodoPago agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarMetodoPago/{codigoMetodoPago:int}")]
		public ActionResult<RespuestaApi<MetodoPagoConsultaDTO>> BuscarMetodoPago(int codigoMetodoPago)
		{
			try
			{
				MetodoPago? entidad = metodoPagoNegocio.BuscarMetodoPago(codigoMetodoPago);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<MetodoPagoConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				MetodoPagoConsultaDTO dto = new MetodoPagoConsultaDTO
				{
					CodigoMetodoPago = entidad.CodigoMetodoPago,
					NombreMetodoPago = entidad.NombreMetodoPago,
					Tipo = entidad.Tipo,
					EntidadFinanciera = entidad.EntidadFinanciera,
					Descripcion = entidad.Descripcion,
					ComisionPorcentaje = entidad.ComisionPorcentaje,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<MetodoPagoConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<MetodoPagoConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarMetodoPago")]
		public ActionResult<RespuestaApi<bool>> EditarMetodoPago([FromBody] MetodoPagoEditarDTO metodoPagoEditarDTO)
		{
			try
			{
				MetodoPago entidad = new MetodoPago
				{
					CodigoMetodoPago = metodoPagoEditarDTO.CodigoMetodoPago,
					NombreMetodoPago = metodoPagoEditarDTO.NombreMetodoPago,
					Tipo = metodoPagoEditarDTO.Tipo,
					EntidadFinanciera = metodoPagoEditarDTO.EntidadFinanciera,
					Descripcion = metodoPagoEditarDTO.Descripcion,
					ComisionPorcentaje = metodoPagoEditarDTO.ComisionPorcentaje,
					Estado = metodoPagoEditarDTO.Estado
				};

				bool resultado = metodoPagoNegocio.EditarMetodoPago(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "MetodoPago editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarMetodoPago/{codigoMetodoPago:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarMetodoPago(int codigoMetodoPago)
		{
			try
			{
				bool resultado = metodoPagoNegocio.EliminarMetodoPago(codigoMetodoPago);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "MetodoPago eliminado correctamente." : "No fue posible eliminar el registro.",
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
