using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PagosController : ControllerBase
	{
		private readonly PagoNegocio pagoNegocio;

		public PagosController()
		{
			pagoNegocio = new PagoNegocio();
		}

		[HttpGet("ConsultarPagos")]
		public ActionResult<RespuestaApi<List<PagoConsultaDTO>>> ConsultarPagos()
		{
			try
			{
				List<Pago> lista = pagoNegocio.ConsultarPagos();

				List<PagoConsultaDTO> listaConsulta = lista.Select(item => new PagoConsultaDTO
				{
					CodigoPago = item.CodigoPago,
					CodigoReserva = item.CodigoReserva,
					CodigoMetodoPago = item.CodigoMetodoPago,
					Monto = item.Monto,
					Referencia = item.Referencia,
					NumeroTransaccion = item.NumeroTransaccion,
					EstadoPago = item.EstadoPago,
					FechaPago = item.FechaPago,
					NombreMetodoPago = item.NombreMetodoPago,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<PagoConsultaDTO>>
				{
					Exito = true,
					Mensaje = "Pagos consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<PagoConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar pagos.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarPago")]
		public ActionResult<RespuestaApi<bool>> AgregarPago([FromBody] PagoAgregarDTO pagoAgregarDTO)
		{
			try
			{
				Pago entidad = new Pago
				{
					CodigoReserva = pagoAgregarDTO.CodigoReserva,
					CodigoMetodoPago = pagoAgregarDTO.CodigoMetodoPago,
					Monto = pagoAgregarDTO.Monto,
					Referencia = pagoAgregarDTO.Referencia,
					NumeroTransaccion = pagoAgregarDTO.NumeroTransaccion,
					EstadoPago = pagoAgregarDTO.EstadoPago
				};

				bool resultado = pagoNegocio.AgregarPago(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Pago agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarPago/{codigoPago:int}")]
		public ActionResult<RespuestaApi<PagoConsultaDTO>> BuscarPago(int codigoPago)
		{
			try
			{
				Pago? entidad = pagoNegocio.BuscarPago(codigoPago);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<PagoConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				PagoConsultaDTO dto = new PagoConsultaDTO
				{
					CodigoPago = entidad.CodigoPago,
					CodigoReserva = entidad.CodigoReserva,
					CodigoMetodoPago = entidad.CodigoMetodoPago,
					Monto = entidad.Monto,
					Referencia = entidad.Referencia,
					NumeroTransaccion = entidad.NumeroTransaccion,
					EstadoPago = entidad.EstadoPago,
					FechaPago = entidad.FechaPago,
					NombreMetodoPago = entidad.NombreMetodoPago,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<PagoConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<PagoConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarPago")]
		public ActionResult<RespuestaApi<bool>> EditarPago([FromBody] PagoEditarDTO pagoEditarDTO)
		{
			try
			{
				Pago entidad = new Pago
				{
					CodigoPago = pagoEditarDTO.CodigoPago,
					CodigoReserva = pagoEditarDTO.CodigoReserva,
					CodigoMetodoPago = pagoEditarDTO.CodigoMetodoPago,
					Monto = pagoEditarDTO.Monto,
					Referencia = pagoEditarDTO.Referencia,
					NumeroTransaccion = pagoEditarDTO.NumeroTransaccion,
					EstadoPago = pagoEditarDTO.EstadoPago,
					Estado = pagoEditarDTO.Estado
				};

				bool resultado = pagoNegocio.EditarPago(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Pago editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarPago/{codigoPago:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarPago(int codigoPago)
		{
			try
			{
				bool resultado = pagoNegocio.EliminarPago(codigoPago);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Pago eliminado correctamente." : "No fue posible eliminar el registro.",
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
